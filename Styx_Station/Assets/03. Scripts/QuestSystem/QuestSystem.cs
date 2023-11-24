using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//This is Demo Version
public class QuestSystem : MonoBehaviour
{
    private List<QusetSystemData> questSystem = new List<QusetSystemData>();
    private int clearIndex;
    public TextMeshProUGUI text;
    public Button testButton;
    private void Awake()
    {
        clearIndex = 0; // ���� ������ �о �ٲ����
        QusetSystemData qusetSystemData1 = new QusetSystemData(false, "������", 0,0,false,1000000000000000000);
        QusetSystemData qusetSystemData2 = new QusetSystemData(false, "������", 0, 0, false, 1000000000000000000);
        QusetSystemData qusetSystemData3 = new QusetSystemData(false, "�̽¿�", 0, 0, false, 1000000000000000000);
        questSystem.Add(qusetSystemData1);
        questSystem.Add(qusetSystemData2);
        questSystem.Add(qusetSystemData3);
        SetString();
    }

    public void TestButton()
    {
        int a = 0;

        if(a == questSystem[clearIndex].intByClearCondition)
        {
            Debug.Log($"Get Money{questSystem[clearIndex].clearCompensation}");
            SharedPlayerStats.money1 += questSystem[clearIndex].clearCompensation;
            clearIndex++;
            SetString();
        }
    }
    
    private void SetString()
    {

        if (clearIndex >= questSystem.Count)
        {
            text.text = $"isNot ClearCompensation ,we will fix ";
            testButton.interactable = false;
        }
        else
        {
            text.text = questSystem[clearIndex].explanation;
            testButton.interactable = true;
        }
    }
}

struct QusetSystemData
{
    public string name;
    public bool isClear;
    public string explanation;
    public int intByClearCondition;
    public float floatByClearCondition;
    public bool boolByClearCondition;
    public ulong clearCompensation;

    public QusetSystemData(bool a ,string b , int c ,float d, bool e , ulong f)
    {
        name = "";
        isClear = a;
        explanation = b;
        intByClearCondition = c;
        floatByClearCondition = d;
        boolByClearCondition = e;
        clearCompensation = f; // ���⼭ clearCompensation�� �ùٸ��� �ʱ�ȭ
    }
}
