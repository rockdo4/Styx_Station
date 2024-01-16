using UnityEngine;

public class MonsterAttackedTakeDamage : MonoBehaviour, IAttackable
{
    private MonsterStats stats;
    private MonsterController controller;
    private void OnEnable()
    {
        stats = GetComponent<MonsterStats>();
        controller = GetComponent<MonsterController>();
    }
    public void OnAttack(GameObject attacker, Attack attack)
    {
        if(!WaveManager.Instance.isWaveInProgress)
        {
            return;
        }
        stats.currHealth -= attack.Damage;
        if (stats.currHealth <= 0)
        {
            stats.currHealth = 0;
            controller.skullImage.SetActive(false);
            controller.lightningImage.SetActive(false);
            WaveManager.Instance.DecreaseAliveMonsterCount();
            CurrencyManager.GetSilver(controller.coin, 0);
            CurrencyManager.GetSilver(controller.pomegranate, 1);
            UIManager.Instance.questSystemUi.DeathEnemyCounting();
            UIManager.Instance.PrintSliverMoney();
            UIManager.Instance.PrintPommeMoney();
        }
    }
}
