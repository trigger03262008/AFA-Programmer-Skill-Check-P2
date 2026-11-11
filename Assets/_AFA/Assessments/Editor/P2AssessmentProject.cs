using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace AFA.Assessments.Editor
{
    // 配布プロジェクトの起動・ビルド用。課題のゲーム処理は含みません。
    public static class P2AssessmentProject
    {
        public const string ScenePath = "Assets/_AFA/Assessments/P2/Scenes/P2_SkillCheck.unity";

        [MenuItem("AFA/P2課題を開く")]
        public static void OpenScene()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                Debug.Log("Playを停止してから課題シーンを開いてください。");
                return;
            }
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                EditorSceneManager.OpenScene(ScenePath);
        }

        // 初回の配布環境作成に使用。受験者が実行する必要はありません。
        public static void ConfigureProject()
        {
            PlayerSettings.companyName = "AFA";
            PlayerSettings.productName = "AFA Programmer Skill Check P2";
            PlayerSettings.defaultScreenWidth = 1100;
            PlayerSettings.defaultScreenHeight = 740;
            PlayerSettings.fullScreenMode = FullScreenMode.Windowed;
            EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(ScenePath, true) };
            AssetDatabase.SaveAssets();
            EditorSceneManager.OpenScene(ScenePath);
        }

        public static void BuildPreview()
        {
            var result = BuildPipeline.BuildPlayer(new BuildPlayerOptions {
                scenes = new[] { ScenePath },
                target = BuildTarget.StandaloneWindows64,
                locationPathName = "Builds/P2/SkillCheck.exe",
                options = BuildOptions.None
            });
            if (result.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
                throw new InvalidOperationException("P2課題のビルドに失敗しました。");
        }
    }
}
