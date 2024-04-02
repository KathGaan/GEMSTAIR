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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (infoText.Length < 1)
        {
            infoText = TextManager.Instance.LoadString("TaroInfoText", cardNum);

            infoImage.GetComponentInChildren<TextMeshProUGUI>().text = infoText;
        }

        infoImage.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoImage.gameObject.SetActive(false);
    }
}
