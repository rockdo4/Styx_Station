using System.Linq;
using UnityEngine;

public class BlackCloudShot : MonoBehaviour
{
    private float timer = 0f;
    private float timeLimit = 0.5f;

    private void OnEnable()
    {
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= timeLimit)
        {
            timer = 0f;
            var monsters =  FindMonsters();
        }
    }

    private GameObject[] FindMonsters()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy")
            .Where(obj => obj.activeSelf)
            .ToArray();

        return monsters;
    }
}
