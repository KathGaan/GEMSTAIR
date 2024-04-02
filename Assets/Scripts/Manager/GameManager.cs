using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingletonManager<GameManager>
{
    public int selectedLevel;

    private LevelData currentLevelData;

    public LevelData CurrentLevelData 
    {
        get { return currentLevelData; }
    }

    private List<int> selectedTaroCards;

    public List<int> SelectedTaroCards
    {
        get { return selectedTaroCards; }
    }

    //PlayBefore
    public void LoadSelectedLevelData()
    {
        currentLevelData = ResourcesManager.Instance.LoadScript(selectedLevel);
    }

    public void SetTaroCards(List<int> cards)
    {
        selectedTaroCards.Clear();
        selectedTaroCards.AddRange(cards);
    }
}

[Serializable]
public class Card
{
    public int num;
    public CardColor color;
}

[Serializable]
public enum CardColor
{
    Red,
    Blue,
    White
}
