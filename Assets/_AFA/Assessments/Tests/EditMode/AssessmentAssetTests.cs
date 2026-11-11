using System.IO;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AFA.Assessments.Tests
{
    /// <summary>課題の正答を採点せず、配布環境の配置と分離を検査する。</summary>
    public sealed class AssessmentAssetTests
    {
        private const string Root = "Assets/_AFA/Assessments/";

        [TestCase("P2")]
        public void SceneAssetsAreLocalToOneCandidate(string role)
        {
            string scene = Root + role + "/Scenes/" + role + "_SkillCheck.unity";
            Assert.IsNotNull(AssetDatabase.LoadAssetAtPath<SceneAsset>(scene));
            foreach (string dependency in AssetDatabase.GetDependencies(scene, true))
                if (dependency.StartsWith("Assets/"))
                    Assert.That(dependency, Does.StartWith(Root + role + "/"), "他担当・本体への依存: " + dependency);
            string assembly = File.ReadAllText(Root + role + "/Scripts/AFA.Assessment." + role + ".asmdef");
            Assert.That(assembly, Does.Not.Contain("AFA.Runtime"));
            Assert.That(assembly, Does.Not.Contain("references"));
        }

        [TestCase("P2")]
        public void UiContainsRequiredControlsAndLocalAssets(string role)
        {
            string folder = Root + role + "/UI/";
            VisualTreeAsset tree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(folder + role + "SkillCheck.uxml");
            Assert.IsNotNull(tree);
            TemplateContainer ui = tree.CloneTree();
            foreach (string name in new[] { "attack-button", "end-turn-button", "reset-button" }) Assert.IsNotNull(ui.Q<Button>(name));
            foreach (string name in new[] { "turn-label", "hp-label", "position-label", "defense-label", "notice-label" }) Assert.IsNotNull(ui.Q<Label>(name));
            for (int i = 0; i < 4; i++) Assert.IsNotNull(ui.Q<VisualElement>("cell-" + i));
            Assert.IsNotNull(ui.Q<Image>("enemy-image"));
            Assert.IsNotNull(AssetDatabase.LoadAssetAtPath<Texture2D>(folder + role + "Enemy.png"));
            PanelSettings panel = AssetDatabase.LoadAssetAtPath<PanelSettings>(folder + role + "PanelSettings.asset");
            Assert.IsNotNull(panel);
            Assert.IsNotNull(panel.themeStyleSheet);
        }

        [Test] public void BuildIncludesOnlyTheP2Assessment()
        {
            Assert.AreEqual(1, EditorBuildSettings.scenes.Length);
            Assert.IsTrue(EditorBuildSettings.scenes[0].enabled);
            Assert.AreEqual(Root + "P2/Scenes/P2_SkillCheck.unity", EditorBuildSettings.scenes[0].path);
        }
    }
}
