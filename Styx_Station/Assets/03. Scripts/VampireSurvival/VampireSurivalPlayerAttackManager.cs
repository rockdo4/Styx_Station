using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireSurivalPlayerAttackManager : MonoBehaviour
{
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    var arrow = ObjectPoolManager.instance.GetGo("VampireSurivalArrow");
        //    arrow.transform.position = transform.position;
           

        //    float randomAngle = Random.Range(0f, 360f);
        //    arrow.transform.rotation = Quaternion.Euler(0f, 0f, randomAngle);
        //    Vector2 direction = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
        //    arrow.GetComponent<VamprieSurivalAttackArrow>().LineAttackRange(direction);
        //    for(int i=1;i<3;++i)
        //    {
        //        var arrowLoop = ObjectPoolManager.instance.GetGo("VampireSurivalArrow");
        //        arrowLoop.transform.position = transform.position;
        //        float subAngle = 0f;
        //        if(i==1)
        //        {
        //            subAngle = randomAngle + 30f;
        //        }
        //        else if(i ==2)
        //        {
        //            subAngle = randomAngle - 30f;
        //        }
        //        arrowLoop.transform.rotation = Quaternion.Euler(0f, 0f, subAngle);
        //        Vector2 directionLoop = new Vector2(Mathf.Cos(subAngle * Mathf.Deg2Rad), Mathf.Sin(subAngle * Mathf.Deg2Rad));
        //        arrowLoop.transform.position += (Vector3)directionLoop;
        //       arrowLoop.GetComponent<VamprieSurivalAttackArrow>().LineAttackRange(directionLoop);
        //    }
        //}
    }
}
