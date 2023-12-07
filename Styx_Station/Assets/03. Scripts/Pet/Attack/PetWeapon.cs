using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Attack/PetWeaPon")]
public class PetWeapon : AttackDefinition
{
    
    public GameObject weaponObject;
    [SerializeField]
    private string scriptName = "PetWeaopn";
    public AttackType type;
    [Tooltip("This is flightSpeed")]
    public float speed =1.5f;

    public override void ExecuteAttack(GameObject attacker, GameObject defender)
    {
        if (attacker != null)
        {
            var excuteAttackpet = attacker.GetComponentInChildren<ExcuteAttackPet>();
            var startPos = excuteAttackpet.petFirePos;
            if(startPos == null ) 
            {
                string currentTime = DateTime.Now.ToString("MM�� dd�� HH�� mm�� ss��");
                string log = $"{currentTime} : Pet�� ������ġ�� Null�Դϴ�.. / script : PetWeapon + 26 ��°��";
                Debug.Log(log);
                Log.Instance.MakeLogText(log);
                return;
            }
            var bow = ObjectPoolManager.instance.GetGo(weaponObject.name);
            if(bow == null) 
            {
                string currentTime = DateTime.Now.ToString("MM�� dd�� HH�� mm�� ss��");
                string log = $"{currentTime} : Pet�� ������Ʈ Ǯ�� Null�Դϴ�. / script : PetWeapon + 31 ��°��";
                Log.Instance.MakeLogText(log);
                return; 
            }
            bow.transform.position = startPos.position;
            var petWeapon = bow.GetComponent<PetBow>();
            if(petWeapon == null)
            {
                string currentTime = DateTime.Now.ToString("MM�� dd�� HH�� mm�� ss��");
                string log = $"{currentTime} : Pet�� PetBow��ũ��Ʈ�� Null�Դϴ�. / script : PetWeapon + 40 ��°��";
                Log.Instance.MakeLogText(log);
                return;
            }
            if (!petWeapon.CheckOnCollided())
            {
                petWeapon.OnCollided += OnBowCollided;
            }
            petWeapon.SetPetBow(attacker, speed, defender);
        }
    }

    private void OnBowCollided(GameObject attacker, GameObject defender)
    {
        if (defender == null) 
        {
            return;
        }

        var attackerStats = attacker.GetComponent<PetController>();
        var target = defender.GetComponent<MonsterStats>();
        Attack attack = CreateAttackToMonster(attackerStats, target);

        var attackables = defender.GetComponents<IAttackable>();
        foreach (var attackable in attackables)
        {
            attackable.OnAttack(attacker, attack);
        }
    }
}
