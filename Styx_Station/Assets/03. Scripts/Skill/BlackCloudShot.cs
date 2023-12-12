using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlackCloudShot : MonoBehaviour
{
    private float timer = 0f;
    private float timeLimit = 0.5f;
    private int hitCount = 0;
    private GameObject[] monsters = new GameObject[5];

    public void SetBlackCloudShot(int h)
    {
        hitCount = h;
    }

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
            FindMonsters();
        }
    }

    private void FindMonsters()
    {
        GameObject[] monstersTemp = GameObject.FindGameObjectsWithTag("Enemy")
            .Where(obj => obj.activeSelf)
            .ToArray();
        

        if(monstersTemp.Length <= hitCount )
        {
            monsters = monstersTemp;
        }
        else
        {
            List<int> selNum = GetRandomNumbers(0, monsters.Length, hitCount);
            for(int i = 0; i < selNum.Count; i++)
            {
                monsters[i] = monstersTemp[selNum[i]];
            }
        }
    }

    private List<int> GetRandomNumbers(int min, int max, int count)
    {
        List<int> numbers = new List<int>();
        while (numbers.Count < count)
        {
            int randomNumber = Random.Range(min, max);
            if (!numbers.Contains(randomNumber))
            {
                numbers.Add(randomNumber);
            }
        }
        return numbers;
    }
}
