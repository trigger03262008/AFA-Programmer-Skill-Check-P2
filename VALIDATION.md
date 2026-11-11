# 配布環境の検証

検証日: 2026-11-11 / Windows / Unity 6000.3.23f1

この記録は未実装の配布ひな形に対するものです。受験者の解答が完成していることを示すものではありません。

| 確認 | 結果 |
| --- | --- |
| 新しい独立プロジェクトでのインポート・C#コンパイル | 成功 |
| EditMode | 3件成功（依存分離、必要なUI・画像・テーマ、P2だけのビルド対象） |
| PlayMode | 2件成功（シーン読み込み・初期値・UI表示領域、3ボタンの配線と未実装状態） |
| Windows x64ビルド | 成功。P2_SkillCheckだけを含む |
| 受験者用Controller | 元のTODOひな形と一致。攻撃・移動・到達・リセット・速度設定の解答は未実装 |
| 公開対象 | P2課題資産、配布用ツールとテスト、Unity設定、日本語資料。元のゲームやGit履歴を含まない |

PlayMode検証は自動テストによるものです。この独立版について、新たな手動のUnity Editor操作・画面外観の検証は行っていません。macOS / Linuxは未検証です。

## 再実行

UnityのWindow → General → Test RunnerでEditModeとPlayModeを実行します。
`AssessmentTemplateOnly` は未実装ひな形専用であり、解答実装後の合否判定には使いません。

バッチモードの実行例（Unity実行ファイルとプロジェクトへのパスは自分の環境に合わせてください）:

```text
Unity.exe -batchmode -nographics -projectPath <project> -runTests -testPlatform EditMode -testResults <editmode.xml> -logFile <editmode.log>
Unity.exe -batchmode -nographics -projectPath <project> -runTests -testPlatform PlayMode -testResults <playmode.xml> -logFile <playmode.log>
Unity.exe -batchmode -nographics -quit -projectPath <project> -executeMethod AFA.Assessments.Editor.P2AssessmentProject.BuildPreview -logFile <build.log>
```

同じプロジェクトをUnity Editorで開いたままバッチ実行しないでください。
ビルド先は `Builds/P2/SkillCheck.exe` です。生成ビルド、キャッシュ、テストログはGitに含めません。
