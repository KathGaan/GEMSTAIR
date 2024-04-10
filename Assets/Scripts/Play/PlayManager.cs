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
    [SerializeField] Animator waitIAnim;

    private CardColor taroColor;

    public CardColor TaroColor
    {
        get { return taroColor; }
    }

    private Card usedTaroCard;

    public Card UsedTaroCard
    {
        get { return usedTaroCard; }
    }

    private GameObject usedTaroObj;
    private TaroFunction taroFunction;
    [SerializeField] Animator taroAnim;

    [SerializeField] List<GameObject> canPlaces;

    [SerializeField] Transform destroyGem;

    private List<bool> cpuSkip = new List<bool>() {false,false,false};

    public List<bool> CpuSkip
    {
        get { return cpuSkip; }
        set { cpuSkip = value; }
    }

    private List<bool> colorSkip = new List<bool>() { false, false, false };

    public List<bool> ColorSkip
    {
        get { return colorSkip; }
        set { colorSkip = value; }
    }

    private bool return20;

    public bool Return20
    {
        set { return20 = value; }
        get { return return20; }
    }

    private int skipPlayer;

    public int SkipPlayer
    {
        set { skipPlayer = value; }
        get { return skipPlayer; }
    }

    //Start
    private void Start()
    {
        GameManager.Instance.PlayManager = this;

        taroFunction = new TaroFunction();

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

        }

        waitImage.gameObject.SetActive(true);
        StartCoroutine(PlayerTurnStart());
    }

    //PlayerTurn

    public IEnumerator PlayerTurnStart()
    {
        if(return20 == true)
        {
            AddTaro(20);
            return20 = false;
        }

        waitIAnim.SetTrigger("PlayerTurn");

        yield return YieldCache.WaitForSeconds(1.5f);

        waitImage.gameObject.SetActive(false);

        if (skipPlayer > 0)
        {
            skipPlayer--;
            if (skipPlayer == 0)
            {
                SkipTurn();
            }
        }
    }

    public void SkipTurn()
    {
        StartCoroutine(CpuTurnStart());
    }

    //CpuTurn

    public IEnumerator CpuTurnStart()
    {
        GameManager.dragObject = null;

        waitImage.gameObject.SetActive(true);

        waitIAnim.SetTrigger("CpuTurn");

        yield return YieldCache.WaitForSeconds(1.5f);

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
                if (cpuSkip[i])
                {
                    cpuSkip[i] = false;
                    break;
                }

                if (TaskCpu(dummy[0]))
                {
                    CpuUse(i , dummy[0]);

                    yield return YieldCache.WaitForSeconds(0.5f);

                    break;
                }

                yield return YieldCache.WaitForSeconds(0.5f);

                ChangeCpuHand(i, dummy);

                yield return YieldCache.WaitForSeconds(0.5f);

            }
        }

        for(int i = 0; i < colorSkip.Count; i++)
        {
            colorSkip[i] = false;
        }

        StartCoroutine(PlayerTurnStart());
    }

    //CpuAction

    private bool TaskCpu(Card card)
    {
        if (colorSkip[(int)card.color])
        {
            return false;
        }

        switch (card.color)
        {
            case CardColor.Red:
                if (GameManager.Instance.CurrentLevelData.RedField.Count <= 0)
                    return true;

                if (card.num > GameManager.Instance.CurrentLevelData.RedField[GameManager.Instance.CurrentLevelData.RedField.Count - 1].num)
                    return true;
                break;
            case CardColor.Blue:
                if (GameManager.Instance.CurrentLevelData.BlueField.Count <= 0)
                    return true;

                if (card.num > GameManager.Instance.CurrentLevelData.BlueField[GameManager.Instance.CurrentLevelData.BlueField.Count - 1].num)
                    return true;
                break;
            case CardColor.White:
                if (GameManager.Instance.CurrentLevelData.WhiteField.Count <= 0)
                    return true;

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

    public List<Card> GetColorParent(CardColor color)
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

    public void ChangeCpuHand(int i)
    {
        GetCpuParent((CardColor)i).Add(GetCpuParent((CardColor)i)[0]);
        GetCpuParent((CardColor)i).RemoveAt(0);

        settingTransforms[4 + i].GetChild(0).SetAsLastSibling();
    }

    //Taro

    public void GetTaro(Card taroCard , CardColor color)
    {
        usedTaroObj = GameManager.dragObject;

        usedTaroCard = taroCard;
        taroColor = color;

        StartCoroutine(ActiveTaroEffect());
    }

    public IEnumerator ActiveTaroEffect()
    {
        taroAnim.gameObject.SetActive(true);
        taroAnim.SetTrigger(usedTaroObj.GetComponent<TaroInfo>().CardNum + "");

        yield return YieldCache.WaitForSeconds(0.5f);

        taroFunction.taroFunctions[usedTaroObj.GetComponent<TaroInfo>().CardNum]();
        usedTaroObj.SetActive(false);

        yield return YieldCache.WaitForSeconds(0.5f);

        Destroy(usedTaroObj);

        usedTaroObj = null;

        taroAnim.gameObject.SetActive(false);
    }

    //CanPlace

    public IEnumerator SetCanPlace()
    {
        if (GameManager.dragObject == null)
            yield break ;

        switch (GameManager.dragObject.GetComponent<DragObject>().Info.color)
        {
            case CardColor.Red:
                canPlaces[0].SetActive(true);
                break;
            case CardColor.Blue:
                canPlaces[1].SetActive(true);
                break;
            case CardColor.White:
                canPlaces[2].SetActive(true);
                break;
            case CardColor.None:
                for (int i = 0; i < canPlaces.Count; i++)
                {
                    canPlaces[i].SetActive(true);
                }
                break;
        }

        while (true)
        {
            if (GameManager.dragObject == null)
                break;

            yield return null;
        }

        for(int i = 0; i < canPlaces.Count; i ++)
        {
            canPlaces[i].SetActive(false);
        }
    }

    //Effect
    public void DestroyGems()
    {
        int i = settingTransforms[(int)taroColor].childCount;

        for (int j = 0; j < i; j++)
        {
            settingTransforms[(int)taroColor].GetChild(0).SetParent(destroyGem);
        }
    }

    public void DestroyGems(CardColor color)
    {
        int i = settingTransforms[(int)color].childCount;

        for (int j = 0; j < i; j++)
        {
            settingTransforms[(int)color].GetChild(0).SetParent(destroyGem);
        }
    }


    public void DestroyGem(Transform gem)
    {
        gem.SetParent(destroyGem);
    }

    public void DestroyHands()
    {
        int i = settingTransforms[3].childCount;

        for (int j = 0; j < i; j++)
        {
            settingTransforms[3].GetChild(0).SetParent(destroyGem);
        }
    }

    public GameObject GenerateGem(Card gem)
    {
        GameObject generate = null;

        switch (gem.color)
        {
            case CardColor.Red:
                generate = Instantiate(prefabs[0]);
                break;
            case CardColor.Blue:
                generate = Instantiate(prefabs[1]);
                break;
            case CardColor.White:
                generate = Instantiate(prefabs[2]);
                break;
        }

        generate.GetComponent<DragObject>().Info.num = gem.num;
        generate.GetComponentInChildren<TextMeshProUGUI>().text = "" + gem.num;

        return generate;
    }

    public void CopyTaro()
    {
        Instantiate(taroHand.GetChild(0).gameObject, taroHand);
    }

    public void AddTaro()
    {
        taroPrefab.GetComponent<TaroInfo>().CardNum = Random.Range(0,22);
        Instantiate(taroPrefab, taroHand);
    }

    public void AddTaro(int i)
    {
        taroPrefab.GetComponent<TaroInfo>().CardNum = i;
        Instantiate(taroPrefab, taroHand);
    }

    public Transform GetParentTransform(CardColor color)
    {
        return settingTransforms[(int)color];
    }

    public Transform GetCpuTransform(CardColor color)
    {
        return settingTransforms[(int)color + 4];
    }

    public List<Card> GetCpuParent(CardColor color)
    {
        switch (color)
        {
            case CardColor.Red:
                return GameManager.Instance.CurrentLevelData.Cpu1Cards;

            case CardColor.Blue:
                return GameManager.Instance.CurrentLevelData.Cpu2Cards;

            case CardColor.White:
                return GameManager.Instance.CurrentLevelData.Cpu3Cards;
            default: break;
        }
        return null;
    }
}
