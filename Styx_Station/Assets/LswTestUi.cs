using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LswTestUi : MonoBehaviour
{
    public GameObject button;
    public GameObject inventory;
    public GameObject inventoryClose;

    public GameObject panel;
    public GameObject panel2;
    public GameObject playerData;

    public List<TextMeshProUGUI> playerStats = new List<TextMeshProUGUI>();

    public void DrawInventory()
    {
        panel2.SetActive(true);
        inventoryClose.SetActive(true);
        panel.SetActive(false);
        button.SetActive(false);
        inventory.SetActive(false);
        playerData.SetActive(false);
    }

    public void CloseInventory()
    {
        panel2.SetActive(false);
        inventoryClose.SetActive(false);
        panel.SetActive(false);
        button.SetActive(true);
        inventory.SetActive(true);
        playerData.SetActive(true);
    }
    public void DrawPanel()
    {
        panel.SetActive(true);
        button.SetActive (false);
        inventory.SetActive(false);
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
        button.SetActive(true);
        inventory.SetActive(true);
    }

    private void Start()
    {
        PlayerStatsStringUpdate();
    }


    public void PlayerStatsStringUpdate()
    {
        playerStats[0].text = $"공격력 : {SharedPlayerStats.GetPlayerPower()}" ;
        playerStats[1].text = $"공격력 증폭: {SharedPlayerStats.GetPlayerPowerBoost()}";
        playerStats[2].text = $"공격 속도 : {SharedPlayerStats.GetPlayerAttackSpeed()}";
        playerStats[3].text = $"치명타 확률 : {SharedPlayerStats.GetAttackCritical()}";
        playerStats[4].text = $"치명타 피해 : {SharedPlayerStats.GetAttackCriticlaPower()}";
        playerStats[5].text = $"몬스터 피해 : {SharedPlayerStats.GetMonsterDamagePower()}";
        playerStats[6].text = $"체력 : {SharedPlayerStats.GetHp()}";
        playerStats[7].text = $"체력 회복 :{SharedPlayerStats.GetHealing()}";
    }
}
