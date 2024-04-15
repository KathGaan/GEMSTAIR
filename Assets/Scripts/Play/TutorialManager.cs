using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject fade;

    [SerializeField] TextMeshProUGUI tutorialText;

    private int tutorialIndex;

    [SerializeField] Transform taros;

    [SerializeField] Transform playerHand;

    [SerializeField] GameObject blockCase11;

    [SerializeField] GameObject blockTaro;

    [SerializeField] Transform redField;

    [SerializeField] GameObject destroyTut;

    private bool case9;
    private bool case11;

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

    private void Update()
    {
        if(GameManager.Instance.CurrentLevelData.PlayerCards.Count == 2 && !case9)
        {
            StartCoroutine(WaitTime(6f));
            case9 = true;
        }

        if (redField.childCount == 0 && !case11)
        {
            NextTutorial();
            case11 = true;
        }
    }

    private IEnumerator WaitTime(float time)
    {
        yield return YieldCache.WaitForSeconds(time);

        NextTutorial();
    }

    //Button

    public void Case4()
    {
        StartCoroutine(WaitTime(1f));
    }

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
                Case4Effect();
                break;
            case 5:
                fade.SetActive(true);
                TextSetting();
                break;
            case 9:
                fade.SetActive(false);
                tutorialText.text = "";
                Case9Effect();
                break;
            case 10:
                fade.SetActive(true);
                TextSetting();
                break;
            case 11:
                fade.SetActive(false);
                tutorialText.text = "";
                blockCase11.SetActive(true);
                blockTaro.SetActive(false);
                break;
            case 12:
                fade.SetActive(true);
                blockCase11.SetActive(false);
                TextSetting();
                break;
            case 13:
                Case13Effect();
                Destroy(destroyTut);
                break;

            default: TextSetting(); break;
        }
    }

    private void Case4Effect()
    {
        for(int i = 0; i < taros.childCount; i++)
        {
            taros.GetChild(i).GetComponent<Button>().interactable = false;
        }

        taros.GetChild(0).GetComponent<Button>().interactable = true;
    }

    private void Case9Effect()
    {
        for(int i =0; i < playerHand.childCount; i++)
        {
            playerHand.GetChild(i).GetComponent<DragObject>().enabled = false;
        }

        playerHand.GetChild(0).GetComponent<DragObject>().enabled = true;

        blockTaro.SetActive(true);
    }

    private void Case13Effect()
    {
        for (int i = 0; i < playerHand.childCount; i++)
        {
            playerHand.GetChild(i).GetComponent<DragObject>().enabled = true;
        }
    }

    private void TextSetting()
    {
        tutorialText.text = TextManager.Instance.LoadString("TutorialText", tutorialIndex);
    }
}
