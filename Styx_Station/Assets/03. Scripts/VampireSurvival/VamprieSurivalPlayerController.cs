using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class VamprieSurivalPlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movePos;
    public float moveSpeed = 3f;

    public List<VamprieSurivalPlayerAttackManager> playerAttackType;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        
    }
    private void Update()
    {
        if (VamprieSurvialUiManager.Instance.vamprieJoystick.IsDragging)
        {
            PlayerMove();
        }
        else
        {
            movePos = Vector2.zero; 
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (playerAttackType[0].nowTime + playerAttackType[0].coolTime < Time.time)
            {
                var t = Instantiate(playerAttackType[0],transform.position,Quaternion.identity);
                t.transform.localPosition = transform.localPosition;
                var attackPos = (Vector3)movePos;
                if (attackPos.x < 0)
                {
                    t.gameObject.transform.rotation = Quaternion.Euler(0, -180, 0);
                }
                else if (attackPos.x > 0)
                {
                    t.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (attackPos.x == 0f)
                {
                    attackPos.x = 1f;
                }
                if (attackPos.y < 0)
                {
                    t.gameObject.transform.rotation = Quaternion.Euler(0, 0, 270);
                }
                else if (attackPos.y > 0)
                {
                    t.gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
                }
                else if (attackPos.y == 0f)
                {
                    attackPos.y = 1f;
                }
                var tt = t.GetComponent<VamprieSurivalPlayerAttackManager>();
                tt.LineAttackRange(attackPos);
            }
        }
        //for(int i =0; i<playerAttackType.Count; i++)
        //{
        //    if(playerAttackType[i].nowTime + playerAttackType[i].coolTime <Time.time)
        //    {
        //        var t = Instantiate(playerAttackType[i]);
        //        var tt = t.GetComponent<VamprieSurivalPlayerAttackManager>();
        //        tt.LineAttackRange((Vector3)movePos);

        //    }
        //}
    }

    private void PlayerMove()
    {
        movePos.x = VamprieSurvialUiManager.Instance.vamprieJoystick.GetAxis(VamprieSurvialJoystick.Axis.H);
        if(movePos.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else if (movePos.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        movePos.y = VamprieSurvialUiManager.Instance.vamprieJoystick.GetAxis(VamprieSurvialJoystick.Axis.V);
        var pos = rb.position;
        pos += movePos * moveSpeed * Time.deltaTime;
        rb.MovePosition(pos);
    }
}
