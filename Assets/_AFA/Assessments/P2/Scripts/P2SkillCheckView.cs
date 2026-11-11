using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace AFA.Assessments.P2
{
    /// <summary>準備済みの表示・配線。ダメージ、移動、到達、撃破の判定は行いません。</summary>
    public sealed class P2SkillCheckView : MonoBehaviour
    {
        [SerializeField] private UIDocument document;
        [SerializeField] private Texture2D enemyImage;
        private Action attack;
        private Action endTurn;
        private Action reset;
        private Button endTurnButton;
        private Button resetButton;
        private Label turnLabel;
        private Label hpLabel;
        private Label positionLabel;
        private Label defenseLabel;
        private Label noticeLabel;
        private Font displayFont;
        public Button AttackButton { get; private set; }

        public void Bind(Action onAttack, Action onEndTurn, Action onReset)
        {
            Unbind();
            VisualElement root = document.rootVisualElement;
            displayFont = Font.CreateDynamicFontFromOSFont(new[] { "Yu Gothic", "Meiryo", "Arial" }, 18);
            root.style.unityFont = displayFont;
            AttackButton = Required<Button>("attack-button");
            endTurnButton = Required<Button>("end-turn-button");
            resetButton = Required<Button>("reset-button");
            turnLabel = Required<Label>("turn-label");
            hpLabel = Required<Label>("hp-label");
            positionLabel = Required<Label>("position-label");
            defenseLabel = Required<Label>("defense-label");
            noticeLabel = Required<Label>("notice-label");
            Required<Image>("enemy-image").image = enemyImage;
            attack = onAttack;
            endTurn = onEndTurn;
            reset = onReset;
            AttackButton.clicked += attack;
            endTurnButton.clicked += endTurn;
            resetButton.clicked += reset;
        }

        public void RenderCommon(int turn, int enemyHp, int position, int defenseHp)
        {
            turnLabel.text = "ターン  " + turn;
            hpLabel.text = "敵HP  " + enemyHp;
            positionLabel.text = "敵進行位置  " + position;
            defenseLabel.text = "防衛HP  " + defenseHp;
            // これはマスの表示位置だけを選ぶ処理です。ゲーム状態は変更しません。
            int visibleCell = Mathf.Clamp(position, 0, 3);
            for (int index = 0; index < 4; index++)
                Required<VisualElement>("cell-" + index).EnableInClassList("current-cell", index == visibleCell);
        }

        public void ShowNotice(string message) { noticeLabel.text = message; }

        private T Required<T>(string elementName) where T : VisualElement
        {
            T element = document.rootVisualElement.Q<T>(elementName);
            if (element == null) throw new InvalidOperationException("課題UIの参照が見つかりません: " + elementName);
            return element;
        }

        private void Unbind()
        {
            if (AttackButton != null && attack != null) AttackButton.clicked -= attack;
            if (endTurnButton != null && endTurn != null) endTurnButton.clicked -= endTurn;
            if (resetButton != null && reset != null) resetButton.clicked -= reset;
            if (displayFont) Destroy(displayFont);
        }

        private void OnDestroy() { Unbind(); }
    }
}
