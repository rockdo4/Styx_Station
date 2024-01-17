using System.Collections.Generic;
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
    public float monsterAttackSpeedIncrease;
    //public MonsterTypeBase monster1;
    //public int monster1Count;
    //public MonsterTypeBase monster2;
    //public int monster2Count;
    //public MonsterTypeBase monster3;
    //public int monster3Count;
    //public MonsterTypeBase monster4;
    //public int monster4Count;

    public List<MonsterTypeBase> monsterList;
    public List<int> monsterCountList;

    public bool isBossWave;
    public MonsterTypeBase bossMonster;
    public int rewardCoins;
    public int linkedQuestId; 
}
