using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace AuraCpuSyncServiceInstaller
{
    class Program
    {
        static void Main()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            ProcessStartInfo installInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                UseShellExecute = true,
                FileName = "AuraCpuSync.exe",
                WindowStyle = ProcessWindowStyle.Normal,
                Arguments = "install"
            };

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(installInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch
            {
                // Log error.
            }

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                CreateNoWindow = false,
                UseShellExecute = true,
                FileName = "AuraCpuSync.exe",
                WindowStyle = ProcessWindowStyle.Normal,
                Arguments = "start"
            };

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
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
