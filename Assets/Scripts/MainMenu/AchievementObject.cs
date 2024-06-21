using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AchievementObject : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] int achieveNum;

    public int AchieveNum
    {
        get { return achieveNum; }
        set { achieveNum = value; }
    }

    [SerializeField] GameObject infoTextUI;

    private string infoText = "";

    [SerializeField] Image image;

    public void SetGrayImage(Material material)
    {
        image.material = material;
    }

    public void Cleared()
    {
        image.material = null;
    }

    private void ChangeLanguageReset()
    {
        infoText = "";
    }

    //Infomation

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeLanguageReset();

        infoText = TextManager.Instance.LoadString("AchieveInfoText", achieveNum);

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
