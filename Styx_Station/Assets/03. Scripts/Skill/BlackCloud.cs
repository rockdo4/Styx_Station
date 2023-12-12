using UnityEngine;

public class BlackCloud : SkillBase
{
    private SkillInventory.InventorySKill blackCloud;
    private GameObject blackCloudPrefab;
    private float damageMultiplier;

    public BlackCloud(SkillInventory.InventorySKill skill, GameObject blackCloudPrefab)
    {
        blackCloud = skill;
        this.blackCloudPrefab = blackCloudPrefab;

        damageMultiplier = blackCloud.skill.Skill_ATK;
    }
    public override void UseSkill(GameObject attacker)
    {
        var blackCloudObj = ObjectPoolManager.instance.GetGo(blackCloudPrefab.name);
        //var tornado = Object.Instantiate(tornadoShotPrefab);
        if (blackCloud == null)
        {
            Debug.Log("ERR: blackCloud is null");
            return;
        }
        blackCloudObj.GetComponent<BlackCloudShot>().SetBlackCloudShot(blackCloud.skill.Skill_ATK_NUM);
    }
}
