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
    Survival,
}

public enum InfoWindowType
{ 
    State,
    Inventory,
    Skill,
    Pet,
}


public enum MonsterType //���� spawner�� monstertypes�� �׻� ������ �� �����ؾ���. ���� ����
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
