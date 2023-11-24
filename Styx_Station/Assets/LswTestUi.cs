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
        playerStats[0].text = $"���ݷ� : {SharedPlayerStats.GetPlayerPower()}" ;
        playerStats[1].text = $"���ݷ� ����: {SharedPlayerStats.GetPlayerPowerBoost()}";
        playerStats[2].text = $"���� �ӵ� : {SharedPlayerStats.GetPlayerAttackSpeed()}";
        playerStats[3].text = $"ġ��Ÿ Ȯ�� : {SharedPlayerStats.GetAttackCritical()}";
        playerStats[4].text = $"ġ��Ÿ ���� : {SharedPlayerStats.GetAttackCriticlaPower()}";
        playerStats[5].text = $"���� ���� : {SharedPlayerStats.GetMonsterDamagePower()}";
        playerStats[6].text = $"ü�� : {SharedPlayerStats.GetHp()}";
        playerStats[7].text = $"ü�� ȸ�� :{SharedPlayerStats.GetHealing()}";
    }
}
