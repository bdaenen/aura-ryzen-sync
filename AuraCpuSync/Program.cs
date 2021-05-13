using Newtonsoft.Json;
using System;

using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using Topshelf;

namespace AuraCpuSync
{
    class Program
    {
        static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            StreamReader r = new StreamReader("settings.json");
            string json = r.ReadToEnd();
            Settings settings = JsonConvert.DeserializeObject<Settings>(json);
            TempReader tempReader = new TempReader(settings.SensorName);
            LedUpdater ledUpdater = new LedUpdater();
            if (settings.EnableRazerChroma)
            {
                _ = ledUpdater.InitChromaAsync();
            }
            ServiceRunner serviceRunner = new ServiceRunner(() =>
            {
                float? temp = tempReader.ReadTemp();
//                Console.Write("temp: "); Console.WriteLine(temp);
                if (temp != null)
                {
                    byte[] color = TempToColor(temp, settings);
//                    Console.WriteLine("{0},{1},{2}",color[0], color[1], color[2]);
                    ledUpdater.UpdateColor(color[0], color[1], color[2]);
                    return true;
                }
                return false;
            }, settings.PollingInterval);


            if (!Environment.UserInteractive || args.Length > 0)
            {
                var rc = Topshelf.HostFactory.Run(x =>
                {
                    x.Service<ServiceRunner>(s =>
                    {
                        s.ConstructUsing(name => serviceRunner);
                        s.WhenStarted(sr => sr.Start());
                        s.WhenStopped(sr => sr.Stop());
                    });
                    x.RunAsLocalSystem();

                    x.SetDescription("Keeps your Aura Sync-compatible RGB lighting in sync with your Ryzen CPU temps.");
                    x.SetDisplayName("Aura Ryzen Sync");
                    x.SetServiceName("Aura Ryzen Sync");
                    x.DependsOn("asComSvc");
                    x.DependsOn("AsSysCtrlService");
                    
                });

                var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
                Environment.ExitCode = exitCode;
            }
            else
            {
                serviceRunner.Start();

                Console.WriteLine("Press any key to stop...");
                Console.ReadKey(true);

                serviceRunner.Stop();
            }
        }

        // TODO: this only seems to work properly for primary colors.
        static byte[] TempToColor(float? temp, Settings settings)
        {
            // TODO: add config
            float minTemp = settings.ColdTemp;
            // TODO: add config
            float maxTemp = settings.HotTemp;
            if (temp > maxTemp)
            {
                temp = maxTemp;
            }

            float? percentage = 1 - (maxTemp - temp) * 1 / (maxTemp - minTemp);

            byte coldColorRed = (byte)settings.ColdColorBlue;
            byte coldColorGreen = (byte)settings.ColdColorGreen;
            byte coldColorBlue = (byte)settings.ColdColorBlue;
            byte hotColorRed = (byte)settings.HotColorRed;
            byte hotColorGreen = (byte)settings.HotColorGreen;
            byte hotColorBlue = (byte)settings.HotColorBlue;


            byte red = (byte)(coldColorRed + percentage * (hotColorRed - coldColorRed));
            byte green = (byte)(coldColorGreen + percentage * (hotColorGreen - coldColorGreen));
            byte blue = (byte)(coldColorBlue + percentage * (hotColorBlue - coldColorBlue));

            return new byte[] {
                red, green, blue
            };
        }

        public void Shutdown()
        {

        }
    }
}
