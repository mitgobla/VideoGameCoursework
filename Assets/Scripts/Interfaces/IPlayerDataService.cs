using System.Collections.Generic;

public interface IPlayerDataService : IGameService
{
    void SaveData(PlayerData data);

    void SaveAllData();

    void LoadData();
    void AddNewPlayerData();

    void RemovePlayerData(int id);

    List<PlayerData> GetPlayers();
    PlayerData GetCurrentPlayerData();
    void SetCurrentPlayerID(int id);

    void UnlockAchievement(string achievementID);
    PlayerData GetPlayerData(int playerID);
}