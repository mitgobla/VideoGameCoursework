using System;
using System.Collections.Generic;

[Serializable]
public class AchievementItemData
{

    public string AchievementID;
    public string AchievementName;
    public string AchievementDescription;

    public AchievementItemData(string achievementID, string achievementName, string achievementDescription)
    {
        AchievementID = achievementID;
        AchievementName = achievementName;
        AchievementDescription = achievementDescription;
    }
}