using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class VamprieSurivalPlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movePos;
    public float moveSpeed = 3f;
    public Animator animator;
    public TextMeshProUGUI playerExpTextMeshProUGUI;
    public TextMeshProUGUI playerLevelTextMeshProUGUI;


    public int exp = 0;
    public int expWeight = 10;
    private int maxExp;
    private int level = 0;

    public float maxHp;
    [HideInInspector]public float currentHp;
    private VampireDamageEffect vampirePlayerEffect;

    public ParticleSystem particle;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        vampirePlayerEffect= GetComponent<VampireDamageEffect>();
        PlayerLevelUp();

        currentHp = maxHp;
    }
    private void Start()
    {
       
    }
    private void Update()
    {
        if (VampireSurvivalGameManager.Instance.isPause || VampireSurvivalGameManager.Instance.isGameover)
        {
            return;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            particle.Play();
        }
        //if (VampireSurvivalGameManager.Instance.isPause)
        //{
        //    if (VampireSurvivalGameManager.Instance.isPlayerLevelup && Input.GetKeyDown(KeyCode.Return))
        //    {
        //        VampireSurvivalGameManager.Instance.isPause = false;
        //        VampireSurvivalGameManager.Instance.isPlayerLevelup = false;
        //        VamprieSurvialUiManager.Instance.playerExpSlider.value = 0f;
        //        PlayerLevelUp();
        //    }
        //    return;
        //}
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    exp++;
        //    playerExpTextMeshProUGUI.text = $"{exp} / {maxExp}";
        //    VamprieSurvialUiManager.Instance.playerExpSlider.value = (float)exp / maxExp;
        //    if (exp >= maxExp )
        //    {
        //        VampireSurvivalGameManager.Instance.isPause = true;
        //        VampireSurvivalGameManager.Instance.isPlayerLevelup= true;
        //        VamprieSurvialUiManager.Instance.JoysitckDragUp();
        //        // Panel¶ç¿ì±â
        //    }
        //}

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
        playerExpTextMeshProUGUI.text = $"{exp} / {maxExp}";
        playerLevelTextMeshProUGUI.text = $"Lv.{level}";
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("VampireEnemy"))
        {
            var monster = collision.GetComponent<VampireSurivalMonster>();
            OnCollisonMonster(monster.GetDamge());
            monster.isAttaking = true;
            monster.nowTime = Time.time;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("VampireEnemy"))
        {
            var monster = collision.GetComponent<VampireSurivalMonster>();
            if (monster.attackDelay + monster.nowTime < Time.time)
            {
                monster.nowTime = Time.time;
                OnCollisonMonster(monster.GetDamge());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("VampireEnemy"))
        {
            var monster = collision.GetComponent<VampireSurivalMonster>();
            monster.isAttaking = false;
        }
    }
    public void OnCollisonMonster(float damage)
    {
        currentHp -=damage;
        if (vampirePlayerEffect.effectCoroutine ==null)
            vampirePlayerEffect.effectCoroutine=StartCoroutine(vampirePlayerEffect.ChangeColor());
        if (currentHp <=0)
        {
            VampireSurvivalGameManager.Instance.isGameover = true;
            VamprieSurvialUiManager.Instance.PopUpGameOverObject();
            animator.SetTrigger("Die");
        }
    }
}
