using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject fade;

    [SerializeField] TextMeshProUGUI tutorialText;

    private int tutorialIndex;

    //Start

    private void Start()
    {
        tutorialIndex = 0;
        TextSetting();

        OptionManager.Instance.changeLanguage += TextSetting;
    }

    private void OnDisable()
    {
        OptionManager.Instance.changeLanguage -= TextSetting;
    }

    //Button

    public void NextTutorial()
    {
        tutorialIndex++;
        TutorialAction();
    }

    //tutorial Action
    private void TutorialAction()
    {
        switch (tutorialIndex)
        {
            case 4:
                fade.SetActive(false);
                tutorialText.text = "";
                break;
            case 5:
                fade.SetActive(true);
                TextSetting();
                break;
            case 9:
                fade.SetActive(false);
                tutorialText.text = "";
                break;
            case 10:
                fade.SetActive(true);
                TextSetting();
                break;
            case 11:
                fade.SetActive(false);
                tutorialText.text = "";
                break;
            case 13:
                Destroy(gameObject);
                break;

            default: TextSetting(); break;
        }
    }

    private void TextSetting()
    {
        tutorialText.text = TextManager.Instance.LoadString("TutorialText", tutorialIndex);
    }
}
