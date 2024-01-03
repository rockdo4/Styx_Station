using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StateWindow_ : Window
{
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

    }
}
