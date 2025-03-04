using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionManager : MonoSingletonManager<OptionManager>
{
    //Resolution

    public void SetResolution(int width)
    {
        SoundManager.Instance.ButtonSound();

        Screen.SetResolution(width, (width / 16) * 9, false);

        if (width >= 1920)
        {
            Screen.fullScreen = true;
        }
    }

    //Sound
    [SerializeField] Slider[] volumes;

    public void SetVolume(int num)
    {
        switch (num)
        {
            case 0: SoundManager.Instance.SetVolumeMaster(volumes[num].value); break;
            case 1: SoundManager.Instance.SetVolumeSFX(volumes[num].value); break;
            case 2: SoundManager.Instance.SetVolumeBGM(volumes[num].value); break;
        }
    }

    //Language
    public Action changeLanguage;
    public void SetLanguage(string language)
    {
        SoundManager.Instance.ButtonSound();

        DataManager.Instance.Data.Language = language;

        if (changeLanguage != null)
        {
            changeLanguage.Invoke();
        }

        OptionTextSetting();
    }

    [SerializeField] GameObject optionUI;

    public void LeaveOption()
    {
        SoundManager.Instance.ButtonSound();

        Time.timeScale = 1f;

        InputManager.Instance.optionOpened = false;

        optionUI.SetActive(false);
    }

    public void GoMain()
    {
        SoundManager.Instance.ButtonSound();

        StartCoroutine(AsyncSceneLoadManager.Instance.AsyncSceneLoad(SceneName.MainMenu));

        optionUI.SetActive(false);
    }

    public void Retry()
    {
        SoundManager.Instance.ButtonSound();

        GameManager.Instance.LoadSelectedLevelData();

        StartCoroutine(AsyncSceneLoadManager.Instance.AsyncSceneLoad(SceneName.Play));

        optionUI.SetActive(false);
    }

    [SerializeField] Button goMainButton;
    [SerializeField] Button retryButton;

    public void OpenOption()
    {
        SoundManager.Instance.ButtonSound();

        Time.timeScale = 0f;

        InputManager.Instance.optionOpened = true;

        optionUI.SetActive(true);

        if (SceneManager.GetActiveScene().name == SceneName.MainMenu.ToString())
        {
            goMainButton.interactable = false;
        }
        else if (!goMainButton.interactable)
        {
            goMainButton.interactable = true;
        }

        if(SceneManager.GetActiveScene().name == SceneName.Play.ToString())
        {
            retryButton.interactable = true;
        }
        else if(retryButton.interactable)
        {
            retryButton.interactable = false;
        }
    }

    //StartSetting
    public void OptionKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !optionUI.activeSelf)
        {
            OpenOption();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && optionUI.activeSelf)
        {
            LeaveOption();
        }
    }

    [SerializeField] TextMeshProUGUI[] texts;

    public void OptionTextSetting()
    {
        for (int i = 0; i < TextManager.Instance.GetCSVLength("OptionText"); i++)
        {
            texts[i].text = TextManager.Instance.LoadString("OptionText", i);
        }
    }

    private void SetLoadData()
    {
        volumes[0].value = DataManager.Instance.Data.MasterVolume;
        volumes[1].value = DataManager.Instance.Data.SFXVolume;
        volumes[2].value = DataManager.Instance.Data.BGMVolume;
    }

    //Version

    [SerializeField] TextMeshProUGUI version;

    private void SetVersionText()
    {
        version.text = Application.version;
    }

    private void Start()
    {
        InputManager.Instance.keyDownAction += OptionKeyDown;

        SetLoadData();

        OptionTextSetting();

        SetVersionText();

        optionUI.SetActive(false);
    }

    //SaveData
    private void OnApplicationQuit()
    {
        DataManager.Instance.SaveData();
    }
}
