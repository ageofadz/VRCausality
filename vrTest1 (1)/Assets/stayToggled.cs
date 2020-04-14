using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stayToggled : MonoBehaviour
{
    private UnityEngine.UI.Toggle toggle;

    private void Start()
    {
        toggle = GetComponent<UnityEngine.UI.Toggle>();
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool isOn)
    {
        UnityEngine.UI.ColorBlock cb = toggle.colors;
        if (isOn)
        {
            GetComponent<Image>().color = Color.gray;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
        }
    }
}