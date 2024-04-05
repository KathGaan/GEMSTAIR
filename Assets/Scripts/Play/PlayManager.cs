using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabs;

    [SerializeField] GameObject taroPrefab;
    [SerializeField] Transform taroHand;

    public Transform TaroHand
    {
        get { return taroHand; }
    }

    [SerializeField] List<Transform> settingTransforms;

    [SerializeField] Transform playerHand;

    public Transform PlayerHand
    {
        get { return playerHand; }
    }

    [SerializeField] Transform dragNow;

    public Transform DragNow
    {
        get { return dragNow; }
    }

    [SerializeField] Image waitImage;

    //Start
    private void Start()
    {
        GameManager.Instance.PlayManager = this;

        StartSetting();
    }

    private void StartSetting()
    {
        List<List<Card>> getSetting = GameManager.Instance.CurrentLevelData.GetSetting();

        for(int i = 0; i < settingTransforms.Count; i++)
        {
            for (int j = 0; j < getSetting[i].Count; j++)
            {
                prefabs[(int)getSetting[i][j].color].GetComponent<DragObject>().Info = getSetting[i][j];
                prefabs[(int)getSetting[i][j].color].GetComponentInChildren<TextMeshProUGUI>().text = "" + getSetting[i][j].num;
                Instantiate(prefabs[(int)getSetting[i][j].color], settingTransforms[i]);
            }
        }
    }

    public void StartTaroSetting()
    {
        for(int i = 0; i < GameManager.Instance.SelectedTaroCards.Count; i++)
        {
            taroPrefab.GetComponent<TaroInfo>().CardNum = GameManager.Instance.SelectedTaroCards[i];
            Instantiate(taroPrefab, taroHand);

            StartCoroutine(PlayerTurnStart());
        }
    }

    //PlayerTurn

    public IEnumerator PlayerTurnStart()
    {
        waitImage.gameObject.SetActive(true);

        waitImage.GetComponent<Animator>().SetTrigger("PlayerTurn");

        yield return YieldCache.WaitForSeconds(1f);

        waitImage.gameObject.SetActive(false);
    }

    //CpuTurn

    public IEnumerator CpuTurnStart()
    {
        waitImage.gameObject.SetActive(true);

        waitImage.GetComponent<Animator>().SetTrigger("CpuTurn");

        yield return YieldCache.WaitForSeconds(1f);

        waitImage.gameObject.SetActive(false);

        StartCoroutine(CpuTurnAction());
    }

    private IEnumerator CpuTurnAction()
    {
        List<Card> dummy = null;

        for(int i = 0; i < 3; i++)
        {
            switch (i)
            {
                case 0: 
                    dummy = GameManager.Instance.CurrentLevelData.Cpu1Cards;
                    break;
                case 1:
                    dummy = GameManager.Instance.CurrentLevelData.Cpu2Cards;
                    break;
                case 2:
                    dummy = GameManager.Instance.CurrentLevelData.Cpu3Cards;
                    break;
            }

            for(int j = 0; j < dummy.Count; j++)
            {
                if (TaskCpu(dummy[0]))
                {
                    CpuUse(i , dummy[0]);

                    yield return YieldCache.WaitForSeconds(0.5f);

                    break;
                }

                yield return YieldCache.WaitForSeconds(0.5f);

                ChangeCpuHand(1, dummy);

                yield return YieldCache.WaitForSeconds(0.5f);

            }
        }

        StartCoroutine(PlayerTurnStart());
    }

    //CpuAction

    private bool TaskCpu(Card card)
    {
        switch (card.color)
        {
            case CardColor.Red:
                if (card.num > GameManager.Instance.CurrentLevelData.RedField[GameManager.Instance.CurrentLevelData.RedField.Count - 1].num)
                    return true;
                break;
            case CardColor.Blue:
                if (card.num > GameManager.Instance.CurrentLevelData.BlueField[GameManager.Instance.CurrentLevelData.BlueField.Count - 1].num)
                    return true;
                break;
            case CardColor.White:
                if (card.num > GameManager.Instance.CurrentLevelData.WhiteField[GameManager.Instance.CurrentLevelData.WhiteField.Count - 1].num)
                    return true;
                break;
        }

        return false;
    }

    private void CpuUse(int i , Card card)
    {
        switch (i)
        {
            case 0:
                settingTransforms[4 + i].GetChild(0).SetParent(settingTransforms[(int)card.color]);
                GetColorParent(card.color).Add(GameManager.Instance.CurrentLevelData.Cpu1Cards[0]);
                GameManager.Instance.CurrentLevelData.Cpu1Cards.RemoveAt(0);
                break;
            case 1:
                settingTransforms[4 + i].GetChild(0).SetParent(settingTransforms[(int)card.color]);
                GetColorParent(card.color).Add(GameManager.Instance.CurrentLevelData.Cpu2Cards[0]);
                GameManager.Instance.CurrentLevelData.Cpu2Cards.RemoveAt(0);
                break;
            case 2:
                settingTransforms[4 + i].GetChild(0).SetParent(settingTransforms[(int)card.color]);
                GetColorParent(card.color).Add(GameManager.Instance.CurrentLevelData.Cpu3Cards[0]);
                GameManager.Instance.CurrentLevelData.Cpu3Cards.RemoveAt(0);
                break;
        }
    }

    private List<Card> GetColorParent(CardColor color)
    {
        switch (color)
        {
            case CardColor.Red:
                return GameManager.Instance.CurrentLevelData.RedField;

            case CardColor.Blue:
                return GameManager.Instance.CurrentLevelData.BlueField;

            case CardColor.White:
                return GameManager.Instance.CurrentLevelData.WhiteField;
            default: break;
        }
        return null;
    }

    private void ChangeCpuHand(int i , List<Card> dummy)
    {
        dummy.Add(dummy[0]);
        dummy.RemoveAt(0);

        settingTransforms[4 + i].GetChild(0).SetAsLastSibling();
    }
}
