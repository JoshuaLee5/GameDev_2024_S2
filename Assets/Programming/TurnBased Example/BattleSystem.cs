using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Runtime.InteropServices;


namespace TurnBased
{
    public class BattleSystem : MonoBehaviour
    {

        [Header("Player")]
        public GameObject playerPrefab;
        public Transform playerBattleStation;
        public Unit playerUnit;
        public BattleHUB playerHUB;
        [Header("Enemy")]
        public GameObject enemyPrefab;
        public Transform enemyBattleStation;
        public Unit enemyUnit;
        public BattleHUB enemyHUB;

        public Text dialogueText;
        public BattleState battlestate;

        private void Start()
        {
            battlestate = BattleState.StartBattle;
            StartCoroutine(SetupBattle());
            //playerHUB.S
        }

        void PlayerTurn()
        {
            dialogueText.text = "Choose Action...";
        }
        public void OnAttack()
        {
            if (battlestate != BattleState.PlayerTurn)
            {
                return;
            }
            StartCoroutine(PlayerAttack());
        }
        public void OnHeal()
        {
            if (battlestate != BattleState.PlayerTurn)
            {
                return;
            }
            StartCoroutine(PlayerHeal());
        }
        void EndBattle()
        {
            if (battlestate == BattleState.Win)
            {
                dialogueText.text = $"You Won the battle by defeating {enemyUnit.unitDescription} {enemyUnit.name}";
            }
            else if (battlestate == BattleState.Lose)
            {
                dialogueText.text = $"You Lost the battle and were defeated by {enemyUnit.unitDescription} {enemyUnit.name}";
            }

        }


        IEnumerator SetupBattle()
        {
            GameObject player = Instantiate (playerPrefab, playerBattleStation);
            playerUnit = player.GetComponent<Unit>();
            GameObject enemy = Instantiate(enemyPrefab, enemyBattleStation);
            enemyUnit = enemy.GetComponent<Unit>();
            dialogueText.text = $"{enemyUnit.unitDescription} {enemyUnit.name} {enemyUnit.unitAction}...";

            yield return new WaitForSeconds(2);
        }
        IEnumerator PlayerAttack()
        {
            bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
            enemyHUB.SetHealth(enemyUnit);
            dialogueText.text = $"{playerUnit.name} attacked {enemyUnit.name}";
            yield return new WaitForSeconds(2);
            if (isDead)
            {
                battlestate = BattleState.Win;
                EndBattle();
            }
            else
            {
                battlestate = BattleState.EnemyTurn;
                StartCoroutine(EnemyTurn());

            }
        }
        IEnumerator PlayerHeal()
        {
            playerUnit.Heal(2);
            playerHUB.SetHealth(playerUnit);
            dialogueText.text = $"{playerUnit.name} feel stronger!";
            yield return new WaitForSeconds(2);
            battlestate = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }
        IEnumerator EnemyTurn()
        {
            dialogueText.text = $"{enemyUnit.name} Attacks!!!";
            yield return new WaitForSeconds(1);

            bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
            playerHUB.SetHealth(playerUnit);
            yield return new WaitForSeconds(1);
            if (isDead)
            {
                battlestate = BattleState.Lose;
                EndBattle();
            }
            else
            {
                battlestate = BattleState.PlayerTurn;
                PlayerTurn();
            }
        }
    }

    public enum BattleState
    { 
        StartBattle,
        PlayerTurn,
        EnemyTurn,
        Win,
        Lose
    }
}


