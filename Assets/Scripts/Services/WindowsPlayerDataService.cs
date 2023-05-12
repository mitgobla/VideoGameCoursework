using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WindowsPlayerDataService : IPlayerDataService
{
    private static string _savePath = Application.persistentDataPath + "/playerData.json";
    private static int _defaultPlayerCount = 3;
    private List<PlayerData> _playerDataList;

    private static int _currentPlayerID = 0;

    public void LoadData()
    {
        if (File.Exists(_savePath))
        {
            using (StreamReader streamReader = new StreamReader(_savePath))
            {
                string json = streamReader.ReadToEnd();
                PlayerDataCollection collection = JsonUtility.FromJson<PlayerDataCollection>(json);
                _playerDataList = collection.PlayerDataList;
                _currentPlayerID = collection.CurrentPlayerID;
            }
        } 
        else
        {
            CreateDefaultPlayerData();
        }
        if (_playerDataList.Count == 0)
        {
            CreateDefaultPlayerData();
        }
        Debug.Log("Loaded " + _playerDataList.Count + " players");
    }

    private void CreateDefaultPlayerData()
    {
        _playerDataList = new List<PlayerData>();
        // create three players
        for (int i = 0; i < _defaultPlayerCount; i++)
        {
            AddNewPlayerData();
        }
    }

    public PlayerData GetCurrentPlayerData()
    {
        return _playerDataList[_currentPlayerID];
    }

    public void SetCurrentPlayerID(int id)
    {
        _currentPlayerID = id;
    }

    public PlayerData GetPlayerData(int playerID)
    {
        return _playerDataList[playerID];
    }

    public void AddNewPlayerData()
    {
        PlayerData newPlayer = new PlayerData(_playerDataList.Count);
        AchievementLoader.Instance.GetAchievementItems().ForEach(achievement => newPlayer.achievements.Add(new AchievementPlayerData(achievement.AchievementID, false)));
        _playerDataList.Add(newPlayer);
        SaveAllData();
    }

    public void SaveAllData()
    {
        string json = JsonUtility.ToJson(new PlayerDataCollection(_playerDataList, _currentPlayerID));
        using (StreamWriter streamWriter = new StreamWriter(_savePath))
        {
            streamWriter.Write(json);
        }
    }

    public void SaveData(PlayerData data)
    {
        _playerDataList[data.playerID] = data;
        SaveAllData();
    }

    public List<PlayerData> GetPlayers()
    {
        return _playerDataList;
    }

    public void RemovePlayerData(int id)
    {
        if (_playerDataList.Count > 1)
        {
            _playerDataList.RemoveAt(id);
            SetCurrentPlayerID(_playerDataList.Count - 1);
            SaveAllData();
        }
    }

    public void UnlockAchievement(string achievementID)
    {
        PlayerData currentPlayerData = GetCurrentPlayerData();
        AchievementPlayerData achievementPlayerData = currentPlayerData.achievements.Find(achievement => achievement.AchievementID == achievementID);
        if (achievementPlayerData.AchievementUnlocked) { return; }
        achievementPlayerData.AchievementUnlocked = true;
        SaveData(currentPlayerData);
    }
}