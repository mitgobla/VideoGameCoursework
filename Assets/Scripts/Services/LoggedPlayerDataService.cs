using System.Collections.Generic;
using UnityEngine;

public class LoggedPlayerDataService : IPlayerDataService
{
    private IPlayerDataService _service;
    public LoggedPlayerDataService(IPlayerDataService service)
    {
        _service = service;
    }
    public void AddNewPlayerData()
    {
        Debug.Log("AddNewPlayerData");
        _service.AddNewPlayerData();
    }
    public void LoadData()
    {
        Debug.Log("LoadData");
        _service.LoadData();
    }
    public void SaveAllData()
    {
        Debug.Log("SaveAllData");
        _service.SaveAllData();
    }
    public void SaveData(PlayerData data)
    {
        Debug.Log("SaveData");
        _service.SaveData(data);
    }

    public List<PlayerData> GetPlayers()
    {
        Debug.Log("GetPlayers");
        return _service.GetPlayers();
    }

    public PlayerData GetCurrentPlayerData()
    {
        Debug.Log("GetCurrentPlayerData");
        return _service.GetCurrentPlayerData();
    }

    public void SetCurrentPlayerID(int id)
    {
        Debug.Log("SetCurrentPlayerID");
        _service.SetCurrentPlayerID(id);
    }

    public void RemovePlayerData(int id)
    {
        Debug.Log("RemovePlayerData");
        _service.RemovePlayerData(id);
    }

    public void UnlockAchievement(string achievementID)
    {
        Debug.Log("UnlockAchievement");
        _service.UnlockAchievement(achievementID);
    }

    public PlayerData GetPlayerData(int playerID)
    {
        Debug.Log("GetPlayerData");
        return _service.GetPlayerData(playerID);
    }
}