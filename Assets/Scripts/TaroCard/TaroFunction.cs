using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaroFunction
{
    public delegate void TaroDelegate();

    public List<TaroDelegate> taroFunctions;

    //Create
    public TaroFunction()
    {
        taroFunctions = new List<TaroDelegate>();

        string funName = "";

        for(int i = 0; i < 22; i++)
        {
            funName = "Taro" + i;

            var method = GetType().GetMethod(funName);

            if(method != null)
            {
                taroFunctions.Add((TaroDelegate)Delegate.CreateDelegate(typeof(TaroDelegate), this, method));
            }
        }
    }

    public void Taro0()
    {
        switch (GameManager.Instance.PlayManager.TaroColor)
        {
            case CardColor.Red:
                GameManager.Instance.CurrentLevelData.RedField.Clear();
                break;
            case CardColor.Blue:
                GameManager.Instance.CurrentLevelData.BlueField.Clear();
                break;
            case CardColor.White:
                GameManager.Instance.CurrentLevelData.WhiteField.Clear();
                break;
        }

        GameManager.Instance.PlayManager.DestroyGems();
    }

    public void Taro1()
    {
        GameManager.Instance.PlayManager.DestroyHands();

        List<CardColor> colors = new List<CardColor>();

        for (int i = 0; i < 3; i++)
        {
            colors.Add((CardColor)i);
        }

        switch (GameManager.Instance.PlayManager.TaroColor)
        {
            case CardColor.Red:
                colors.RemoveAt(0);
                break;
            case CardColor.Blue:
                colors.RemoveAt(1);
                break;
            case CardColor.White:
                colors.RemoveAt(2);
                break;
        }

        for(int i = 0; i < GameManager.Instance.CurrentLevelData.PlayerCards.Count; i++)
        {
            GameManager.Instance.CurrentLevelData.PlayerCards[i].color = colors[UnityEngine.Random.Range(0,2)];
            GameManager.Instance.PlayManager.GenerateGem(GameManager.Instance.CurrentLevelData.PlayerCards[i]).transform.SetParent(GameManager.Instance.PlayManager.PlayerHand);
        }
        
    }

    public void Taro2()
    {
        Card newCard = new Card();

        newCard.color = CardColor.Red;
        newCard.num = 11;

        GameManager.Instance.CurrentLevelData.RedField.Add(newCard);

        GameManager.Instance.PlayManager.GenerateGem(newCard).transform.SetParent(GameManager.Instance.PlayManager.GetParentTransform(newCard.color));
    }

    public void Taro3()
    {
        Card newCard = new Card();

        newCard.color = CardColor.Blue;
        newCard.num = 11;

        GameManager.Instance.CurrentLevelData.BlueField.Add(newCard);

        GameManager.Instance.PlayManager.GenerateGem(newCard).transform.SetParent(GameManager.Instance.PlayManager.GetParentTransform(newCard.color));
    }

    public void Taro4()
    {
        Card newCard = new Card();

        newCard.color = CardColor.White;
        newCard.num = 11;

        GameManager.Instance.CurrentLevelData.WhiteField.Add(newCard);

        GameManager.Instance.PlayManager.GenerateGem(newCard).transform.SetParent(GameManager.Instance.PlayManager.GetParentTransform(newCard.color));
    }

    public void Taro5()
    {
        GameManager.Instance.PlayManager.CpuSkip[0] = true;
    }

    public void Taro6()
    {
        GameManager.Instance.PlayManager.CpuSkip[1] = true;
    }

    public void Taro7()
    {
        GameManager.Instance.PlayManager.CpuSkip[2] = true;
    }

    public void Taro8()
    {
        GameManager.Instance.PlayManager.DestroyHands();

        for (int i = 0; i < GameManager.Instance.CurrentLevelData.PlayerCards.Count; i++)
        {
            GameManager.Instance.CurrentLevelData.PlayerCards[i].num++;
            GameManager.Instance.PlayManager.GenerateGem(GameManager.Instance.CurrentLevelData.PlayerCards[i]).transform.SetParent(GameManager.Instance.PlayManager.PlayerHand);
        }
    }

    public void Taro9()
    {
        if(GameManager.Instance.PlayManager.TaroHand.childCount > 0)
        {
            GameManager.Instance.PlayManager.CopyTaro();
        }
    }

    public void Taro10()
    {
        Card newCard = new Card();

        newCard.color = GameManager.Instance.PlayManager.TaroColor;
        newCard.num = UnityEngine.Random.Range(0,11);

        GameManager.Instance.PlayManager.GetColorParent(GameManager.Instance.PlayManager.TaroColor).Add(newCard);

        GameManager.Instance.PlayManager.GenerateGem(newCard).transform.SetParent(GameManager.Instance.PlayManager.GetParentTransform(newCard.color));
    }

    public void Taro11()
    {
        GameManager.Instance.PlayManager.ColorSkip[(int)GameManager.Instance.PlayManager.TaroColor] = true;
    }

    public void Taro12()
    {
        for(int i = 0; i < 3; i++)
        {
            if(GameManager.Instance.PlayManager.GetParentTransform((CardColor)i).childCount > 0)
            {
                GameManager.Instance.PlayManager.GetColorParent((CardColor)i).RemoveAt(GameManager.Instance.PlayManager.GetParentTransform((CardColor)i).childCount - 1);
                GameManager.Instance.PlayManager.DestroyGem(
                GameManager.Instance.PlayManager.GetParentTransform((CardColor)i).GetChild(GameManager.Instance.PlayManager.GetParentTransform((CardColor)i).childCount - 1));
            }
        }
    }

    public void Taro13()
    {
        Card newCard = new Card();

        newCard.color = GameManager.Instance.PlayManager.TaroColor;
        newCard.num = 1;

        GameManager.Instance.PlayManager.GetCpuParent(GameManager.Instance.PlayManager.TaroColor).Add(newCard);

        GameManager.Instance.PlayManager.GenerateGem(newCard).transform.SetParent(GameManager.Instance.PlayManager.GetCpuTransform(newCard.color));
    }

    public void Taro14()
    {
        GameManager.Instance.PlayManager.SkipTurn();

        for(int i = GameManager.Instance.PlayManager.TaroHand.childCount; i < 3;i++)
        {
            GameManager.Instance.PlayManager.AddTaro();
        }
    }

    public void Taro15()
    {
        for(int i = 0; i < 3; i++)
        {
            GameManager.Instance.PlayManager.ChangeCpuHand(i);
        }
    }

    public void Taro16()
    {
        Card newCard = new Card();

        newCard.color = GameManager.Instance.PlayManager.TaroColor;
        newCard.num = 11;

        GameManager.Instance.PlayManager.GetColorParent(GameManager.Instance.PlayManager.TaroColor).Add(newCard);

        GameManager.Instance.PlayManager.GenerateGem(newCard).transform.SetParent(GameManager.Instance.PlayManager.GetParentTransform(newCard.color));

        for(int i = 0; i < 3; i++)
        {
            if((CardColor)i != GameManager.Instance.PlayManager.TaroColor)
            {
                GameManager.Instance.PlayManager.GetColorParent((CardColor)i).Clear();
                GameManager.Instance.PlayManager.DestroyGems((CardColor)i);
            }
        }

    }

    public void Taro17()
    {
        GameManager.Instance.PlayManager.DestroyHands();

        for (int i = 0; i < GameManager.Instance.CurrentLevelData.PlayerCards.Count; i++)
        {
            GameManager.Instance.CurrentLevelData.PlayerCards[i].color = GameManager.Instance.PlayManager.TaroColor;
            GameManager.Instance.PlayManager.GenerateGem(GameManager.Instance.CurrentLevelData.PlayerCards[i]).transform.SetParent(GameManager.Instance.PlayManager.PlayerHand);
        }
    }

    public void Taro18()
    {
        GameManager.Instance.PlayManager.DestroyHands();

        for (int i = 0; i < GameManager.Instance.CurrentLevelData.PlayerCards.Count; i++)
        {
            GameManager.Instance.CurrentLevelData.PlayerCards[i].color = GameManager.Instance.PlayManager.TaroColor;
            GameManager.Instance.PlayManager.GenerateGem(GameManager.Instance.CurrentLevelData.PlayerCards[i]).transform.SetParent(GameManager.Instance.PlayManager.PlayerHand);
        }
    }

    public void Taro19()
    {
        GameManager.Instance.PlayManager.DestroyHands();

        for (int i = 0; i < GameManager.Instance.CurrentLevelData.PlayerCards.Count; i++)
        {
            GameManager.Instance.CurrentLevelData.PlayerCards[i].color = GameManager.Instance.PlayManager.TaroColor;
            GameManager.Instance.PlayManager.GenerateGem(GameManager.Instance.CurrentLevelData.PlayerCards[i]).transform.SetParent(GameManager.Instance.PlayManager.PlayerHand);
        }
    }

    public void Taro20()
    {
        if (GameManager.Instance.PlayManager.GetParentTransform(GameManager.Instance.PlayManager.TaroColor).childCount > 0)
        {
            GameManager.Instance.PlayManager.GetColorParent(GameManager.Instance.PlayManager.TaroColor).RemoveAt(GameManager.Instance.PlayManager.GetParentTransform(GameManager.Instance.PlayManager.TaroColor).childCount - 1);
            GameManager.Instance.PlayManager.DestroyGem(
            GameManager.Instance.PlayManager.GetParentTransform(GameManager.Instance.PlayManager.TaroColor).GetChild(GameManager.Instance.PlayManager.GetParentTransform(GameManager.Instance.PlayManager.TaroColor).childCount - 1));
        }

        if (UnityEngine.Random.Range(0, 4) != 2)
        {
            GameManager.Instance.PlayManager.Return20 = true;
        }
    }

    public void Taro21()
    {
        for(int i = 0; i < 3; i ++)
        {
            GameManager.Instance.PlayManager.CpuSkip[i] = true;
        }

        GameManager.Instance.PlayManager.SkipPlayer = 2;
    }
}
