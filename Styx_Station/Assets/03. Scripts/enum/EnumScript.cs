public enum ItemOptionString
{
    Attack,
    Health,
}

public enum ItemTier
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
}

public enum MonsterType //몬스터 spawner의 monstertypes와 항상 동일한 값 유지해야함. 추후 수정
{
    protoRange1,
    protoMelee1,
    None
}