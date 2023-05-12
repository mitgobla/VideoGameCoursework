using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public int playerID;
    public string playerName;
    public ColorData playerColor;
    public int score;
    public int highScore;
    public int lives;
    public bool[] homesOccupied;
    public List<AchievementPlayerData> achievements;

    public PlayerData(int playerID, string playerName, ColorData playerColor, int score, int highScore, int lives, bool[] homesOccupied, List<AchievementPlayerData> achievements)
    {
        this.playerID = playerID;
        this.playerColor = playerColor;
        this.playerName = playerName;
        this.score = score;
        this.highScore = highScore;
        this.lives = lives;
        this.homesOccupied = homesOccupied;
        this.achievements = achievements;
    }

    public PlayerData(int playerID)
    {
        this.playerID = playerID;
        this.playerColor = new ColorData();
        this.playerName = "Player " + (playerID + 1);
        this.score = 0;
        this.highScore = 0;
        this.lives = 3;
        this.homesOccupied = new bool[0];
        this.achievements = new List<AchievementPlayerData>();
    }
}