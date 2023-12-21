using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireSurivalPlayerAttackManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var arrow = ObjectPoolManager.instance.GetGo("VampireSurivalArrow");
            arrow.transform.position = transform.position;

            //arrow.transform.LookAt(movePos);
            //arrow.GetComponent<VamprieSurivalAttackArrow>().LineAttackRange(new Vector2(1f, 0));
            float randomAngle = Random.Range(0f, 360f);

            // Z 축을 기준으로 회전
            arrow.transform.rotation = Quaternion.Euler(0f, 0f, randomAngle);

            // 랜덤한 각도에 따른 방향 벡터 계산
            Vector2 direction = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));

            // 이후에 필요한 작업 수행
            arrow.GetComponent<VamprieSurivalAttackArrow>().LineAttackRange(direction);
        }
    }
}
