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
    Attack,
    Health,
    AttackSpeed,
    HealingHealth,
    AttackPer,
    Evade,
    DamageReduction,
    Bloodsucking,
    CoinAcquire,
    NormalDamage,
    SkillDamage,
    BossDamage,
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
public enum InventoryType
{
    Inventory,
    Custom,
    State,
    Skill,
}

public enum MonsterType //���� spawner�� monstertypes�� �׻� ������ �� �����ؾ���. ���� ����
{
    protoRange1,
    protoMelee1,
    None
}

public enum SkillType
{
    Passive,
    Active,
}

public enum SkillType_2
{
    Normal,
    Chain,
}

public enum SkillStartPos
{
    Body,
    Air,
}