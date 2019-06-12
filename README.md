# PowerCfgToggle

## これは何?

Windows の電源設定コマンド `powercfg.exe` を呼び出して電源オプションを切り替えるユーティリティ.

Ryzen Mobile の発熱が激しく, 電源オプションにて `プロセッサの電源管理 -> 最大のプロセッサの状態` を 99% 以下とした電源設定を作成するとスロットリングが発生しにくくなるものの, クロックが 1.68GHz (Ryzen 7 2700U の場合) で頭打ちになるため, 切り替えの手間を軽減する目的で開発.

### 動作環境

- Windows 10
- .NET Framework 4.7

## ビルド

Visual Studio などの IDE がインストール済みの環境があればそちらでビルドが可能.

### IDE がない場合

- [Build Tools for Visual Studio](https://visualstudio.microsoft.com/ja/downloads/) を入手してインストールする.
- Developer Command Prompt for Visual Studio を起動する.
- ソースコードのディレクトリへ移動して `MSBuild.exe` を実行する.

## 使い方

`PowerCfgToggle.exe` 単体で動作する.
`exe` ファイルを実行すると Windows で設定した電源設定が順番に切り替わり, 通知が表示される.

通知をクリックして閉じるか, 一定時間経過すると自動で終了する.

ThinkPad では Fn + F12 キーに登録しておくと一発で呼び出せて便利.

## クレジット

通知領域のアイコンに [Material Design Icons](https://materialdesignicons.com/) の `battery` を使用しており, [Apache License 2.0](https://github.com/google/material-design-icons/blob/master/LICENSE) でライセンスされている.