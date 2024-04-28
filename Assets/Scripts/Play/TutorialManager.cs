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

    [SerializeField] GameObject blockCase21;

    [SerializeField] GameObject blockTaro;

    [SerializeField] Transform redField;

    [SerializeField] GameObject destroyTut;

    [SerializeField] Button viewBT;

    private bool case18;
    private bool case21;

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
        if(GameManager.Instance.CurrentLevelData.PlayerCards.Count == 2 && !case18)
        {
            StartCoroutine(WaitTime(4.5f));
            case18 = true;
        }

        if (redField.childCount == 0 && !case21)
        {
            StartCoroutine(WaitTime(0.5f));
            case21 = true;
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
            case 2:
                viewClone.SetActive(true);
                TextSetting();
                break;

            case 3:
                viewBT.onClick.AddListener(NextTutorial);
                viewClone.SetActive(false);
                TaroDisable();
                fade.SetActive(false);
                break;
            case 4:
                fade.SetActive(true);
                gem.SetActive(true);
                TextSetting();
                break;
            case 5:
                gem.SetActive(false);
                playerView.SetActive(true);
                TextSetting();
                break;
            case 6:
                playerView.SetActive(false);
                cpuView.SetActive(true);
                TextSetting();
                break;
            case 7:
                cpuView.SetActive(false);
                useView.SetActive(true);
                TextSetting();
                break;
            case 8:
                useView.SetActive(false);
                TextSetting();
                break;

            case 9:
                fade.SetActive(false);
                break;
            case 10:
                viewBT.onClick.RemoveListener(NextTutorial);
                fade.SetActive(true);
                TextSetting();
                break;
            case 11:
                TaroDisable();
                taros.GetChild(0).GetComponent<Button>().interactable = true;
                fade.SetActive(false);
                break;
            case 12:
                fade.SetActive(true);
                TextSetting();
                break;
            case 18:
                fade.SetActive(false);
                Case18Effect();
                break;
            case 19:
                fade.SetActive(true);
                TextSetting();
                break;

            case 21:
                blockTaro.SetActive(false);
                fade.SetActive(false);
                blockCase21.SetActive(true);
                break;
            case 22:
                fade.SetActive(true);
                blockCase21.SetActive(false);
                Case21Effect();
                TextSetting();
                break;
            case 23:
                Destroy(destroyTut);
                break;


            default: TextSetting(); break;
        }
    }

    [SerializeField] GameObject viewClone;

    [SerializeField] GameObject gem;

    [SerializeField] GameObject playerView;
    [SerializeField] GameObject cpuView;
    [SerializeField] GameObject useView;

    private void TaroDisable()
    {
        for(int i = 0; i < taros.childCount; i++)
        {
            taros.GetChild(i).GetComponent<Button>().interactable = false;
        }
    }

    private void Case18Effect()
    {
        for(int i =0; i < playerHand.childCount; i++)
        {
            playerHand.GetChild(i).GetComponent<DragObject>().enabled = false;
        }

        playerHand.GetChild(0).GetComponent<DragObject>().enabled = true;

        blockTaro.SetActive(true);
    }

    private void Case21Effect()
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
