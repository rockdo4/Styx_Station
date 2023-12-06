using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private GameObject attacker;
    private Vector2 pos;
    private string piercingBowName;
    private float speed;
    private int fireCount = 3;
    private float fireBet = 0.3f;

    private WaitForSeconds wait;

    private bool isShootAll = false;

    private void Start()
    {
        wait = new WaitForSeconds(fireBet);

        StartCoroutine(Fire());
    }

    private void Update()
    {
        if (isShootAll)
            Destroy(this);
    }

    public void SetShooter(GameObject a, Vector2 b, string s, float sp)
    {
        attacker = a;
        pos = b;
        piercingBowName = s;
        speed = sp;
    }

    private IEnumerator Fire()
    {
        while (fireCount > 0)
        {
            var arrow = ObjectPoolManager.instance.GetGo(piercingBowName);
            if (arrow == null)
            {
                Debug.Log("ERR: arrow is null");
                yield return null;
            }
            arrow.transform.position = pos;

            var piercingArrow = arrow.GetComponent<PiercingArrow>();
            if (!piercingArrow.CheckOnCollided())
            {
                piercingArrow.OnCollided += OnBowCollided;
            }

            piercingArrow.Fire(attacker, speed);

            fireCount--;

            yield return wait;
        }
        isShootAll = true;
    }

    private void OnBowCollided(GameObject attacker, GameObject defender)
    {
        //애니메이션 시작 -> 실제 타격 사이에 거리와 방향이 변경될 수 있기 때문에 체크 필요
        if (defender == null) //남아있지만 죽은 상태일 수도 있음
        {
            return;
        }
        var attackerStats = attacker.GetComponent<ResultPlayerStats>();
        var target = defender.GetComponent<MonsterStats>();
        Attack attack = CreateAttackToMonster(attackerStats, target);

        //Iattackable을 상속 받은 것이 여러개 일 수도 있기 때문에 모두 호출
        var attackables = defender.GetComponents<IAttackable>();
        foreach (var attackable in attackables)
        {
            attackable.OnAttack(attacker, attack);
        }
    }

    public Attack CreateAttackToMonster(ResultPlayerStats attacker, MonsterStats defender)
    {
        float criticalChance = attacker.GetCritical();   //0~100까지 치확
        float randomFloat = Random.Range(0f, 100f);
        randomFloat = Mathf.Round(randomFloat * 10f) / 10f;
        bool isCritical = randomFloat <= criticalChance;

        var currentDamage = attacker.ResultMonsterNormalDamage(isCritical, 0);

        return new Attack(currentDamage, false);
    }
}
