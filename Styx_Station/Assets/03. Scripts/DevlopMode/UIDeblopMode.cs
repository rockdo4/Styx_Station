using UnityEngine;

public class UIDeblopMode : MonoBehaviour
{
    private bool skill = false;
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

    public void SkillAcquire()
    {
        if (!skill)
        {
            var skills = InventorySystem.Instance.skillInventory;

            for(int i = 0; i< skills.skills.Count; ++i)
            {
                skills.skills[i].acquire = true;
            }

            skill = true;
        }
    }
}
