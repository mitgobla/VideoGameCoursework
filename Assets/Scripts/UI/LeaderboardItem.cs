using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardItem : MonoBehaviour
{

    private string _achievementID;
    [SerializeField] private TMP_Text _leaderboardName;
    [SerializeField] private TMP_Text _leaderboardRank;
    [SerializeField] private TMP_Text _leaderboardScore;
    [SerializeField] private Image _leaderboardImage;

    public void SetLeaderboardName(string leaderboardName)
    {
        _leaderboardName.text = leaderboardName;
    }

    public void SetLeaderboardScore(string leaderboardScore)
    {
        _leaderboardScore.text = leaderboardScore;
    }

    public void SetLeaderboardRank(string leaderboardRank)
    {
        _leaderboardRank.text = leaderboardRank;
    }

    public void SetLeaderboardColor(Color color)
    {
        _leaderboardImage.color = color;
    }
}
