using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StateWindow : InventoryWindow
{
    public InventoryUI baseInventory;
    public GameObject stateText;
    private List<TextMeshProUGUI> state = new List<TextMeshProUGUI>();

    private void Awake()
    {
        foreach (Transform child in stateText.transform)
        {
            state.Add(child.gameObject.GetComponent<TextMeshProUGUI>());
        }

    }

    public override void Open()
    {
        base.Open();

        GetState();
    }

    public override void Close()
    {
        base.Close();
    }
    public void GetState()
    {
            state[1].text = "Attack : " + InventorySystem.Instance.inventory.t_Attack.ToString();
        state[2].text = "Health : " + InventorySystem.Instance.inventory.t_Health.ToString();
        state[3].text = "AttackSpeed : " + InventorySystem.Instance.inventory.t_AttackSpeed.ToString();
        state[4].text = "HealHealth : " + InventorySystem.Instance.inventory.t_HealHealth.ToString();
        state[5].text = "AttackPer : " + InventorySystem.Instance.inventory.t_AttackPer.ToString();
        state[6].text = "Evade : " + InventorySystem.Instance.inventory.t_Evade.ToString();
        state[7].text = "DamageReduction : " + InventorySystem.Instance.inventory.t_DamageReduction.ToString();
        state[8].text = "BloodSucking : " + InventorySystem.Instance.inventory.t_BloodSucking.ToString();
        state[9].text = "CoinAcquire : " + InventorySystem.Instance.inventory.t_CoinAcquire.ToString();
        state[10].text = "NormalDamage : " + InventorySystem.Instance.inventory.t_NormalDamage.ToString();
        state[11].text = "SkillDamage : " + InventorySystem.Instance.inventory.t_SkillDamage.ToString();
        state[12].text = "BossDamage : " + InventorySystem.Instance.inventory.t_BossDamage.ToString();
    }
}
