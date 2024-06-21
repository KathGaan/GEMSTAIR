using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditMain : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private void Start()
    {
        TextSetting();
    }

    private void OnEnable()
    {
        TextSetting();
        OptionManager.Instance.changeLanguage += TextSetting;
    }

    private void OnDisable()
    {
        OptionManager.Instance.changeLanguage -= TextSetting;
    }

    public void TextSetting()
    {
        text.text = TextManager.Instance.LoadString("CreditText", 0);
    }
}
