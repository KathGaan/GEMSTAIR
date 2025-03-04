using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelName;
    [SerializeField] TextMeshProUGUI levelInfo;
    private List<GameObject> levels = new List<GameObject>();

    [SerializeField] TextMeshProUGUI buttonText;

    [SerializeField] GameObject hintUI;

    [SerializeField] GameObject FinishText;

    private void Start()
    {
        GetLevelData();

        NumSetting();

        TextSet();

        OptionManager.Instance.changeLanguage += TextSet;

        ButtonSet();
    }

    private void GetLevelData()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            levels.Add(transform.GetChild(i).gameObject);
        }
    }

    //NumSetting
    private void NumSetting()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].GetComponentInChildren<TextMeshProUGUI>().text = "" + (i + 1);
        }
    }

    //Button
    public void SelectLevel(int num)
    {
        SoundManager.Instance.ButtonSound();

        if (hintUI.activeSelf)
        {
            hintUI.SetActive(false);
        }

        levelName.text = num + ". " + TextManager.Instance.LoadString("LevelNameText",num);
        levelInfo.text = TextManager.Instance.LoadString("LevelInfoText", num);
        GameManager.Instance.selectedLevel = num;
    }

    public void StartLevel()
    {
        SoundManager.Instance.ButtonSound();

        if(GameManager.Instance.selectedLevel == 61)
        {
            StartCoroutine(AsyncSceneLoadManager.Instance.AsyncSceneLoad(SceneName.MainMenu));
            return;
        }

        GameManager.Instance.LoadSelectedLevelData();

        if (GameManager.Instance.selectedLevel == 0)
        {
            StartCoroutine(AsyncSceneLoadManager.Instance.AsyncSceneLoad(SceneName.PlayTutorial));
        }
        else
        {
            StartCoroutine(AsyncSceneLoadManager.Instance.AsyncSceneLoad(SceneName.Play));
        }
    }

    private void ButtonSet()
    {
        for(int i = 0; i < DataManager.Instance.Data.ClearData.Count; i++)
        {
            if (levels.Count > i)
            {
                levels[i].GetComponent<Button>().interactable = true;
            }

            if(i == 60)
            {
                FinishText.GetComponent<Button>().interactable = true;
            }
        }
    }

    //Text

    private void OnDisable()
    {
        OptionManager.Instance.changeLanguage -= TextSet;
    }

    private void TextSet()
    {
        buttonText.text = TextManager.Instance.LoadString("OneButtonTexts", 0);

        levelName.text = GameManager.Instance.selectedLevel + ". " + TextManager.Instance.LoadString("LevelNameText", GameManager.Instance.selectedLevel);
        levelInfo.text = TextManager.Instance.LoadString("LevelInfoText", GameManager.Instance.selectedLevel);
    }
}
