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

            // Z ���� �������� ȸ��
            arrow.transform.rotation = Quaternion.Euler(0f, 0f, randomAngle);

            // ������ ������ ���� ���� ���� ���
            Vector2 direction = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));

            // ���Ŀ� �ʿ��� �۾� ����
            arrow.GetComponent<VamprieSurivalAttackArrow>().LineAttackRange(direction);
        }
    }
}
