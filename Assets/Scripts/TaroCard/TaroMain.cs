using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.UI;

public class TaroMain : MonoBehaviour
{
    [SerializeField] GameObject selectUI;

    [SerializeField] Button startButton;

    [SerializeField] List<Image> taroCards;
    private Color visualCard = new Color(1, 1, 0.5f, 1);
    private Color disVisualCard = new Color(1, 1, 0.5f, 0);
    private List<int> selectedTaroCards = new List<int>();

    //View
    public void ViewTable()
    {
        SoundManager.Instance.ButtonSound();

        if (selectUI.activeSelf)
        {
            selectUI.SetActive(false);
        }
        else
        {
            selectUI.SetActive(true);
        }
    }

    //Start
    private void Start()
    {
        startButton.interactable = false;

        TextSet();

        OptionManager.Instance.changeLanguage += TextSet;
    }

    private void OnDisable()
    {
        OptionManager.Instance.changeLanguage -= TextSet;
    }

    private void TextSet()
    {
        startButton.GetComponentInChildren<TextMeshProUGUI>().text = TextManager.Instance.LoadString("OneButtonTexts", 1);
    }

    //TaroSelect
    public void SelectThisCard(int num)
    {
        for(int i = 0; i < selectedTaroCards.Count; i++)
        {
            if (selectedTaroCards[i] == num)
            {
                selectedTaroCards.RemoveAt(i);
                taroCards[num].color = disVisualCard;
                startButton.interactable = false;
                return;
            }
        }

        if(selectedTaroCards.Count >= GameManager.Instance.CurrentLevelData.TaroCardNum)
        {
            return;
        }

        selectedTaroCards.Add(num);
        taroCards[num].color = visualCard;

        if (selectedTaroCards.Count >= GameManager.Instance.CurrentLevelData.TaroCardNum)
        {
            startButton.interactable = true;
        }
    }

    public void EndSelect()
    {
        SoundManager.Instance.ButtonSound();

        gameObject.SetActive(false);

        GameManager.Instance.SetTaroCards(selectedTaroCards);
    }
}
