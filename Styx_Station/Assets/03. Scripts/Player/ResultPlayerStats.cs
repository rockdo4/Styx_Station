using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResultPlayerStats : MonoBehaviour
{

    private PlayerAttributes playerAttribute;

    private int weaponPower = 1; // �κ��丮�� ������
    private float weaponBuff =1f; // �κ��丮�� ������

    private Dictionary<string, int> attackPowerTabel = new Dictionary<string, int>(); 
    //�������� ������ ����


    private float normalMonsterDamage;
    private float skillMonsterDamage;

    private void Awake()
    {
        playerAttribute = GetComponent<PlayerAttributes>();
        attackPowerTabel.Add("None", 0);
        attackPowerTabel.Add("A1", 0);
        attackPowerTabel.Add("B1", 0);
        attackPowerTabel.Add("C1", 0);
    }

    private int GetPlayerPower() // player ���ݷ� ��� 
    // ���� (�⺻���� ���ݷ� + ���׷��̵���ݷ�  +��� ���ݷ� ) * ������ 
    // ���׷��̵� ���ܰ��� ��ü ���ݷ��� 1000�����϶��� 10���� , �̻��϶��� A B C  ���� * 0.01 
    // ��ü������ * 10���� ������ �ϴ� ���� 

    {
        //int power = (int)((playerAttribute.attackPower + (attackPowerTabel["None"] * 10) +
        //      (attackPowerTabel["A1"] * UnitConverter.A1 * 0.01) + (attackPowerTabel["B1"] * UnitConverter.B1 * 0.01) +
        //      (attackPowerTabel["C1"] * UnitConverter.C1 * 0.01) + weaponPower) * weaponBuff);
        //var playerPower = SharedPlayerStats.GetPlayerPower() - 1;
        //if (power < UnitConverter.A1)
        //{
        //    attackPowerTabel["None"] = playerPower;
        //    return power;
        //}
        //else if (power > UnitConverter.A1 && power < UnitConverter.B1)
        //{
        //    attackPowerTabel["A1"] = playerPower - attackPowerTabel["None"]-1;
        //    return (int)((playerAttribute.attackPower + (attackPowerTabel["None"] * 10) +
        //        (attackPowerTabel["A1"] * UnitConverter.A1 * 0.01) + weaponPower) * weaponBuff);
        //}
        //else if (power > UnitConverter.B1 && power < UnitConverter.C1)
        //{
        //    attackPowerTabel["B1"] = playerPower - attackPowerTabel["None"] - attackPowerTabel["A1"];
        //    return (int)((playerAttribute.attackPower + (attackPowerTabel["None"] * 10) +
        //      (attackPowerTabel["A1"] * UnitConverter.A1 * 0.01) + (attackPowerTabel["B1"] * UnitConverter.B1 * 0.01) +
        //      weaponPower) * weaponBuff);
        //}
        //else if (power > UnitConverter.C1 && power < UnitConverter.D1)
        //{
        //    attackPowerTabel["B1"] = playerPower - attackPowerTabel["None"] - attackPowerTabel["A1"];
        //    return (int)((playerAttribute.attackPower + (attackPowerTabel["None"] * 10) +
        //      (attackPowerTabel["A1"] * UnitConverter.A1 * 0.01) + (attackPowerTabel["B1"] * UnitConverter.B1 * 0.01) +
        //      (attackPowerTabel["C1"] * UnitConverter.C1 * 0.01)+weaponPower) * weaponBuff);
        //}
        // ���� ����ġ ����

        // �Ʒ��� ����ġ ������ ���� ����

        // + �� *�� ���� �ɼ� ����
        return (int)((playerAttribute.attackPower +((SharedPlayerStats.GetPlayerPower()-1) * 10) + weaponPower) * weaponBuff);
    }

    private float GetPowerBoost()
        //���ݷ�����
    {
        float upgrad = 0.1f;
        var boost = (SharedPlayerStats.GetPlayerPowerBoost() -1)* upgrad;
        return boost;
    }

    private float GetCritclaPower()
    {
        return (SharedPlayerStats.GetAttackCriticlaPower() - 1) * 0.1f + 1.5f;
    }

    private float GetMonsterDamage()
    {
        return (SharedPlayerStats.GetMonsterDamagePower() - 1) * 0.1f;
    }

    //private float GetMonsterDamage()
    //    // �Ϲ� ���� ���ط� ��� 
    //{
    //    var damage = GetPlayerPower();
    //    return damage + (damage * ((SharedPlayerStats.GetMonsterDamagePower()-1) / 100));
    //}


    private void GetNormalDamage()
    {
        var power = GetPlayerPower();
        normalMonsterDamage = power +(power * GetPowerBoost()) * GetMonsterDamage();
    }
    private void GetNoramlCriticalDamage()
    {
        var power = GetPlayerPower();
        normalMonsterDamage = power + (power * GetPowerBoost())*
            GetCritclaPower() * GetMonsterDamage();
    }


    public float ResultMonsterNormalDamage(bool isCritical, float monsterDefense) // ���� ��� ���� ������ ���
    {
        // ũ��Ƽ�� ���� : isCritical, ���Ͱ� �޴� ���� ���� : monsterDefense
        if (isCritical)
        {
            GetNoramlCriticalDamage();
        }
        else
        {
            GetNormalDamage();
        }
        //Debug.Log(normalMonsterDamage);
        return normalMonsterDamage - (normalMonsterDamage * monsterDefense);
    }

    // ���� �Ϲ� ������

    private void GetSkillDamage(float skillCount)
        //skillCount ��ų �����
    {
        var power = GetPlayerPower();
        skillMonsterDamage = power + (power * GetPowerBoost()) * skillCount
            * GetMonsterDamage();
    }


    private void GetSkillCriticalDamage(float skillCount, float skillPower)
    {
        var power = GetPlayerPower();
        skillMonsterDamage = (power + (power * GetPowerBoost()) * skillCount
            * GetCritclaPower() * 0.1f + 1.5f) * (GetMonsterDamage() + skillPower);
    }

    public float ResultMonsterSkillDamage(bool isCritical, float monsterDefense, float a, float t)
    // a �� ��ų ��� t�� ��ų���ط� �ӽ� �� 
    {
        if (isCritical)
        {
            GetSkillCriticalDamage(a, t);
        }
        else
        {
            GetSkillDamage(a);
        }
        return skillMonsterDamage - (skillMonsterDamage * (1 - monsterDefense));
    }

    // �������ط��� ���� �Ϲ� ���� ���ط��� ���� �ص� ���� ������ �������� ����

    public float GetCritical()
    {
        return (SharedPlayerStats.GetAttackCritical() - 1) * 0.1f;
    }
}