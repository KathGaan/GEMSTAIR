using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TextManager : SingletonManager<TextManager>
{
    private Dictionary<string, List<Dictionary<string, object>>> preLoadCSV = new Dictionary<string, List<Dictionary<string, object>>>();

    public string LoadString(string FileName,int LoadIndex)
    {
        if(!preLoadCSV.ContainsKey(FileName))
        {
            preLoadCSV.Add(FileName, ResourcesManager.Instance.Read(FileName));
        }

        return preLoadCSV.GetValueOrDefault(FileName)[LoadIndex][DataManager.Instance.Data.Language].ToString();
    }
    
    public int GetCSVLength(string FileName)
    {
        if (!preLoadCSV.ContainsKey(FileName))
        {
            preLoadCSV.Add(FileName, ResourcesManager.Instance.Read(FileName));
        }

        return preLoadCSV.GetValueOrDefault(FileName).Count;
    }
}
