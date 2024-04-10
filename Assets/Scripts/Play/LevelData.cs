using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData_",menuName = "Level/LevelData")]
public class LevelData : ScriptableObject
{
    [SerializeField] List<Card> redField;
    public List<Card> RedField
    {
        get { return redField; }
    }

    [SerializeField] List<Card> blueField;

    public List<Card> BlueField
    {
        get { return blueField; }
    }
    
    [SerializeField] List<Card> whiteField;
    
    public List<Card> WhiteField
    {
        get { return whiteField; }
    }

    [SerializeField] List<Card> playerCards;

    public List<Card> PlayerCards 
    { 
        get { return playerCards; }
    }

    [SerializeField] List<Card> cpu1Cards;

    public List<Card> Cpu1Cards
    {
        get { return cpu1Cards; }
    }

    [SerializeField] List<Card> cpu2Cards;

    public List<Card> Cpu2Cards
    {
        get { return cpu2Cards; }
    }

    [SerializeField] List<Card> cpu3Cards;

    public List<Card> Cpu3Cards
    {
        get { return cpu3Cards; }
    }

    [SerializeField] int taroCardNum;

    public int TaroCardNum
    {
        get { return taroCardNum; }
    }

    public void DeepCopy(LevelData cdata)
    {
        cdata.redField = new List<Card>();
        cdata.blueField = new List<Card>();
        cdata.whiteField = new List<Card>();
        cdata.playerCards = new List<Card>();
        cdata.cpu1Cards = new List<Card>();
        cdata.cpu2Cards = new List<Card>();
        cdata.cpu3Cards = new List<Card>();

        foreach (Card card in redField)
        {
            cdata.redField.Add(new Card(card));
        }
        foreach (Card card in blueField)
        {
            cdata.blueField.Add(new Card(card));
        }
        foreach (Card card in whiteField)
        {
            cdata.whiteField.Add(new Card(card));
        }
        foreach (Card card in playerCards)
        {
            cdata.playerCards.Add(new Card(card));
        }
        foreach (Card card in cpu1Cards)
        {
            cdata.cpu1Cards.Add(new Card(card));
        }
        foreach (Card card in cpu2Cards)
        {
            cdata.cpu2Cards.Add(new Card(card));
        }
        foreach (Card card in cpu3Cards)
        {
            cdata.cpu3Cards.Add(new Card(card));
        }

        cdata.taroCardNum = taroCardNum;
    }

    public List<List<Card>> GetSetting()
    {
        List<List<Card>> settingData = new List<List<Card>> ()
        { 
            redField,
            blueField,
            whiteField,
            playerCards,
            cpu1Cards,
            cpu2Cards,
            cpu3Cards 
        };

        return settingData;
    }
}
