#if UNITY_EDITOR
using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

namespace AFA.Assessments.Tests
{
    public sealed class AssessmentSceneTests
    {
        [UnityTearDown] public IEnumerator CleanupScenes()
        {
            for (int i = SceneManager.sceneCount - 1; i >= 0; i--)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.path.StartsWith("Assets/_AFA/Assessments/")) yield return SceneManager.UnloadSceneAsync(scene);
            }
        }

        [UnityTest] public IEnumerator AllScenesLoadWithInitialStateAndVisibleControls()
        {
            foreach (string role in new[] { "P2" })
            {
                yield return Load(role);
                Scene scene = SceneManager.GetSceneByName(role + "_SkillCheck");
                UIDocument document = scene.GetRootGameObjects().Select(item => item.GetComponent<UIDocument>()).First(item => item != null);
                VisualElement ui = document.rootVisualElement;
                Assert.IsNotNull(ui.panel);
                Assert.AreEqual("ターン  1", ui.Q<Label>("turn-label").text);
                Assert.AreEqual("敵HP  5", ui.Q<Label>("hp-label").text);
                Assert.AreEqual("敵進行位置  0", ui.Q<Label>("position-label").text);
                Assert.AreEqual("防衛HP  3", ui.Q<Label>("defense-label").text);
                Assert.IsNotNull(ui.Q<Image>("enemy-image").image);
                foreach (string name in new[] { "attack-button", "end-turn-button", "reset-button" })
                {
                    Button button = ui.Q<Button>(name);
                    Assert.IsTrue(button.enabledInHierarchy);
                    Assert.Greater(button.resolvedStyle.width, 0);
                    Assert.Greater(button.resolvedStyle.height, 0);
                }
                yield return SceneManager.UnloadSceneAsync(scene);
            }
        }

        [UnityTest, Category("AssessmentTemplateOnly")]
        public IEnumerator TemplateButtonsAreConnectedButDoNotImplementTheAnswers()
        {
            // 配布ひな形専用の検査。受験後の正答採点には使用しない。
            foreach (string role in new[] { "P2" })
            {
                yield return Load(role);
                Scene scene = SceneManager.GetSceneByName(role + "_SkillCheck");
                UIDocument document = scene.GetRootGameObjects().Select(item => item.GetComponent<UIDocument>()).First(item => item != null);
                VisualElement ui = document.rootVisualElement;
                string[] names = { "attack-button", "end-turn-button", "reset-button" };
                string[] prefixes = { "攻撃要求", "ターン終了要求", "リセット要求" };
                for (int i = 0; i < names.Length; i++)
                {
                    Button button = ui.Q<Button>(names[i]);
                    using (NavigationSubmitEvent submit = NavigationSubmitEvent.GetPooled())
                    {
                        submit.target = button;
                        button.SendEvent(submit);
                    }
                    yield return null;
                    Assert.That(ui.Q<Label>("notice-label").text, Does.StartWith(prefixes[i]));
                    Assert.AreEqual("ターン  1", ui.Q<Label>("turn-label").text);
                    Assert.AreEqual("敵HP  5", ui.Q<Label>("hp-label").text);
                    Assert.AreEqual("敵進行位置  0", ui.Q<Label>("position-label").text);
                    Assert.AreEqual("防衛HP  3", ui.Q<Label>("defense-label").text);
                }
                yield return SceneManager.UnloadSceneAsync(scene);
            }
        }

        private static IEnumerator Load(string role)
        {
            string path = "Assets/_AFA/Assessments/" + role + "/Scenes/" + role + "_SkillCheck.unity";
            yield return EditorSceneManager.LoadSceneAsyncInPlayMode(path, new LoadSceneParameters(LoadSceneMode.Additive));
            yield return null;
            yield return null;
        }
    }
}
#endif
