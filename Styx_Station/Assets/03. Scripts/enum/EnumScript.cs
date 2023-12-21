public enum ItemOptionString
{
    Attack,
    Health,
}

public enum Tier
{
    Common,
    Uncommon,
    Rare,
    Unique,
    Legendry,
}

public enum Enchant
{
    Old,
    EntryLevel,
    Creation,
    Masters,
    MasterPiece,
}

public enum ItemType
{
    None = -1, //Lsw -> QuestSystemDefault
    Weapon,
    Armor,
    Ring,
    Symbol,
}

public enum AddOptionString
{
    None = -1,
    AttackPer,
    HealthPer,
    Evade,
    SkillDamage,
    BossDamage,
    CoinAcquire,
    DamageReduction,
    Bloodsucking,
}
public enum States
{
    Idle,
    Move,
    Attack,
    Die,
}

public enum AttackType
{
    Ranged, 
    Melee, 
    Tank,
    None,
}

public enum PlayerTier
{
    one,
    two,
    three,
    four,
    five,
}
public enum FoodType
{
    None =-1,
    F,
    E,
    D,
    C,
    B,
    A,
    S,
}
public enum Language
{
    KOR,
    ENG,
}

public enum AttackerType
{
    Enemy,
    Player
}

public enum PetAttackType
{
    LineAttack,
    AngleAttack,
}

public enum WindowType
{
    Info,
    DiningRoom,
    Lab,
    Cleaning,
    BossRush,
    Menu,
    Shop,
    Mission,
}

public enum InfoWindowType
{ 
    State,
    Inventory,
    Skill,
    Pet,
}


public enum MonsterType 
{
    protoRange1,
    protoMelee1,
    None
}

public enum SkillType
{
    Active,
    Passive,
}

 public enum VamprieSurivalAttackType
{ 
    LineAttackRange,
    CircleAttack,
}

public enum QuestType
{
    Nonne=-1,
    EneyDeathCount=1,
    WaveClear,
    DungeonClear,
    Gatcha,
    PlayerStatsUpgrade,

    QuestTypeClearAndWait =1000,
}

public enum DungeonType
{
    None = -1,
    SweepHomeBaseAndClean=1,
    BreakDownTrain,
}

public enum GatchaType
{
    None=-1,
    Weapon = 1,
    Skill,
    Pet,
}

public enum UpgradeType
{
    None=-1,
    Power,
    PowerBoost,
    AttackSpeed,
    Critical,
    CriticalPower,
    MonsterDamage,
    MaxHp,
    Healing,
    Weapon,
    Skill
}

public enum RewardType
{
    None =-1,
    Sliver,
    StyxPomegranate,
    SoulStone,
}

public enum LabType
{
    LabPower1,
    LabHp1,
    LabCriticalPower,
    LabSliverUp,
    LabPower2,
    LabHp2,
    None =100,
}