using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TaroInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image infoImage;

    private string infoText = "";

    [SerializeField] int cardNum;

    [SerializeField] Image cardImage;

    public int CardNum
    {
        set { cardNum = value; }
        get { return cardNum; }
    }

    [SerializeField] TaroDataBase dataBase;

    //Start

    private void Start()
    {
        ImageSetting();
    }

    private void ImageSetting()
    {
        cardImage.sprite = ResourcesManager.Instance.Load<Sprite>("TaroImage/" + cardNum);

        if (GetComponent<DragObject>() != null)
        {
            GetComponent<DragObject>().Info = dataBase.TaroDatas[cardNum];
        }
    }

    public void HintSetting()
    {
        ImageSetting();
    }


    //Infomation

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoText = TextManager.Instance.LoadString("TaroInfoText", cardNum);

        infoImage.GetComponentInChildren<TextMeshProUGUI>().text = infoText;
        
        infoImage.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoImage.gameObject.SetActive(false);
    }
}
