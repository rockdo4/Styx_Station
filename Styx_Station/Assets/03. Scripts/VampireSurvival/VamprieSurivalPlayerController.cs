using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class VamprieSurivalPlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movePos;
    public float moveSpeed = 3f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (VamprieSurvialUiManager.Instance.vamprieJoystick.IsDragging)
        {
            var pos = rb.velocity;
            pos += movePos * moveSpeed * Time.fixedDeltaTime;
            rb.velocity = pos;
        }
        else
        {
            rb.velocity = Vector2.zero; 
        }
    }
    private void Update()
    {
        if (VamprieSurvialUiManager.Instance.vamprieJoystick.IsDragging)
        {
            
            Debug.Log($"Vertical:  {VamprieSurvialUiManager.Instance.vamprieJoystick.GetAxis(VamprieSurvialJoystick.Axis.H)}");
            Debug.Log($"Horizon:  {VamprieSurvialUiManager.Instance.vamprieJoystick.GetAxis(VamprieSurvialJoystick.Axis.V)}");
            movePos.x = VamprieSurvialUiManager.Instance.vamprieJoystick.GetAxis(VamprieSurvialJoystick.Axis.H);
            movePos.y = VamprieSurvialUiManager.Instance.vamprieJoystick.GetAxis(VamprieSurvialJoystick.Axis.V);
        }
       
    }
}
