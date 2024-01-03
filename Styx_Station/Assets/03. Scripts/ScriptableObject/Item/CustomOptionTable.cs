using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/CustomOptionTable")]
public class CustomOptionTable : ScriptableObject
{
    public List<CustomOption> table = new List<CustomOption>();

    [System.Serializable]
    public class Option
    {
        public AddOptionString optionName;
        public float value;
    }

    public Option GetPickCustom(string name)
    {
        var custom = table.Where(x => x.name == name).FirstOrDefault();

        if (custom == null)
            return null;

        var pick = custom.GetOption();

        var option = new Option();

        float value = Random.Range(pick.minValue, pick.maxValue);

        option.optionName = pick.option;
        option.value = value;

        return option;
    }
}
