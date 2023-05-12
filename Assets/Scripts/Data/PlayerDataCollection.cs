using System;
using System.Collections.Generic;

[Serializable]
public class PlayerDataCollection
{
    public List<PlayerData> PlayerDataList;
    public int CurrentPlayerID;

    public PlayerDataCollection(List<PlayerData> playerDataList, int currentPlayerID)
    {
        PlayerDataList = playerDataList;
        CurrentPlayerID = currentPlayerID;
    }
}