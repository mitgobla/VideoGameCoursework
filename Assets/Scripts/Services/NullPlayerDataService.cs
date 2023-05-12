using System.Collections.Generic;

public class NullPlayerDataService : IPlayerDataService
{
    public void AddNewPlayerData()
    {
        //
    }
    public void LoadData()
    {
        //
    }
    public void SaveAllData()
    {
        //
    }
    public void SaveData(PlayerData data)
    {
        //
    }

    public List<PlayerData> GetPlayers()
    {
        return new List<PlayerData>();
    }

    public PlayerData GetCurrentPlayerData()
    {
        return null;
    }

    public void SetCurrentPlayerID(int id)
    {
        //
    }

    public void RemovePlayerData(int id)
    {
        //
    }

    public void UnlockAchievement(string achievementID)
    {
        //
    }

    public PlayerData GetPlayerData(int playerID)
    {
        return null;
    }
}