using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Money/BossRush")]
public class BossRushMoney : ScriptableObject
{
    public List<Money> Table = new List<Money>();

    [System.Serializable]
    public struct Money
    {
        public int silver;
        public int soul;
    }
}
