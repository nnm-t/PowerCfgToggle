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

        private readonly ProcessStartInfo startInfo;
        
        private PowerConfigPair currentConfig;

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

        public string Execute()
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
            
            // 次の電源設定にスイッチ
            return ApplyConfig(FindNextConfig());
        }

        private string ApplyConfig(PowerConfigPair config)
        {
            // 引数を設定コマンドへ変更
            startInfo.Arguments = $"/s {config.Guid}";
            
            // コマンド実行
            var process = Process.Start(startInfo);

            if (process == null)
            {
                throw new InvalidOperationException();
            }
            
            process.WaitForExit();

            return config.Name;
        }

        private PowerConfigPair FindNextConfig()
        {
            foreach (var pair in powerConfigs)
            {
                if (!Equals(currentConfig, pair))
                {
                    continue;
                }
                
                // リストから一致するか探す
                
                if (Equals(pair, powerConfigs.Last.Value))
                {
                    // 現設定がリストの最後の時, 最初のノードの値を反映
                    return powerConfigs.First.Value;
                }
                
                // そうでない時は次のノードの値を反映
                return powerConfigs.Find(pair)?.Next?.Value;
            }

            return null;
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

            process.WaitForExit();
        }
    }
}