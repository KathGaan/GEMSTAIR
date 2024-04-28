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

    private List<int> selectedTaroCards = new List<int>();

    public List<int> SelectedTaroCards
    {
        get { return selectedTaroCards; }
    }

    private PlayManager playManager;

    public PlayManager PlayManager
    {
        set { playManager = value; }
        get { return playManager; }
    }

    static public GameObject dragObject;

    private bool usedTaro;

    public bool UsedTaro
    {
        set { usedTaro = value; }
        get { return usedTaro; }
    }

    //PlayBefore
    public void LoadSelectedLevelData()
    {
        LevelData levelData = ScriptableObject.CreateInstance<LevelData>();

        ResourcesManager.Instance.LoadScript(selectedLevel).DeepCopy(levelData);

        currentLevelData = levelData;
    }

    public void SetTaroCards(List<int> cards)
    {
        selectedTaroCards.Clear();
        selectedTaroCards.AddRange(cards);
        playManager.StartTaroSetting();
    }
}

[Serializable]
public class Card
{
    public int num;
    public CardColor color;

    public bool ab;
    public int abNum;

    public Card() { }

    public Card(Card other)
    {
        this.color = other.color;
        this.num = other.num;
        this.ab = other.ab;
        this.abNum = other.abNum;
    }
}

[Serializable]
public enum CardColor
{
    Red,
    Blue,
    White,
    None
}
