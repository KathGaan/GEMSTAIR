using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UniqueGemInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] GameObject infoTextUI;

    private string infoText = "";

    [SerializeField] int uniqueGemNum;

    public int UniqueGemNum
    {
        get { return uniqueGemNum; }
        set { uniqueGemNum = value; }
    }

    [SerializeField] Image uniqueGemImage;

    private void Start()
    {
        ImageSetting();
    }


    private void ImageSetting()
    {
        uniqueGemImage.sprite = ResourcesManager.Instance.Load<Sprite>("UniqueGemImage/" + uniqueGemNum);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {

        infoText = TextManager.Instance.LoadString("UniqueGemInfoText", uniqueGemNum);

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
