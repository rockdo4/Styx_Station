using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBox : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    void Awake()
    {
        float fixedAspectRatio = 1920f / 1080f;
        float currentAspectRatio = (float)Screen.width / (float)Screen.height;
        if (currentAspectRatio == fixedAspectRatio)
        {
            cam.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
            return;
        }
        else if (currentAspectRatio > fixedAspectRatio)
        {
            float w = fixedAspectRatio / currentAspectRatio;
            float x = (1 - w) / 2;
            cam.rect = new Rect(x, 0.0f, w, 1.0f);
        }
        else if (currentAspectRatio < fixedAspectRatio)
        {
            float h = currentAspectRatio / fixedAspectRatio;
            float y = (1 - h) / 2;
            cam.rect = new Rect(0.0f, y, 1.0f, h);
        }
    }
}
