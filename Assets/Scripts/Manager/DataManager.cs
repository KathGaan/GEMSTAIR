using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : SingletonManager<DataManager>
{
    public DataManager()
    {
        Data = new Data();

        string path = Path.Combine(Application.persistentDataPath, "Data.json");

        if (!File.Exists(path))
        {
            DefaultSetting();

            SaveData();
        }

        LoadData();
    }

    public Data Data;

    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(Data);

        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);

        string code = Convert.ToBase64String(bytes);

        string path = Path.Combine(Application.persistentDataPath, "Data.json");

        File.WriteAllText(path, code);
    }

    public void LoadData()
    {
        string path = Path.Combine(Application.persistentDataPath, "Data.json");

        string jsonData = File.ReadAllText(path);

        byte[] bytes = Convert.FromBase64String(jsonData);

        string code = System.Text.Encoding.UTF8.GetString(bytes);

        Data = JsonUtility.FromJson<Data>(code);
    }

    private void DefaultSetting()
    {
        Data.MasterVolume = 0.8f;
        Data.SFXVolume = 1f;
        Data.BGMVolume = 1f;
        Data.Language = "Korean";
    }

    public void SaveClearData(int num)
    {
        if(Data.ClearData.Count <= num)
        {
            Data.ClearData.Add(true);
        }
    }

    public void SaveAchievementData(int num)
    {
        if (Data.Achievement.Contains(num))
            return;

        Data.Achievement.Add(num);

        AchievementManager.Instance.ApearClearUI();
    }
}

[Serializable]
public class Data
{
    public float MasterVolume;
    public float SFXVolume;
    public float BGMVolume;

    public string Language;


    public List<bool> ClearData = new List<bool>();

    public List<int> Achievement = new List<int>();
}
