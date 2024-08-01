using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GemInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler , IPointerClickHandler
{
    [SerializeField] GameObject infoTextUI;

    private string infoText = "";

    [SerializeField] int gemAbNum;

    public int GemAbNum
    {
        get { return gemAbNum; }
        set { gemAbNum = value; }
    }

    [SerializeField] Image taroImage;

    //Start

    private void Start()
    {
        ImageSetting();
    }

    public void ImageSetting()
    {
        taroImage.sprite = ResourcesManager.Instance.Load<Sprite>("TaroImage/" + gemAbNum);
    }


    //Infomation

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoText = TextManager.Instance.LoadString("GemInfoText", gemAbNum);

        infoTextUI.GetComponentInChildren<TextMeshProUGUI>().text = infoText;

        infoTextUI.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoTextUI.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        infoTextUI.SetActive(false);
    }
}
