using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    [SerializeField] Button hintButton;

    [SerializeField] GameObject hintUI;

    [SerializeField] List<TaroInfo> hintCards;

    private void Start()
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
        hintButton.GetComponentInChildren<TextMeshProUGUI>().text = TextManager.Instance.LoadString("OneButtonTexts", 3);
    }

    public void OpenHintUI()
    {
        SoundManager.Instance.ButtonSound();

        if (!hintUI.activeSelf)
        {
            hintUI.SetActive(true);

            LoadTaroHint();
        }
        else
        {
            hintUI.SetActive(false);
        }
    }

    private void LoadTaroHint()
    {
        for(int i = 0; i < 3; i++)
        {
            hintCards[i].gameObject.SetActive(true);

            if (ResourcesManager.Instance.LoadScript(GameManager.Instance.selectedLevel).TaroHint.Count <= i)
            {
                hintCards[i].gameObject.SetActive(false);
            }
            else
            {
                hintCards[i].CardNum = ResourcesManager.Instance.LoadScript(GameManager.Instance.selectedLevel).TaroHint[i];
                hintCards[i].HintSetting();
            }
        }
    }
}
