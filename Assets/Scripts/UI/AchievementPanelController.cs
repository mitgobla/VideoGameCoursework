using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchievementPanelController : MonoBehaviour
{
    [SerializeField] private GameObject _achievementItemTemplate;
    [SerializeField] private TMP_Dropdown _dropdown;

    [SerializeField] private GameObject _achievementPanel;

    private IPlayerDataService _playerDataService;
    private List<AchievementItemData> _achievementItems;
    private List<Transform> _achievementItemTemplates = new();

    private void OnEnable()
    {
        if (_playerDataService != null)
        {
            UpdateDropDown();
        }
    }

    private void Start()
    {
        _playerDataService = ServiceLocator.Instance.Get<IPlayerDataService>();

        UpdateDropDown();

        PlayerData playerData = _playerDataService.GetCurrentPlayerData();
        List<AchievementPlayerData> playerDataAchievements = playerData.achievements;

        _achievementItemTemplate.SetActive(false);

        _achievementItems = AchievementLoader.Instance.GetAchievementItems();

        foreach (AchievementItemData achievementData in _achievementItems)
        {
            GameObject achievementItemObject = Instantiate(_achievementItemTemplate, _achievementPanel.transform);
            achievementItemObject.SetActive(true);
            AchievementItem achievementItem = achievementItemObject.GetComponent<AchievementItem>();
            achievementItemObject.name = achievementData.AchievementID;
            achievementItem.SetAchievementID(achievementData.AchievementID);
            achievementItem.SetAchievementName(achievementData.AchievementName);
            achievementItem.SetAchievementDescription(achievementData.AchievementDescription);

            AchievementPlayerData playerAchievementData = playerDataAchievements.Find(achievement => achievement.AchievementID == achievementData.AchievementID);
            achievementItem.SetAchievementImage(playerAchievementData.AchievementUnlocked);
            _achievementItemTemplates.Add(achievementItemObject.transform);
        }

        _dropdown.onValueChanged.AddListener(delegate { OnDropDownChange(); });
    }

    private void PopulateAchievements(int playerID)
    {
        // for every child in the achievement panel, set the achievement image
        PlayerData playerData = _playerDataService.GetPlayerData(playerID);
        List<AchievementPlayerData> playerDataAchievements = playerData.achievements;

        foreach(Transform achievementItemObject in _achievementItemTemplates)
        {
            AchievementItem achievementItem = achievementItemObject.gameObject.GetComponent<AchievementItem>();
            AchievementPlayerData playerAchievementData = playerDataAchievements.Find(achievement => achievement.AchievementID == achievementItem.GetAchievementID());
            achievementItem.SetAchievementImage(playerAchievementData.AchievementUnlocked);
        }
    }

    private void UpdateDropDown()
    {
        List<PlayerData> playerDataList = _playerDataService.GetPlayers();
        _dropdown.ClearOptions();
        List<string> options = new List<string>();
        foreach (var playerData in playerDataList)
        {
            options.Add(playerData.playerName);
        }
        _dropdown.AddOptions(options);
    }

    private void OnDropDownChange()
    {
        PopulateAchievements(_dropdown.value);
    }



}
