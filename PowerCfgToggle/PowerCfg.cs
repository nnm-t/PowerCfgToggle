using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace PowerCfgToggle
{
    public class PowerCfg
    {
        private Regex guidLineRegex = new Regex("^電源設定の GUID");
        
        private Regex guidRegex = new Regex("([0-9a-fA-F]{8})-([0-9a-fA-F]{4})-([0-9a-fA-F]{4})-([0-9a-fA-F]{4})-([0-9a-fA-F]{12})");
        
        private Regex currentRegex = new Regex(@"\*$");

        public static void Execute()
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "powercfg.exe",
                Arguments = "/l",
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            var process = Process.Start(startInfo);
            var output = process.StandardOutput;

            var str = output.ReadToEnd();

            Console.Write(str);
        }
    }
}