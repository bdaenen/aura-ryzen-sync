using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace AuraCpuSyncServiceUninstaller
{
    class Program
    {
        static void Main()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            ProcessStartInfo stopInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                UseShellExecute = true,
                FileName = "AuraCpuSync.exe",
                WindowStyle = ProcessWindowStyle.Normal,
                Arguments = "stop"
            };

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(stopInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch
            {
                // Log error.
            }

            ProcessStartInfo uninstallInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                UseShellExecute = true,
                FileName = "AuraCpuSync.exe",
                WindowStyle = ProcessWindowStyle.Normal,
                Arguments = "uninstall"
            };

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(uninstallInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch
            {
                // Log error.
            }
        }
    }
}
