using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class LeaderboardPanelController : MonoBehaviour
{
    [SerializeField] private GameObject _leaderboardItemTemplate;
    [SerializeField] private GameObject _leaderboardPanel;

    private IPlayerDataService _playerDataService;

    private void Start()
    {
        _playerDataService = ServiceLocator.Instance.Get<IPlayerDataService>();
        UpdateLeaderboard();
    }

    private void OnEnable()
    {
        if (_playerDataService != null)
        {
            UpdateLeaderboard();
        }
    }

    private void OnDisable()
    {
        foreach (Transform child in _leaderboardPanel.transform)
        {
            if (child.gameObject.activeSelf)
            {
                Destroy(child.gameObject);
            }

        }
    }

    private void UpdateLeaderboard()
    {
        _leaderboardItemTemplate.SetActive(false);
        List<PlayerData> playerData = _playerDataService.GetPlayers();
        playerData.Sort(SortByHighScore);

        for (int i = 0; i < playerData.Count; i++)
        {
            GameObject leaderboardItemObject = Instantiate(_leaderboardItemTemplate, _leaderboardPanel.transform);
            leaderboardItemObject.SetActive(true);
            LeaderboardItem leaderboardItem = leaderboardItemObject.GetComponent<LeaderboardItem>();
            Debug.Log(playerData[i].playerID + " highscore " + playerData[i].highScore);
            leaderboardItem.SetLeaderboardScore("Highscore: " + playerData[i].highScore.ToString());
            leaderboardItem.SetLeaderboardColor(new Color(playerData[i].playerColor.r, playerData[i].playerColor.g, playerData[i].playerColor.b));
            int rank = i + 1;
            leaderboardItem.SetLeaderboardRank("#" + rank);
            leaderboardItem.SetLeaderboardName(playerData[i].playerName);
        }
    }

    private static int SortByHighScore(PlayerData p1, PlayerData p2)
    {
        return p2.highScore.CompareTo(p1.highScore);
    }
}