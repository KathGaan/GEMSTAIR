using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoSingletonManager<AchievementManager>
{
    [SerializeField] GameObject achievementUI;

    [SerializeField] Material material;

    [SerializeField] Transform achievements;

    private List<AchievementObject> achievementObjects;

    private void Start()
    {
        SetAchieveData();
    }

    private void SetAchieveData()
    {
        achievementObjects = new List<AchievementObject>();

        for(int i = 0; i < achievements.childCount; i++)
        {
            achievementObjects.Add(achievements.GetChild(i).GetComponent<AchievementObject>());
        }
    }

    private void SettingAchieveImage()
    {
        for(int i = 0; i < achievements.childCount; i++)
        {
            if (DataManager.Instance.Data.Achievement.Contains(achievementObjects[i].AchieveNum))
            {
                achievementObjects[i].Cleared();
            }
            else
            {
                achievementObjects[i].SetGrayImage(material);
            }
        }
    }

    public void OpenUI()
    {
        SoundManager.Instance.ButtonSound();

        SettingAchieveImage();

        achievementUI.SetActive(true);
    }

    public void CloseUI()
    {
        SoundManager.Instance.ButtonSound();

        achievementUI.SetActive(false);
    }

    public void AchivementTask()
    {
        int i = GameManager.Instance.selectedLevel;

        switch (i)
        {
            case 10:
            case 20:
            case 25:
            case 30:
            case 35:
                DataManager.Instance.SaveAchievementData(i);
                
                if(SteamManager.Initialized)
                {
                    SteamUserStats.SetAchievement("STAGE_" + i);

                    SteamUserStats.StoreStats();
                }
                break;

            default:
                break;
        }
    }    

}
