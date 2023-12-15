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
        //Debug.Log($"Damage: {attack.Damage}");
        stats.currHealth -= attack.Damage;
        //Debug.Log($"Health: {stats.currHealth}");
        //Debug.Log($"OnAttack: {attack.Damage}");
        if (stats.currHealth <= 0)
        {
            stats.currHealth = 0;
            controller.skullImage.SetActive(false);
            controller.lightningImage.SetActive(false);
            WaveManager.Instance.DecreaseAliveMonsterCount();
            CurrencyManager.GetSilver(controller.coin, 0);
            CurrencyManager.GetSilver(controller.pomegranate, 1);
            //WaveManager.Instance.IncreaseMoney1();
            //UIManager.instance.ReSetText();
            //PlayerStatsUpgardeUI.Instance.ResetStringMoney();
        }
    }
}
