using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GachaInfo : MonoBehaviour
{
    private ItemGacha item;
    private SkillGacha skill;
    private PetGacha pet;
    private ShopSystem shop;
    public enum GachaType
    {
        None = -1,
        Item,
        Skill,
        Crew,
    }

    public GachaType type;

    public int value;

    public GameObject[] slots;

    public Button reGacha;

    public Button Exit;

    public TextMeshProUGUI valueText;

    public void First(ItemGacha itemGacha, SkillGacha skillGacha, PetGacha petGacha)
    {
        item = itemGacha;
        skill = skillGacha;
        pet = petGacha;
        shop = ShopSystem.Instance;
    }
    public void InfoUpdate(int buttonIndex, int typeIndex)
    {
        value = buttonIndex;
        type = (GachaType)typeIndex;

        TextUpdate();
    }

    private void TextUpdate()
    {
        switch (type)
        {
            case GachaType.Item:
                ItemText();
                break;
            case GachaType.Skill:
                SkillText();
                break;
            case GachaType.Crew:
                PetText();
                break;
        }
    }

    private void ItemText()
    {
        switch(value)
        {
            case 0:
                valueText.text = $"{item.minValue}";
                reGacha.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{item.minGacha} Gacha";
                break;

                case 1:
                valueText.text = $"{item.middleValue}";
                reGacha.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{item.middleGach} Gacha";
                break;

                case 2:
                valueText.text = $"{item.maxValue}";
                reGacha.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{item.maxGacha} Gacha";
                break;
        }
    }

    private void SkillText()
    {
        switch (value)
        {
            case 0:
                valueText.text = $"{skill.minValue}";
                reGacha.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{skill.minGacha} Gacha";
                break;

            case 1:
                valueText.text = $"{skill.middleValue}";
                reGacha.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{skill.middleGach} Gacha";
                break;

            case 2:
                valueText.text = $"{skill.maxValue}";
                reGacha.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{skill.maxGacha} Gacha";
                break;
        }
    }

    private void PetText()
    {
        switch (value)
        {
            case 0:
                valueText.text = $"{pet.minValue}";
                reGacha.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{pet.minGacha} Gacha";
                break;

            case 1:
                valueText.text = $"{pet.middleValue}";
                reGacha.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{pet.middleGach} Gacha";
                break;

            case 2:
                valueText.text = $"{pet.maxValue}";
                reGacha.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{pet.maxGacha} Gacha";
                break;
        }
    }

    public void OnClickReGacha()
    {
        foreach(var slot in  slots)
        {
            slot.SetActive(false);
        }

        switch(type)
        {
            case GachaType.None:
                break;
            case GachaType.Item:
                ItemGacha();
                break;
            case GachaType.Skill:
                SkillGacha();
                break;
            case GachaType.Crew:
                PetGacha();
                break;
        }
    }

    public void ItemGacha()
    {
        switch(value)
        {
            case 0:

                if (CurrencyManager.money3 < item.minValue)
                {
                    OnClickGachaInfoClose();
                    return;
                }
                CurrencyManager.money3 -= item.minValue;
                shop.ItemGacha(this, item.minGacha);
                break;
            case 1:
                if (CurrencyManager.money3 < item.middleValue)
                {
                    OnClickGachaInfoClose();
                    return;
                }

                CurrencyManager.money3 -= item.middleValue;
                shop.ItemGacha(this, item.middleGach);
                break;
            case 2:
                if (CurrencyManager.money3 < item.maxValue)
                {
                    OnClickGachaInfoClose();
                    return;
                }

                CurrencyManager.money3 -= item.maxValue;
                shop.ItemGacha(this,item.maxGacha);
                break;
        }
    }

    public void SkillGacha()
    {
        switch (value)
        {
            case 0:
                if (CurrencyManager.money3 < skill.minValue)
                {
                    OnClickGachaInfoClose();
                    return;
                }

                CurrencyManager.money3 -= skill.minValue;
                shop.SkillGacha(this, skill.minGacha);
                break;
            case 1:
                if (CurrencyManager.money3 < skill.middleValue)
                {
                    OnClickGachaInfoClose();
                    return;
                }

                CurrencyManager.money3 -= skill.middleValue;
                shop.SkillGacha(this, skill.middleGach);
                break;
            case 2:
                if (CurrencyManager.money3 < skill.maxValue)
                {
                    OnClickGachaInfoClose();
                    return;
                }

                CurrencyManager.money3 -= skill.maxValue;
                shop.SkillGacha(this, skill.maxGacha);
                break;
        }
    }

    public void PetGacha()
    {
        switch (value)
        {
            case 0:
                if (CurrencyManager.money3 < pet.minValue)
                {
                    OnClickGachaInfoClose();
                    return;
                }

                CurrencyManager.money3 -= pet.minValue;
                shop.PetGacha(this, pet.minGacha);
                break;
            case 1:
                if (CurrencyManager.money3 < pet.middleValue)
                {
                    OnClickGachaInfoClose();
                    return;
                }

                CurrencyManager.money3 -= pet.middleValue;
                shop.PetGacha(this, pet.middleGach);
                break;
            case 2:
                if (CurrencyManager.money3 < pet.maxValue)
                {
                    OnClickGachaInfoClose();
                    return;
                }

                CurrencyManager.money3 -= pet.maxValue;
                shop.PetGacha(this, pet.maxGacha);
                break;
        }
    }

    public void OnClickGachaInfoClose()
    {
        foreach (var slot in slots)
        {
            slot.SetActive(false);
        }

        switch (type)
        {
            case GachaType.Item:
                item.GachaUpdate();
                value = -1;
                type = GachaType.None;
                gameObject.SetActive(false);
                break;

                case GachaType.Skill:
                skill.GachaUpdate();
                value = -1;
                type = GachaType.None;
                gameObject.SetActive(false);
                break;

            case GachaType.Crew:
                pet.GachaUpdate();
                value = -1;
                type = GachaType.None;
                gameObject.SetActive(false);
                break;
        }
    }
}
