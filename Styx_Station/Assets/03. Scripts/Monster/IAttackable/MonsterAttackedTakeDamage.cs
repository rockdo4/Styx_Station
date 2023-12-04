using UnityEngine;

public class MonsterAttackedTakeDamage : MonoBehaviour, IAttackable
{
    private MonsterStats stats;
    private void OnEnable()
    {
        stats = GetComponent<MonsterStats>();
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
            WaveManager.instance.DecreaseAliveMonsterCount();
            WaveManager.instance.IncreaseMoney1();
            //UIManager.instance.ReSetText();
            //PlayerStatsUpgardeUI.Instance.ResetStringMoney();
        }
    }
}
