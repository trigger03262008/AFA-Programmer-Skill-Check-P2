using UnityEngine;

namespace AFA.Assessments.P2
{
    /// <summary>受験者が編集するひな形。共通ルールと担当別ルールは未実装です。</summary>
    public sealed class P2SkillCheckController : MonoBehaviour
    {
        [SerializeField, Tooltip("接続済みの表示担当。通常は変更不要です。")]
        private P2SkillCheckView view;

        [Header("課題の実行状態（開始値を設定済み）")]
        [SerializeField] private int turn = 1;
        [SerializeField] private int enemyHp = 5;
        [SerializeField] private int enemyPosition = 0;
        [SerializeField] private int defenseHp = 3;
        [SerializeField] private bool enemyDefeated = false;
        [SerializeField] private bool enemyArrived = false;

        // TODO P2: Inspectorから変更できる移動速度を定義し、ターン終了処理で利用してください。

        public int CurrentTurn => turn;
        public int EnemyHp => enemyHp;
        public int EnemyPosition => enemyPosition;
        public int DefenseHp => defenseHp;
        public bool EnemyDefeated => enemyDefeated;
        public bool EnemyArrived => enemyArrived;

        private void Start()
        {
            view.Bind(Attack, EndTurn, ResetBattle);
            RefreshDisplay();
        }

        public void Attack()
        {
            // TODO 共通: 攻撃要求を処理してください。仕様はASSESSMENT_README.mdを参照。
            view.ShowNotice("攻撃要求を受信しました。課題の処理は未実装です。");
            RefreshDisplay();
        }

        public void EndTurn()
        {
            // TODO 共通: ターン更新、敵の移動、到達を処理してください。
            // TODO P2: Inspectorで設定した移動速度に対応してください。
            view.ShowNotice("ターン終了要求を受信しました。課題の処理は未実装です。");
            RefreshDisplay();
        }

        public void ResetBattle()
        {
            // TODO 共通: 変化した状態を初期状態に戻してください。
            view.ShowNotice("リセット要求を受信しました。課題の処理は未実装です。");
            RefreshDisplay();
        }

        private void RefreshDisplay()
        {
            // 数値テキストと位置表示への受け渡しは準備済みです。
            view.RenderCommon(turn, enemyHp, enemyPosition, defenseHp);
            RefreshRoleDisplay();
        }

        private void RefreshRoleDisplay()
        {
            // 担当別の表示が必要な場合だけ追加してください。
        }
    }
}
