using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelName;
    [SerializeField] TextMeshProUGUI levelInfo;
    [SerializeField] List<GameObject> levels;

    [SerializeField] TextMeshProUGUI buttonText;

    private void Start()
    {
        NumSetting();

        TextSet();

        OptionManager.Instance.changeLanguage += TextSet;
    }

    //NumSetting
    private void NumSetting()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].GetComponentInChildren<TextMeshProUGUI>().text = "" + i;
        }
    }

    //Button
    public void SelectLevel(int num)
    {
        SoundManager.Instance.ButtonSound();

        levelName.text = TextManager.Instance.LoadString("LevelNameText",num);
        levelInfo.text = TextManager.Instance.LoadString("LevelInfoText", num);
        GameManager.Instance.selectedLevel = num;
    }

    public void StartLevel()
    {
        SoundManager.Instance.ButtonSound();

        GameManager.Instance.LoadSelectedLevelData();

        StartCoroutine( AsyncSceneLoadManager.Instance.AsyncSceneLoad(SceneName.Play));
    }

    //Text

    private void OnDisable()
    {
        OptionManager.Instance.changeLanguage -= TextSet;
    }

    private void TextSet()
    {
        buttonText.text = TextManager.Instance.LoadString("OneButtonTexts", 0);

        levelName.text = TextManager.Instance.LoadString("LevelNameText", GameManager.Instance.selectedLevel);
        levelInfo.text = TextManager.Instance.LoadString("LevelInfoText", GameManager.Instance.selectedLevel);
    }
}
