using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    //Button Function
    public void StartGame()
    {
        SoundManager.Instance.ButtonSound();

        StartCoroutine(AsyncSceneLoadManager.Instance.AsyncSceneLoad(SceneName.LevelSelect));
    }

    public void OpenSetting()
    {
        OptionManager.Instance.OpenOption();
    }

    public void QuitGame()
    {
        SoundManager.Instance.ButtonSound();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    [SerializeField] TextMeshProUGUI[] texts;

    private void SetMainText()
    {
        for (int i = 0; i < TextManager.Instance.GetCSVLength("MainText"); i++)
        {
            texts[i].text = TextManager.Instance.LoadString("MainText", i);
        }
    }
    //Start
    private void Start()
    {
        OptionManager.Instance.changeLanguage += SetMainText;

        SetMainText();
    }

    private void OnDisable()
    {
        OptionManager.Instance.changeLanguage -= SetMainText;
    }

    //Credit

    [SerializeField] GameObject credit;

    public void OpenCredit()
    {
        SoundManager.Instance.ButtonSound();

        if (credit.activeSelf)
        {
            credit.SetActive(false);
        }
        else
        {
            credit.SetActive(true);
        }
    }
}
