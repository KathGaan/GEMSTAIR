using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaroMain : MonoBehaviour
{
    [SerializeField] GameObject selectUI;

    [SerializeField] TextMeshProUGUI howMany;

    private int howManyTaros;

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

        howManyTaros = GameManager.Instance.CurrentLevelData.TaroCardNum;

        howManyText();

        TextSet();

        if(GameManager.Instance.selectedLevel <= 20)
        {
            taroCards[14].GetComponent<Button>().interactable = false;
        }

        SetBanTaro();

        OptionManager.Instance.changeLanguage += TextSet;
        OptionManager.Instance.changeLanguage += howManyText;
    }

    private void OnDisable()
    {
        OptionManager.Instance.changeLanguage -= TextSet;
        OptionManager.Instance.changeLanguage -= howManyText;
    }

    private void SetBanTaro()
    {
        for(int i = 0; i < ResourcesManager.Instance.LoadScript(GameManager.Instance.selectedLevel).BanTaro.Count; i++)
        {
            taroCards[ResourcesManager.Instance.LoadScript(GameManager.Instance.selectedLevel).BanTaro[i]].GetComponent<Button>().interactable = false;
        }
    }

    private void TextSet()
    {
        startButton.GetComponentInChildren<TextMeshProUGUI>().text = TextManager.Instance.LoadString("OneButtonTexts", 1);
    }

    private void howManyText()
    {
        howMany.text = TextManager.Instance.LoadString("OneButtonTexts", 2) + howManyTaros;
    }

    //TaroSelect
    public void SelectThisCard(int num)
    {
        for(int i = 0; i < selectedTaroCards.Count; i++)
        {
            if (selectedTaroCards[i] == num)
            {
                SoundManager.Instance.ButtonSound();
                selectedTaroCards.RemoveAt(i);
                taroCards[num].color = disVisualCard;
                startButton.interactable = false;
                howManyTaros++;
                howManyText();
                return;
            }
        }

        if(selectedTaroCards.Count >= GameManager.Instance.CurrentLevelData.TaroCardNum)
        {
            return;
        }

        SoundManager.Instance.ButtonSound();
        selectedTaroCards.Add(num);
        taroCards[num].color = visualCard;
        howManyTaros--;
        howManyText();

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
