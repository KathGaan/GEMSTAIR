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

        for (int i = 0; i < GameManager.Instance.CurrentLevelData.PlayerCards.Count; i++)
        {
            if (--GameManager.Instance.CurrentLevelData.PlayerCards[i].num < 0)
            {
                GameManager.Instance.CurrentLevelData.PlayerCards.RemoveAt(i--);
            }
            else
            {
                GameManager.Instance.PlayManager.GenerateGem(GameManager.Instance.CurrentLevelData.PlayerCards[i]).transform.SetParent(GameManager.Instance.PlayManager.PlayerHand);
            }
        }

        if (GameManager.Instance.CurrentLevelData.PlayerCards.Count == 0)
        {
            GameManager.Instance.PlayManager.LevelClear();
        }
    }

    public void Taro2()
    {
        Card newCard = new Card();

        newCard.color = GameManager.Instance.PlayManager.TaroColor;
        newCard.num = 1;

        GameManager.Instance.PlayManager.GetColorParent(newCard.color).Add(newCard);

        GameManager.Instance.PlayManager.GenerateGem(newCard).transform.SetParent(GameManager.Instance.PlayManager.GetParentTransform(newCard.color));
    }

    public void Taro3()
    {
        Card newCard = new Card();

        newCard.color = GameManager.Instance.PlayManager.TaroColor;
        newCard.num = 6;

        GameManager.Instance.PlayManager.GetColorParent(newCard.color).Add(newCard);

        GameManager.Instance.PlayManager.GenerateGem(newCard).transform.SetParent(GameManager.Instance.PlayManager.GetParentTransform(newCard.color));
    }

    public void Taro4()
    {
        Card newCard = new Card();

        newCard.color = GameManager.Instance.PlayManager.TaroColor;
        newCard.num = 11;

        GameManager.Instance.PlayManager.GetColorParent(newCard.color).Add(newCard);

        GameManager.Instance.PlayManager.GenerateGem(newCard).transform.SetParent(GameManager.Instance.PlayManager.GetParentTransform(newCard.color));
    }

    public void Taro5()
    {
        if (GameManager.Instance.PlayManager.GetParentTransform(GameManager.Instance.PlayManager.TaroColor).childCount > 0)
        {
            GameManager.Instance.PlayManager.GetColorParent(GameManager.Instance.PlayManager.TaroColor).Add(GameManager.Instance.PlayManager.GetColorParent(GameManager.Instance.PlayManager.TaroColor)[0]);
            GameManager.Instance.PlayManager.GetColorParent(GameManager.Instance.PlayManager.TaroColor).RemoveAt(0);
            GameManager.Instance.PlayManager.GetParentTransform(GameManager.Instance.PlayManager.TaroColor).GetChild(0).SetAsLastSibling();
            if(GameManager.Instance.PlayManager.GetParentTransform(GameManager.Instance.PlayManager.TaroColor).GetChild(0).GetComponent<DragObject>().Info.ab)
            {
                GameManager.Instance.PlayManager.TaroGemFunction.ActiveFunction(TaroGemFunction.LoadAt.Changed, GameManager.Instance.PlayManager.GetParentTransform(GameManager.Instance.PlayManager.TaroColor).GetChild(0).GetComponent<DragObject>());
            }
        }

        CardColor j = GameManager.Instance.CurrentLevelData.PlayerCards[0].color;

        int k = GameManager.Instance.CurrentLevelData.PlayerCards[0].abNum;

        GameManager.Instance.PlayManager.Return5 += 1;

        for (int i = 0; i < GameManager.Instance.CurrentLevelData.PlayerCards.Count; i++)
        { 
            if(j != GameManager.Instance.CurrentLevelData.PlayerCards[i].color)
            {
                GameManager.Instance.PlayManager.Return5 = 0;
                break;
            }
            else if(j == CardColor.None && k != GameManager.Instance.CurrentLevelData.PlayerCards[i].abNum)
            {
                GameManager.Instance.PlayManager.Return5 = 0;
                break;
            }
        }
    }

    public void Taro6()
    {
        if (GameManager.Instance.CurrentLevelData.PlayerCards.Count == 1)
        {
            GameManager.Instance.PlayManager.DestroyHands();
            GameManager.Instance.CurrentLevelData.PlayerCards[0].num = 12;
            GameManager.Instance.PlayManager.GenerateGem(GameManager.Instance.CurrentLevelData.PlayerCards[0]).transform.SetParent(GameManager.Instance.PlayManager.PlayerHand);
        }
    }

    public void Taro7()
    {
        GameManager.Instance.PlayManager.CpuSkip[(int)GameManager.Instance.PlayManager.TaroColor] = true;

        GameManager.Instance.PlayManager.ChangeCpuTaros(GameManager.Instance.PlayManager.TaroColor, 7);
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
        for (int i = 0; i < 3; i++)
        {
            Card newCard = new Card();

            newCard.color = (CardColor)i;
            newCard.num = UnityEngine.Random.Range(0, 11);

            GameManager.Instance.PlayManager.GetColorParent(newCard.color).Add(newCard);

            GameManager.Instance.PlayManager.GenerateGem(newCard).transform.SetParent(GameManager.Instance.PlayManager.GetParentTransform(newCard.color));
        }
    }

    public void Taro11()
    {
        GameManager.Instance.PlayManager.ColorSkip[(int)GameManager.Instance.PlayManager.TaroColor] = true;
        GameManager.Instance.PlayManager.ChangeCpuTaros(11);
    }

    public void Taro12()
    {
        GameManager.Instance.PlayManager.DestroySound();

        for (int i = 0; i < 3; i++)
        {
            if(GameManager.Instance.PlayManager.GetParentTransform((CardColor)i).childCount > 0)
            {
                GameManager.Instance.PlayManager.GetColorParent((CardColor)i).RemoveAt(GameManager.Instance.PlayManager.GetParentTransform((CardColor)i).childCount - 1);
                GameManager.Instance.PlayManager.DestroyGem
                    (
                        GameManager.Instance.PlayManager.GetParentTransform((CardColor)i).GetChild(GameManager.Instance.PlayManager.GetParentTransform((CardColor)i).childCount - 1)
                        , (CardColor)i
                        , true
                    );
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

        GameManager.Instance.PlayManager.GetCpuTransform(newCard.color).GetComponent<GridField>().GetChilds();
    }

    public void Taro14()
    {
        TaroGemFunction.BlockTaroGem += 2;
        GameManager.Instance.PlayManager.VisualBlockTaroGem();
    }

    public void Taro15()
    {
        for(int i = 0; i < 3; i++)
        {
            GameManager.Instance.PlayManager.ChangeCpuHand(i , false);
        }
    }

    public void Taro16()
    {
        Card newCard = new Card();

        newCard.color = GameManager.Instance.PlayManager.TaroColor;
        newCard.num = 11;

        GameManager.Instance.PlayManager.GetColorParent(GameManager.Instance.PlayManager.TaroColor).Add(newCard);

        GameManager.Instance.PlayManager.GenerateGem(newCard).transform.SetParent(GameManager.Instance.PlayManager.GetParentTransform(newCard.color));

        int i = (int)newCard.color + 1;

        if (i >= 3)
            i = 0;

        GameManager.Instance.PlayManager.GetColorParent((CardColor)i).Clear();
        GameManager.Instance.PlayManager.DestroyGems((CardColor)i);

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
            GameManager.Instance.PlayManager.GetParentTransform(GameManager.Instance.PlayManager.TaroColor).GetChild(GameManager.Instance.PlayManager.GetParentTransform(GameManager.Instance.PlayManager.TaroColor).childCount - 1)
            ,GameManager.Instance.PlayManager.TaroColor
            );
        }

        if (UnityEngine.Random.Range(0, 4) != 2)
        {
            GameManager.Instance.PlayManager.Return20 += 1;
        }
    }

    public void Taro21()
    {
        GameManager.Instance.PlayManager.SkipTurn();

        GameManager.Instance.PlayManager.SkipCpuAfter = 2;
    }
}
