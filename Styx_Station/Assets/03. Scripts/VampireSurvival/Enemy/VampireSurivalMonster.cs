using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireSurivalMonster : PoolAble
{
    public int maxHp;
    public int hp;
    private float timer;
    public float findPlayerTimer;


    public void Update()
    {
        timer += Time.deltaTime;
        if(timer >=findPlayerTimer)
        {
            timer = 0;
            FindPlayer();
        }
    }
    public void FindPlayer()
    {

    }
}
