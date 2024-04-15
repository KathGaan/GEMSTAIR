using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpUI : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> texts;

    [SerializeField] GameObject taroPrefab;

    [SerializeField] Transform selectedTaroCard;

    [SerializeField] TextMeshProUGUI infoText;

    private void Start()
    {
        SetTexts();

        infoText.text = GameManager.Instance.PlayManager.PopUpText;

        OptionManager.Instance.changeLanguage += SetTexts;

        StartCoroutine(CreatePlayTaro());
    }

    private void OnDisable()
    {
        OptionManager.Instance.changeLanguage -= SetTexts;
    }

    public void SetTexts()
    {
        for(int i = 0; i < texts.Count;i++)
        {
            texts[i].text = TextManager.Instance.LoadString("PopUpText", i);
        }
    }

    private IEnumerator CreatePlayTaro()
    {
        yield return YieldCache.WaitForSeconds(1f);

        for (int i = 0; i < GameManager.Instance.SelectedTaroCards.Count; i++)
        {
            taroPrefab.GetComponent<TaroInfo>().CardNum = GameManager.Instance.SelectedTaroCards[i];
            Instantiate(taroPrefab, selectedTaroCard);

        }
    }

    //Button

    public void ButtonClick(int sceneNumber)
    {
        SoundManager.Instance.ButtonSound();

        if(sceneNumber == 3)
        {
            GameManager.Instance.LoadSelectedLevelData();
        }

        StartCoroutine(AsyncSceneLoadManager.Instance.AsyncSceneLoad((SceneName)sceneNumber));
    }
}
