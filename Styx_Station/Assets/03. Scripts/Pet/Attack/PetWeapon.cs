using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Attack/PetWeaPon")]
public class PetWeapon : AttackDefinition
{
    public enum AttackType
    {
        [Tooltip("일직선공격")]
        LineAttack,
        [Tooltip("곡선공격")]
        AngleAttack,
    }
    public GameObject weaponObject;
    [SerializeField]
    private string scriptName = "PetWeaopn";
    public AttackType type;

    public override void ExecuteAttack(GameObject attacker, GameObject defender)
    {
        if (attacker != null)
        {
            var excuteAttackpet = attacker.GetComponentInChildren<ExcuteAttackPet>();
            var startPos = excuteAttackpet.petFirePos;
            var bow = ObjectPoolManager.instance.GetGo(weaponObject.name);
            if(bow == null) 
            {
                string currentTime = DateTime.Now.ToString("MM월 dd일 HH시 mm분 ss초");
                string log = $"{currentTime} : Pet의 오브젝트 풀이 Null입니다. / script : PetWeapon + 31 번째줄";
                Log.Instance.MakeLogText(log);
                return; 
            }
            bow.transform.position = startPos.position;
            var petWeapon = bow.GetComponent<PlayerBow>();
            if(petWeapon == null)
            {
                string currentTime = DateTime.Now.ToString("MM월 dd일 HH시 mm분 ss초");
                string log = $"{currentTime} : Pet의 PlayerBow스크립트가 Null입니다. / script : PetWeapon + 40번째줄";
                Log.Instance.MakeLogText(log);
                return;
            }
        }

        //var playerBow = bow.GetComponent<PlayerBow>();
        //if (!playerBow.CheckOnCollided())
        //{
        //    playerBow.OnCollided += OnBowCollided;
        //}
        //playerBow.Fire(attacker, speed, targetPos);
    }

    private void OnBowCollided(GameObject attacker, GameObject defender)
    {
       
        if (defender == null) 
        {
            return;
        }

        //var attackerStats = attacker.GetComponent<ResultPlayerStats>();
        //var target = defender.GetComponent<MonsterStats>();
        //Attack attack = CreateAttackToMonster(attackerStats, target);

        //var attackables = defender.GetComponents<IAttackable>();
        //foreach (var attackable in attackables)
        //{
        //    attackable.OnAttack(attacker, attack);
        //}
    }
}
