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

    public List<List<Card>> GetSetting()
    {
        List<List<Card>> settingData = new List<List<Card>> 
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
