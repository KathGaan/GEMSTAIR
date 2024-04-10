using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TaroDataBase", menuName = "Taro/DataBase")]
public class TaroDataBase : ScriptableObject
{
    [SerializeField] List<Card> taroDatas;

    public List<Card> TaroDatas
    {
        get { return taroDatas; }
    }
}
