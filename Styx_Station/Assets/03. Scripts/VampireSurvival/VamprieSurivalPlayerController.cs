using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class VamprieSurivalPlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movePos;
    public float moveSpeed = 3f;
    public Animator animator;

    public List<VamprieSurivalPlayerAttackManager> playerAttackType;

    public int exp = 0;
    public int expWeight = 10;
    public int maxExp;
    private int level = 0;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerLevelUp();
    }
    private void FixedUpdate()
    {
        
    }
    private void Update()
    {
        if (VamprieSurvialUiManager.Instance.isPause)
        {
            if (VamprieSurvialUiManager.Instance.isPlayerLevelup && Input.GetKeyDown(KeyCode.Return))
            {
                VamprieSurvialUiManager.Instance.isPause = false;
                VamprieSurvialUiManager.Instance.isPlayerLevelup = false;
                PlayerLevelUp();
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            exp++;
            if(exp >= maxExp )
            {
                VamprieSurvialUiManager.Instance.isPause = true;
                VamprieSurvialUiManager.Instance.isPlayerLevelup= true;
                VamprieSurvialUiManager.Instance.vamprieJoystick.gameObject.SetActive(false);
                // Panel¶ç¿ì±â
            }
        }
        if (VamprieSurvialUiManager.Instance.vamprieJoystick.IsDragging)
        {
            PlayerMove();
        }
        else
        {
            movePos = Vector2.zero;
            PlayerAnimationSetting(movePos.magnitude);
        }
    }

    private void PlayerMove()
    {
        movePos.x = VamprieSurvialUiManager.Instance.vamprieJoystick.GetAxis(VamprieSurvialJoystick.Axis.H);
        if(movePos.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (movePos.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        movePos.y = VamprieSurvialUiManager.Instance.vamprieJoystick.GetAxis(VamprieSurvialJoystick.Axis.V);
        var pos = rb.position;
        pos += movePos * moveSpeed * Time.deltaTime;
        rb.MovePosition(pos);

        PlayerAnimationSetting(movePos.magnitude);
    }

    private void PlayerAnimationSetting(float value)
    {
        animator.SetFloat("RunState", value);
    }

    private void PlayerLevelUp()
    {
        exp = 0;
        level++;
        maxExp = expWeight * (int)Math.Pow(2, (level - 1));
    }
}
