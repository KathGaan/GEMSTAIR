using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
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

    [SerializeField] GameObject clearUI;

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

    private int return20;

    public int Return20
    {
        set { return20 = value; }
        get { return return20; }
    }

    private int return5;

    public int Return5
    {
        set { return5 = value; }
        get { return return5; }
    }

    private int skipCpuAfter;

    public int SkipCpuAfter
    {
        set { skipCpuAfter = value; }
        get { return skipCpuAfter; }
    }

    private string popUpText;

    public string PopUpText
    {
        get { return popUpText; }
    }

    private bool StopAll;

    [SerializeField] SoundClip soundClip;

    private int cpuX;

    public int CpuX
    {
        set { cpuX = value; }
        get { return cpuX; }
    }

    private bool taroGem9;

    public bool TaroGem9
    {
        get { return taroGem9; }
        set { taroGem9 = value; }
    }

    private TaroGemFunction taroGemFunction;

    public TaroGemFunction TaroGemFunction
    {
        get { return taroGemFunction; }
    }

    private bool skipPlayerTurn;

    public bool SkipPlayerTurn
    {
        get { return skipPlayerTurn; }
        set { skipPlayerTurn = value; }
    }

    [SerializeField] List<Image> cpuTaros;

    [SerializeField] List<Image> fieldTaros;

    private Color invisibleColor = new Color(1, 1, 1, 0);
    private Color visibleColor = new Color(1, 1, 1, 1);

    //Start
    private void Start()
    {
        GameManager.Instance.PlayManager = this;

        taroFunction = new TaroFunction();

        taroGemFunction = new TaroGemFunction();

        StartSetting();

        TrunOffPlayerHand();
    }

    private void StartSetting()
    {
        GameObject newObj = null;

        List<List<Card>> getSetting = GameManager.Instance.CurrentLevelData.GetSetting();

        for(int i = 0; i < settingTransforms.Count; i++)
        {
            for (int j = 0; j < getSetting[i].Count; j++)
            {
                if (getSetting[i][j].ab == true)
                {
                    prefabs[(int)getSetting[i][j].color + 3].GetComponent<GemInfo>().GemAbNum = getSetting[i][j].abNum;

                    newObj = Instantiate(prefabs[(int)getSetting[i][j].color + 3], settingTransforms[i]);

                    newObj.GetComponent<DragObject>().Info.num = getSetting[i][j].num;
                    newObj.GetComponent<DragObject>().Info.abNum = getSetting[i][j].abNum;
                    newObj.GetComponentInChildren<TextMeshProUGUI>().text = "" + getSetting[i][j].num;
                }
                else
                {
                    newObj = Instantiate(prefabs[(int)getSetting[i][j].color], settingTransforms[i]);

                    newObj.GetComponent<DragObject>().Info.num = getSetting[i][j].num;
                    newObj.GetComponentInChildren<TextMeshProUGUI>().text = "" + getSetting[i][j].num;
                }
            }
        }
    }

    private void TrunOffPlayerHand()
    {
        for (int i = 0; i < playerHand.childCount; i++)
        {
            playerHand.GetChild(i).GetComponent<DragObject>().enabled = false;
        }
    }

    private void TrunOnPlayerHand()
    {
        for (int i = 0; i < playerHand.childCount; i++)
        {
            playerHand.GetChild(i).GetComponent<DragObject>().enabled = true;
        }
    }


    public void StartTaroSetting()
    {
        for(int i = 0; i < GameManager.Instance.SelectedTaroCards.Count; i++)
        {
            taroPrefab.GetComponent<TaroInfo>().CardNum = GameManager.Instance.SelectedTaroCards[i];
            Instantiate(taroPrefab, taroHand);

        }

        taroHand.GetComponent<GridField>().GetChilds();

        waitImage.gameObject.SetActive(true);

        TrunOnPlayerHand();

        StartCoroutine(PlayerTurnStart());
    }

    //PlayerTurn

    public IEnumerator PlayerTurnStart()
    {
        if (return20 > 0)
        {
            for(int i = 0; i < return20; i++)
            {
                AddTaro(20);
            }
            return20 = 0;
        }

        if (return5 > 0)
        {
            for (int i = 0; i < return5; i++)
            {
                AddTaro(5);
            }
            return5 = 0;
        }

        for(int i = 0; i < cpuTaros.Count; i++)
        {
            cpuTaros[i].color = invisibleColor;
            fieldTaros[i].color = invisibleColor;
        }

        if (skipCpuAfter == 1)
        {
            ChangeCpuTaros(21);
        }

        waitIAnim.SetTrigger("PlayerTurn");

        SoundManager.Instance.SFXPlay(soundClip.Clips[0]);

        yield return YieldCache.WaitForSeconds(1.5f);

        waitImage.gameObject.SetActive(false);

        if(skipPlayerTurn)
        {
            SkipTurn();
            skipPlayerTurn = false;
        }
    }

    public void SkipTurn()
    {
        StartCoroutine(CpuTurnStart());
    }

    public void LevelClear()
    {
        popUpText = "Clear!";

        clearUI.SetActive(true);

        SoundManager.Instance.SFXPlay(soundClip.Clips[2]);

        DataManager.Instance.SaveClearData(GameManager.Instance.selectedLevel);

        AchievementManager.Instance.AchivementTask();
    }

    //CpuTurn

    public IEnumerator CpuTurnStart()
    {
        if(StopAll)
        {
            yield break;
        }

        playerHand.GetComponent<GridField>().GetChilds();

        if(GameManager.Instance.CurrentLevelData.PlayerCards.Count == 0)
        {
            LevelClear();
            yield break;
        }

        GameManager.dragObject = null;

        if(taroGem9)
        {
            taroGem9 = false;

            yield break;
        }

        waitImage.gameObject.SetActive(true);

        waitIAnim.SetTrigger("CpuTurn");

        SoundManager.Instance.SFXPlay(soundClip.Clips[1]);

        yield return YieldCache.WaitForSeconds(1.5f);

        if (skipCpuAfter > 0)
        {
            skipCpuAfter--;
            if (skipCpuAfter <= 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    CpuSkip[i] = true;
                }
            }
        }

        StartCoroutine(CpuTurnAction());
    }

    private IEnumerator CpuTurnAction()
    {
        DragObject dummy = null;

        for(cpuX = 0; cpuX < 3; cpuX++)
        {
            if(StopAll)
            {
                yield break;
            }


            for(int j = 0; j < settingTransforms[4 + cpuX].childCount; j++)
            {

                dummy = settingTransforms[4 + cpuX].GetChild(0).GetComponent<DragObject>();

                if (cpuSkip[cpuX])
                {
                    cpuSkip[cpuX] = false;
                    break;
                }

                if (TaskCpu(dummy))
                {
                    
                    CpuUse(cpuX, dummy);


                    yield return YieldCache.WaitForSeconds(0.5f);

                    break;
                }

                yield return YieldCache.WaitForSeconds(0.25f);

                ChangeCpuHand(cpuX, true);

                yield return YieldCache.WaitForSeconds(0.25f);

            }
        }

        for(int i = 0; i < colorSkip.Count; i++)
        {
            colorSkip[i] = false;
        }

        StartCoroutine(PlayerTurnStart());
    }

    //CpuAction

    private bool TaskCpu(DragObject card)
    {
        if (colorSkip[(int)card.Info.color])
        {
            return false;
        }

        if( GetColorParent(card.Info.color).Count <= 0)
        {
            return true;
        }

        if(card.Info.num > settingTransforms[(int)card.Info.color].GetChild(settingTransforms[(int)card.Info.color].childCount - 1).GetComponent<DragObject>().Info.num)
        {
            return true;
        }

        return false;
    }

    private void CpuUse(int i , DragObject card)
    {
        SoundManager.Instance.SFXPlay(soundClip.Clips[4]);


        settingTransforms[4 + i].GetChild(0).SetParent(settingTransforms[(int)card.Info.color]);

        GetColorParent(card.Info.color).Add(card.Info);

        GetCpuParent((CardColor)i).RemoveAt(0);

        settingTransforms[(int)card.Info.color].GetComponent<GridField>().GetChilds();
        settingTransforms[(int)card.Info.color].GetComponent<FieldGridField>().SetGemSize();
        settingTransforms[4 + i].GetComponent<GridField>().GetChilds();

        if (settingTransforms[4 + cpuX].childCount <= 0)
        {
            LevelFailed();

            return;
        }

        TaskAb(card);

    }

    private void TaskAb(DragObject card)
    {
        if (card.Info.ab == true)
        {
            taroGemFunction.ActiveFunction(TaroGemFunction.LoadAt.CpuUse, card);
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

    public void ChangeCpuHand(int i , bool insertTrue)
    {
        if (insertTrue)
        {
            SoundManager.Instance.SFXPlay(soundClip.Clips[5]);

            if(settingTransforms[4 + i].GetChild(0).GetComponent<DragObject>().Info.ab == true)
            {
                taroGemFunction.ActiveFunction(TaroGemFunction.LoadAt.CpuTask, settingTransforms[4 + i].GetChild(0).GetComponent<DragObject>());
            }
        }

        GetCpuParent((CardColor)i).Add(GetCpuParent((CardColor)i)[0]);
        GetCpuParent((CardColor)i).RemoveAt(0);

        settingTransforms[4 + i].GetChild(0).SetAsLastSibling();

        settingTransforms[4 + i].GetComponent<GridField>().GetChilds();
    }

    public void LevelFailed()
    {
        if (clearUI.activeSelf)
            return;

        popUpText = "Failed...";

        clearUI.SetActive(true);

        SoundManager.Instance.SFXPlay(soundClip.Clips[3]);

        StopAll = true;
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
        taroAnim.Play(usedTaroObj.GetComponent<TaroInfo>().CardNum + "");

        SoundManager.Instance.SFXPlay(soundClip.Clips[6]);

        yield return YieldCache.WaitForSeconds(0.5f);

        taroFunction.taroFunctions[usedTaroObj.GetComponent<TaroInfo>().CardNum]();
        usedTaroObj.SetActive(false);

        PlayerHand.GetComponent<GridField>().GetChilds();
        TaroHand.GetComponent<GridField>().GetChilds();
        PlayGetChilds();

        yield return YieldCache.WaitForSeconds(0.5f);

        Destroy(usedTaroObj);

        usedTaroObj = null;

        taroAnim.gameObject.SetActive(false);
    }

    public void ChangeCpuTaros(CardColor color , int num)
    {
        cpuTaros[(int)color].sprite = ResourcesManager.Instance.Load<Sprite>("TaroImage/" + num);
        cpuTaros[(int)color].color = visibleColor;
    }

    public void ChangeCpuTaros(int num)
    {
        for(int i = 0; i < cpuTaros.Count; i++)
        {
            cpuTaros[i].sprite = ResourcesManager.Instance.Load<Sprite>("TaroImage/" + num);
            cpuTaros[i].color = visibleColor;
        }

        if(num == 11)
        {
            fieldTaros[(int)taroColor].color = visibleColor;
        }
    }

    public void PlayGetChilds()
    {
        for (int i = 0; i < 3; i++)
        {
            settingTransforms[i].GetComponent<GridField>().GetChilds();
        }
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
        SoundManager.Instance.SFXPlay(soundClip.Clips[7]);

        int i = settingTransforms[(int)taroColor].childCount;

        for (int j = 0; j < i; j++)
        {
            if(settingTransforms[(int)taroColor].GetChild(0).GetComponent<DragObject>().Info.ab)
            {
                taroGemFunction.ActiveFunction(TaroGemFunction.LoadAt.OnBreak, settingTransforms[(int)taroColor].GetChild(0).GetComponent<DragObject>());
            }

            settingTransforms[(int)taroColor].GetChild(0).SetParent(destroyGem);
        }
    }

    public void DestroyGems(CardColor color)
    {
        SoundManager.Instance.SFXPlay(soundClip.Clips[7]);

        int i = settingTransforms[(int)color].childCount;

        for (int j = 0; j < i; j++)
        {
            if (settingTransforms[(int)color].GetChild(0).GetComponent<DragObject>().Info.ab)
            {
                taroGemFunction.ActiveFunction(TaroGemFunction.LoadAt.OnBreak, settingTransforms[(int)color].GetChild(0).GetComponent<DragObject>());
            }

            settingTransforms[(int)color].GetChild(0).SetParent(destroyGem);
        }
    }


    public void DestroyGem(Transform gem , bool soundFalse = false)
    {
        if(!soundFalse)
            SoundManager.Instance.SFXPlay(soundClip.Clips[7]);

        if (gem.GetComponent<DragObject>().Info.ab)
        {
            taroGemFunction.ActiveFunction(TaroGemFunction.LoadAt.OnBreak, gem.GetComponent<DragObject>());
        }

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

        int i = 0;

        if (gem.ab == true)
        {
            i = 3;
            prefabs[(int)gem.color + i].GetComponent<GemInfo>().GemAbNum = gem.abNum;
        }


        generate = Instantiate(prefabs[(int)gem.color + i], destroyGem);

        generate.GetComponent<DragObject>().Info.abNum = gem.abNum;
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
        taroHand.GetComponent<GridField>().GetChilds();
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

    public void DropSound()
    {
        SoundManager.Instance.SFXPlay(soundClip.Clips[4]);
    }

    public void DestroySound()
    {
        SoundManager.Instance.SFXPlay(soundClip.Clips[7]);
    }
}
