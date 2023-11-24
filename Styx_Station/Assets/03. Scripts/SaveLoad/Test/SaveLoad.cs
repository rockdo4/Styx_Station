using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SaveDataVersionCurrent = SaveDataV1;
using System.Numerics;

public class SaveLoad : MonoBehaviour
{
    private void Awake()
    {
        Load();
    }
    public void Save()
    {
        SaveDataVersionCurrent data = new SaveDataVersionCurrent();

        data.playerdata.playerPower = SharedPlayerStats.GetPlayerPower();
        data.playerdata.playerPowerboost = SharedPlayerStats.GetPlayerPowerBoost();
        data.playerdata.playerAttackSpeed = SharedPlayerStats.GetPlayerAttackSpeed();
        data.playerdata.critical=SharedPlayerStats.GetAttackCritical();
        data.playerdata.criticalPower =SharedPlayerStats.GetAttackCriticlaPower();
        data.playerdata.monsterDamage = SharedPlayerStats.GetMonsterDamagePower();
        data.playerdata.maxHp = SharedPlayerStats.GetHp();
        data.playerdata.healing = SharedPlayerStats.GetHealing();
        data.playerdata.money1 = SharedPlayerStats.money1.ToString();
        data.playerdata.money2 = SharedPlayerStats.money2.ToString();
        data.playerdata.money3 = SharedPlayerStats.money3.ToString();

        SaveLoadSystem.JsonSave(data, "Test.json");
        Debug.Log("Save ");
    }

    public void Load()
    {

        var path = Path.Combine(Application.persistentDataPath, "Test.json");
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);

            JObject jsonObject = JObject.Parse(json);
            string dataString = jsonObject["playerdata"].ToString();
            var data = JsonConvert.DeserializeObject<PlayerData>(dataString);
            SharedPlayerStats.PlayerPower = data.playerPower;
            SharedPlayerStats.PlayerPowerBoost = data.playerPowerboost;
            SharedPlayerStats.PlayerAttackSpeed = data.playerAttackSpeed;
            SharedPlayerStats.Critical = data.critical;
            SharedPlayerStats.CriticalPower = data.criticalPower;
            SharedPlayerStats.MonsterDamage = data.monsterDamage;
            SharedPlayerStats.MaxHp = data.maxHp;
            SharedPlayerStats.Healing = data.healing;

            SharedPlayerStats.money1 = BigInteger.Parse(data.money1);
            SharedPlayerStats.money2 = BigInteger.Parse(data.money2);
            SharedPlayerStats.money3 = BigInteger.Parse(data.money3);
        }
    }

}