using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace PowerCfgToggle
{
    public class PowerConfig
    {
        private readonly Regex guidLineRegex = new Regex("^電源設定の GUID");

        private readonly Regex guidRegex =
            new Regex("([0-9a-fA-F]{8})-([0-9a-fA-F]{4})-([0-9a-fA-F]{4})-([0-9a-fA-F]{4})-([0-9a-fA-F]{12})");

        private readonly Regex currentRegex = new Regex(@"\*$");

        private readonly LinkedList<PowerConfigPair> powerConfigs;
        
        private PowerConfigPair currentConfig;

        private ProcessStartInfo startInfo;

        public PowerConfig()
        {
            startInfo = new ProcessStartInfo
            {
                FileName = "powercfg.exe",
                RedirectStandardOutput = true,
                UseShellExecute = false
            };
            
            powerConfigs = new LinkedList<PowerConfigPair>();
        }

        public void Execute()
        {
            // GUID一覧を取得
            foreach (var pair in PickUpGuid())
            {
                if (powerConfigs.Contains(pair))
                {
                    continue;
                }
                
                powerConfigs.AddLast(pair);
            }
        }

        private IEnumerable<PowerConfigPair> PickUpGuid()
        {
            // 一覧表示
            startInfo.Arguments = "/l";
                
            var process = Process.Start(startInfo);

            if (process == null)
            {
                throw new InvalidOperationException();
            }

            using (var output = process.StandardOutput)
            {
                while (!output.EndOfStream)
                {
                    var line = output.ReadLine();

                    if (line == null)
                    {
                        break;
                    }

                    // GUIDが含まれる行か判定
                    if (!guidLineRegex.IsMatch(line))
                    {
                        continue;
                    }

                    var firstBracket = line.IndexOf('(');
                    var lastBracket = line.LastIndexOf(')');

                    // 先頭と末尾の括弧の位置から名前を抜き出す
                    var name = line.Substring(firstBracket + 1, lastBracket - firstBracket - 1);
                    // 正規表現でGUIDを抜き出す
                    var guid = guidRegex.Match(line).Value;
                    
                    var pair = new PowerConfigPair(name, guid);

                    // 現在の電源設定である時は保持
                    if (currentRegex.IsMatch(line))
                    {
                        currentConfig = pair;
                    }

                    yield return pair;
                }
            }
        }
    }
}