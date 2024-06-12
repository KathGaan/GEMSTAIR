using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UniqueGemFunction
{
    static public CardColor PlacedColor;

    private delegate void UniqueGemDelegate(LoadAt loadAt);

    private List<UniqueGemDelegate> uniqueGemFunctions;

    private DragObject targetObj;

    public UniqueGemFunction()
    {
        uniqueGemFunctions = new List<UniqueGemDelegate>();

        string methodName = "";

        for(int i = 0; i < 13; i++)
        {
            methodName = "UniqueGem" + i;

            var method = GetType().GetMethod(methodName);

            if(method != null)
            {
                uniqueGemFunctions.Add((UniqueGemDelegate)Delegate.CreateDelegate(typeof(UniqueGemDelegate), this, method));
            }
            else
            {
                uniqueGemFunctions.Add(null);
            }
        }
    }

    private void UniqueGemGenerate()
    {

        Card newCard = new Card();

        int i = (int)PlacedColor;

        for (int j = 0; j < 3; j++)
        {
            if (i == j)
                continue;

            newCard.abNum = targetObj.Info.abNum;
            newCard.color = CardColor.None;
            newCard.num = targetObj.Info.num;

            GameManager.Instance.PlayManager.GetColorParent((CardColor)j).Add(newCard);

            GameManager.Instance.PlayManager.GenerateGem(newCard).transform.SetParent(GameManager.Instance.PlayManager.GetParentTransform((CardColor)j));
        }
    }
    public enum LoadAt
    {
        OnBreak,
        PlayerUse,
    }

    public void ActiveFunction(LoadAt loadAt, DragObject callObj)
    {
        targetObj = callObj;

        uniqueGemFunctions[callObj.Info.abNum](loadAt);
    }

    public void UniqueGem1(LoadAt loadAt)
    {
        switch(loadAt)
        {
            case LoadAt.PlayerUse:
                UniqueGemGenerate();
                GameManager.Instance.PlayManager.PlayGetChilds();
                break;

            default: break;
        }
    }

    public void UniqueGem2(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.PlayerUse:
                for(int i = 0; i < GameManager.Instance.PlayManager.GetColorParent(PlacedColor).Count; i++)
                {
                    if (GameManager.Instance.PlayManager.GetColorParent(PlacedColor)[i].ab == false)
                        continue;

                    GameManager.Instance.PlayManager.GetColorParent(PlacedColor)[i].abNum = 11;

                    GameManager.Instance.PlayManager.GetParentTransform(PlacedColor).GetChild(i).GetComponent<DragObject>().Info.abNum = 11;

                    GameManager.Instance.PlayManager.GetParentTransform(PlacedColor).GetChild(i).GetComponent<GemInfo>().GemAbNum = 11;

                    GameManager.Instance.PlayManager.GetParentTransform(PlacedColor).GetChild(i).GetComponent<GemInfo>().ImageSetting();
                }

                GameManager.Instance.PlayManager.PlayGetChilds();
                break;

            default: break;
        }
    }

    public void UniqueGem4(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.OnBreak:
                GameManager.Instance.CurrentLevelData.PlayerCards.Add(targetObj.Info);

                GameManager.Instance.PlayManager.GenerateGem(targetObj.Info).transform.SetParent(GameManager.Instance.PlayManager.PlayerHand);

                GameManager.Instance.PlayManager.PlayGetChilds();
                break;

            default: break;
        }
    }

    public void UniqueGem5(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.PlayerUse:
                break;

            default: break;
        }
    }

    public void UniqueGem6(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.PlayerUse:
                TaroGemFunction.BlockTaroGem = true;
                break;

            default: break;
        }
    }

    public void UniqueGem8(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.PlayerUse:
                GameManager.Instance.PlayManager.AddTaro(6);
                break;

            default: break;
        }
    }

    public void UniqueGem10(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.PlayerUse:
                GameManager.Instance.CurrentLevelData.PlayerCards.Add(GameManager.Instance.PlayManager.GetColorParent(PlacedColor)[0]);

                GameManager.Instance.PlayManager.GenerateGem(GameManager.Instance.PlayManager.GetColorParent(PlacedColor)[0]).transform.SetParent(GameManager.Instance.PlayManager.PlayerHand);

                GameManager.Instance.PlayManager.PlayGetChilds();
                break;

            default: break;
        }
    }

    public void UniqueGem11(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.PlayerUse:

                Card newCard = new Card(targetObj.Info);

                newCard.num--;

                if (newCard.num >= 0)
                {
                    GameManager.Instance.CurrentLevelData.PlayerCards.Add(newCard);

                    GameManager.Instance.PlayManager.GenerateGem(newCard).transform.SetParent(GameManager.Instance.PlayManager.PlayerHand);

                    GameManager.Instance.PlayManager.PlayGetChilds();
                }
                break;

            default: break;
        }
    }

    public void UniqueGem12(LoadAt loadAt)
    {
        switch (loadAt)
        {
            case LoadAt.OnBreak:
                GameManager.Instance.PlayManager.DontDestroy = true;

                GameManager.Instance.PlayManager.GetColorParent(PlacedColor).Add(targetObj.Info);

                break;

            default: break;
        }
    }
}
