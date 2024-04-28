using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaroGemFunction 
{
    private delegate void TaroGemDelegate(LoadAt loadAt);

    private List<TaroGemDelegate> taroGemFunctions;

    private DragObject targetObj;

    public TaroGemFunction()
    {
        taroGemFunctions = new List<TaroGemDelegate>();

        string methodName = "";

        for (int i = 0; i < 22; i++)
        {
            methodName = "TaroGem" + i;

            var method = GetType().GetMethod(methodName);

            if (method != null)
            {
                taroGemFunctions.Add((TaroGemDelegate)Delegate.CreateDelegate(typeof(TaroGemDelegate), this, method));
            }
        }
    }

    public enum LoadAt
    {
        OnBreak,
        Changed,
        PlayerUse,
        CpuUse,
        CpuTask
    }

    public void ActiveFunction(LoadAt loadAt, DragObject callObj)
    {
        targetObj = callObj;

        taroGemFunctions[callObj.Info.abNum](loadAt);
    }

    private void TaroGemGenerate(int num)
    {

        Card newCard = new Card();

        int i = (int)targetObj.Info.color + 1;

        if (i >= 3)
            i = 0;

        newCard.color = (CardColor)i;
        newCard.num = num;

        GameManager.Instance.PlayManager.GetColorParent(newCard.color).Add(newCard);

        GameManager.Instance.PlayManager.GenerateGem(newCard).transform.SetParent(GameManager.Instance.PlayManager.GetParentTransform(newCard.color));
    }


    public void TaroGem0(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.OnBreak:
                GameManager.Instance.PlayManager.LevelFailed();
                break;
            default: break;
        }
    }

    public void TaroGem1(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.PlayerUse:
            case LoadAt.CpuUse:

                targetObj.Info.num = 0;

                targetObj.GetComponentInChildren<TextMeshProUGUI>().text = "" + 0;

                break;

            default: break;
        }
    }

    public void TaroGem2(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.PlayerUse:
            case LoadAt.CpuUse:
                TaroGemGenerate(1);
                GameManager.Instance.PlayManager.PlayGetChilds();
                break;

            default: break;
        }
    }

    public void TaroGem3(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.PlayerUse:
            case LoadAt.CpuUse:
                TaroGemGenerate(6);
                GameManager.Instance.PlayManager.PlayGetChilds();
                break;
            default: break;
        }
    }

    public void TaroGem4(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.PlayerUse:
            case LoadAt.CpuUse:
                TaroGemGenerate(11);
                GameManager.Instance.PlayManager.PlayGetChilds();
                break;
            default: break;
        }
    }

    public void TaroGem5(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.PlayerUse:
            case LoadAt.CpuUse:
                if(targetObj.Info == targetObj.transform.parent.GetChild(0).GetComponent<DragObject>().Info)
                {
                    GameManager.Instance.PlayManager.LevelFailed();
                }
                break;

            case LoadAt.Changed:
                GameManager.Instance.PlayManager.LevelFailed();
                break;
        }
    }

    public void TaroGem6(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.PlayerUse:
                if (GameManager.Instance.PlayManager.PlayerHand.childCount > 0)
                {
                    GameManager.Instance.PlayManager.LevelFailed();
                }
                break;
            default: break;
        }
    }

    public void TaroGem7(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.PlayerUse:
            case LoadAt.CpuUse:
                GameManager.Instance.PlayManager.GetColorParent(targetObj.Info.color).RemoveAt(GameManager.Instance.PlayManager.GetParentTransform(targetObj.Info.color).childCount - 2);
                GameManager.Instance.PlayManager.DestroyGem(
                GameManager.Instance.PlayManager.GetParentTransform(targetObj.Info.color).GetChild(GameManager.Instance.PlayManager.GetParentTransform(targetObj.Info.color).childCount - 2));
                GameManager.Instance.PlayManager.PlayGetChilds();
                break;

            default: break;
        }
    }

    public void TaroGem8(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.PlayerUse:
            case LoadAt.CpuUse:

                targetObj.Info.num = 10;

                targetObj.GetComponentInChildren<TextMeshProUGUI>().text = "" + 10;

                break;

            default: break;
        }
    }

    public void TaroGem9(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.PlayerUse:
                GameManager.Instance.PlayManager.TaroGem9 = true;
                break;
            case LoadAt.CpuUse:
                GameManager.Instance.PlayManager.CpuX--;
                break;
            default: break;
        }
    }

    public void TaroGem10(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.PlayerUse:
                if (UnityEngine.Random.Range(0, 2) == 1)
                {
                    GameManager.Instance.PlayManager.AddTaro(10);
                }

                break;

            default: break;
        }
    }

    public void TaroGem11(LoadAt loadAt)
    {
        switch (loadAt)
        {
            default: break;
        }
    }

    public void TaroGem12(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.PlayerUse:
            case LoadAt.CpuUse:

                CardColor color = targetObj.Info.color;

                targetObj.transform.SetAsFirstSibling();
                GameManager.Instance.PlayManager.GetColorParent(color).Insert
                    (
                        0,
                        GameManager.Instance.PlayManager.GetColorParent(color)[GameManager.Instance.PlayManager.GetColorParent(color).Count - 1]
                    );
                GameManager.Instance.PlayManager.GetColorParent(color).RemoveAt(GameManager.Instance.PlayManager.GetColorParent(color).Count - 1);

                GameManager.Instance.PlayManager.PlayGetChilds();
                break;

            default: break;
        }
    }

    public void TaroGem13(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.PlayerUse:
                if (GameManager.Instance.PlayManager.PlayerHand.childCount <= 0)
                {
                    GameManager.Instance.PlayManager.LevelFailed();
                }

                break;

            default: break;
        }
    }

    public void TaroGem14(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.PlayerUse:
            case LoadAt.CpuUse:

                targetObj.Info.num = GameManager.Instance.PlayManager.GetParentTransform(targetObj.Info.color).childCount;

                targetObj.GetComponentInChildren<TextMeshProUGUI>().text = "" + targetObj.Info.num;
                break;

            default: break;
        }
    }

    public void TaroGem15(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.CpuTask:
                GameManager.Instance.PlayManager.LevelFailed();
                break;

            default: break;
        }
    }

    public void TaroGem16(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.CpuUse:
            case LoadAt.PlayerUse:

                int i = (int)targetObj.Info.color + 1;

                if (i >= 3)
                    i = 0;

                if (GameManager.Instance.PlayManager.GetParentTransform((CardColor)i).childCount > 0)
                {
                    GameManager.Instance.PlayManager.GetColorParent((CardColor)i).RemoveAt(GameManager.Instance.PlayManager.GetParentTransform((CardColor)i).childCount - 1);
                    GameManager.Instance.PlayManager.DestroyGem(
                    GameManager.Instance.PlayManager.GetParentTransform((CardColor)i).GetChild(GameManager.Instance.PlayManager.GetParentTransform((CardColor)i).childCount - 1));
                }
                GameManager.Instance.PlayManager.PlayGetChilds();
                break;
            default: break;
        }
    }

    public void TaroGem17(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.PlayerUse:
                GameManager.Instance.PlayManager.AddTaro(17);
                break;

            default: break;
        }
    }

    public void TaroGem18(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.PlayerUse:
                GameManager.Instance.PlayManager.AddTaro(18);
                break;

            default: break;
        }
    }

    public void TaroGem19(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.PlayerUse:
                GameManager.Instance.PlayManager.AddTaro(19);
                break;

            default: break;
        }
    }

    public void TaroGem20(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.OnBreak:
                GameManager.Instance.PlayManager.AddTaro(20);
                break;

            default: break;
        }
    }

    public void TaroGem21(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.PlayerUse:
                GameManager.Instance.PlayManager.SkipPlayerTurn = true;
                break;
            case LoadAt.CpuUse:
                GameManager.Instance.PlayManager.CpuSkip[GameManager.Instance.PlayManager.CpuX] = true;
                break;

            default: break;
        }
    }
}
