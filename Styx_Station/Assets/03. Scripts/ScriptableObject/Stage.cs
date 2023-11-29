using UnityEngine;

[CreateAssetMenu(fileName = "Stage.asset", menuName = "Stage/StageBase")]
public class Stage : ScriptableObject
{
    public int index;
    public int difficulty;
    public int chapterId;
    public int stageId;
    public int waveId;
    public int preWaveId;
    public float waveTimer;
    public int monsterAttackIncrease;
    public int monsterHealthIncrease;
    public int monsterAttackSpeedIncrease;
    public MonsterTypeBase monster1;
    public int monster1Count;
    public MonsterTypeBase monster2;
    public int monster2Count;
    public bool isBossWave;
    public MonsterTypeBase bossMonster;
    public int rewardExperience;
    public int rewardCoins;
    public MoneyType rewardSpecialCurrency1;
    public int specialCurrency1Amount;
    public MoneyType rewardSpecialCurrency2;
    public int specialCurrency2Amount;
    public int rewardItem1Id; //itemTable의 index
    public int rewardItem1Amount;
    public int rewardItem2Id; //itemTable의 index
    public int rewardItem2Amount;
    public int rewardItem3Id; //itemTable의 index
    public int rewardItem3Amount;
    public int rewardItem4Id; //itemTable의 index
    public int rewardItem4Amount;
    public int linkedQuestId; 
}
