using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private float duration = 1.0f;
    private float speed = 1.0f;
    //private Color color = Color.white;

    private TextMeshPro textMesh;
    private float timer = 0f;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    public void Set(string text, Color color)
    {
        textMesh.text = text;
        textMesh.color = color;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        textMesh.alpha = 1f - (timer / duration);

        transform.position += Vector3.up * speed * Time.deltaTime;

        if (timer > duration)
        {
            Destroy(gameObject);
        }
    }
}
