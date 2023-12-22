using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDeblopMode : MonoBehaviour
{

   public void DiningRoomDecearseTime()
    {
        DiningRoomSystem.Instance.timer -= 10;
    }

    public void PlayerBuffReset()
    {
        PlayerBuff.Instance.Reset();
    }

    public void PlayerStatsReset()
    {
        SharedPlayerStats.ResetAll();
    }

    public void LabDecreaseTimer()
    {
        if(LabSystem.Instance.isResearching)
        {
            LabSystem.Instance.timerTic -= 600 * 1000;
            if(LabSystem.Instance.timerTic <2)
            {
                LabSystem.Instance.timerTic = 1;
            }
        }
    }
}
