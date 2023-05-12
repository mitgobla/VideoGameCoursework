using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class AchievementLoader : Singleton<AchievementLoader>
{
    private static string achievementPath = "achievements";
    private static List<AchievementItemData> achievements = new List<AchievementItemData>();

    private void Awake()
    {
        if (achievements.Count == 0)
        { 
            LoadAchievements();
        }
    }

    private void LoadAchievements()
    {
        achievements.Clear();
        TextAsset textAsset = Resources.Load<TextAsset>(achievementPath);
        string json = textAsset.text;
        Debug.Log(json);

        AchievementResource achievementResource = JsonUtility.FromJson<AchievementResource>(json);
        achievements = achievementResource.achievements;
        Debug.Log("Loaded " + achievements.Count + " achievements from resource");
    }

    public List<AchievementItemData> GetAchievementItems()
    {
        return achievements;
    }
}