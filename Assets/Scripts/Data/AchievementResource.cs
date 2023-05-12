using System;
using System.Collections.Generic;

[Serializable]
public class AchievementResource
{
    public List<AchievementItemData> achievements;

    public AchievementResource(List<AchievementItemData> achievements)
    {
        this.achievements = achievements;
    }
}