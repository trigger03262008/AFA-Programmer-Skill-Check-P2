# AFA Programmer Skill Check P2

**課題……というかテストです。
これには特に答えも乗っけてないので、出来たって人は暇な時に俺に声かけてください。
AIに丸投げは禁止、聞くのはおｋ。丸投げしたらすぐ分かるからね。

## 起動

1. このリポジトリをclone、またはCode → Download ZIPで取得して展開します。
2. Unity Hubでルートフォルダを追加し、**Unity 6000.6.8f1**で開きます。
3. 読み込み完了後、上部メニュー **AFA → P2課題を開く** を選び、▶を押します。

シーンを直接開く場合は `Assets/_AFA/Assessments/P2/Scenes/P2_SkillCheck.unity` です。
空のUntitledシーンでは何も表示されません。Gameタブをダブルクリックすると表示を拡大できます。
ボタンでメッセージだけが変わり、数値が変わらないのは未実装のひな形として正常です。

## 以前の版から更新する場合

Unityを終了してから、このプロジェクトのフォルダで `git pull --ff-only` を実行し、Unity Hubで使用するEditorを **6000.6.8f1** に指定して開き直します。実装中の変更は先にコミットしてください。ZIPで取得した場合は最新版を別フォルダへ展開し、実装中のファイルへ上書きしないでください。

Editorの入手先は [Unity 6000.6.8f1公式ページ](https://unity.com/releases/editor/whats-new/6000.6.8f1) です。

## 資料

- [受験者向け仕様・AI利用ルール・提出方法](ASSESSMENT_README.md)
- [採点基準・手動テスト・口頭質問](ASSESSMENT_RUBRIC.md)
- [提出メモ](Assets/_AFA/Assessments/P2/SUBMISSION.md)
- [検証記録](VALIDATION.md)

`main`は配布版、`assessment/p2`は受験作業の開始用です。画像を含め通常のGitで取得でき、Git LFSは不要です。
日本語表示はOSフォントを使用します。Windowsで検証しています。他OSでは日本語対応フォントが必要です。
