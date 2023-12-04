using System.Collections.Generic;
using UnityEngine;
using static Item;

[CreateAssetMenu(menuName = "Items/CustomOption")]
public class CustomOption : ScriptableObject
{
    [Tooltip("커스텀 테이블")]
    public List<AddOption> addOptions = new List<AddOption>();

    [System.Serializable]
    public class AddOption
    {
        [Tooltip("확률")]
        public float weight;
        [Tooltip("추가 옵션")]
        public AddOptionString option;
        [Tooltip("최소 증가량")]
        public float minValue;
        [Tooltip("최대 증가량")]
        public float maxValue;
    }

    protected AddOption PickOption()
    {
        if(addOptions == null)
            return null;

        if(addOptions.Count <=0)
            return null;

        float sum = 0;
        foreach(var option in addOptions)
        {
            sum += option.weight;
        }

        if (sum <= 0)
            return null;

        if (sum > 1)
            return null;

        var random = Random.Range(0, sum);

        for(int i = 0; i < addOptions.Count; ++i)
        {
            var radomOption = addOptions[i];
            if(radomOption.weight>random)
                return radomOption;

            else 
                random -= radomOption.weight;
        }

        return null;
    }

    public AddOption GetOption()
    {
        var option = new AddOption();
        option.option = AddOptionString.None;
        option.weight = 0f;

        var pickOption = PickOption();

        if (pickOption != null)
            option = pickOption;

        return option;
    }
}
