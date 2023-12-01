using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
                //FindObjectOfType �Լ��� ������� ����.
            }

            return m_instance;
        }
    }
    private static UIManager m_instance; // �̱����� �Ҵ�� ����

    public TextMeshProUGUI text;
    
    public void ReSetText()
    {
        text.text = $"{UnitConverter.OutString(CurrencyManager.money1)}";
    }
}