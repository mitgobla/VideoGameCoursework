using System;
using System.Collections.Generic;

[Serializable]
public class AchievementPlayerData
{

    public string AchievementID;
    public bool AchievementUnlocked;

    public AchievementPlayerData(string achievementID, bool achievementUnlocked)
    {
        AchievementID = achievementID;
        AchievementUnlocked = achievementUnlocked;
    }
}