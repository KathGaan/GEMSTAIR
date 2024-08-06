using Steamworks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoSingletonManager<AchievementManager>
{
    [SerializeField] GameObject achievementUI;

    [SerializeField] Material material;

    [SerializeField] Transform achievements;

    [SerializeField] GameObject achievementClearUI;

    [SerializeField] SoundClip soundClip;

    private List<AchievementObject> achievementObjects;

    private void Start()
    {
        SetAchieveData();
    }

    public IEnumerator UpdateAchievementTask()
    {
        while (true)
        {
            switch (GameManager.Instance.selectedLevel)
            {
                case 29:
                    if (GameManager.Instance.PlayManager.TaroHand.childCount >= 4)
                    {
                        SaveAchievementData(GameManager.Instance.selectedLevel);
                        yield break;
                    }
                    break;
                case 34:
                    if(GameManager.Instance.PlayManager.PlayerHand.childCount >= 4)
                    {
                        if (GameManager.Instance.CurrentLevelData.PlayerCards[0].num == 1)
                        {
                            SaveAchievementData(GameManager.Instance.selectedLevel);
                            yield break;
                        }
                    }
                    break;

                default: yield break;
            }

            yield return null;
        }
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
            case 40:
            case 45:
            case 50:
            case 55:
            case 60:
                SaveAchievementData(i);
                break;
            case 4:
                if (GameManager.Instance.CurrentLevelData.BlueField.Count != 6)
                    return;
                for (int j = 0; j < 6; j++)
                {
                    if (GameManager.Instance.CurrentLevelData.BlueField[j].num != j)
                        return;
                }
                SaveAchievementData(i);
                break;
            case 12:
                if (GameManager.Instance.CurrentLevelData.BlueField.Count != 12)
                    return;
                for (int j = 0; j < 12; j++)
                {
                    if (GameManager.Instance.CurrentLevelData.BlueField[j].num != j)
                        return;
                }
                SaveAchievementData(i);
                break;

            case 14:
                if(GameManager.Instance.PlayManager.TaroHand.childCount == 1)
                {
                    SaveAchievementData(i);
                }
                break;

            default:
                break;
        }
    }    

    public void SaveAchievementData(int num)
    {
        DataManager.Instance.SaveAchievementData(num);

        if (SteamManager.Initialized)
        {
            SteamUserStats.SetAchievement("STAGE_" + num);

            SteamUserStats.StoreStats();
        }
    }

    public void ApearClearUI()
    {
        SoundManager.Instance.SFXPlay(soundClip.Clips[0]);

        achievementClearUI.transform.GetChild(0).GetComponent<Image>().sprite = ResourcesManager.Instance.Load<Sprite>("Achievement/STAGE_" + GameManager.Instance.selectedLevel);
        achievementClearUI.GetComponentInChildren<TextMeshProUGUI>().text = TextManager.Instance.LoadString("AchieveInfoText", GameManager.Instance.selectedLevel);
        StartCoroutine(ApearUI());
    }

    private IEnumerator ApearUI()
    {
        achievementClearUI.SetActive(true);

        achievementClearUI.GetComponent<Animator>().SetTrigger("Clear");

        yield return YieldCache.WaitForSeconds(4f);

        achievementClearUI.SetActive(false);
    }

}
