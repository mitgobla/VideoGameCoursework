using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementItem : MonoBehaviour
{

    private string _achievementID;
    [SerializeField] private TMP_Text _achievementName;
    [SerializeField] private TMP_Text _achievementDescription;
    [SerializeField] private Image _achievementImage;

    [SerializeField] private Sprite _lockedSprite;
    [SerializeField] private Sprite _unlockedSprite;

    public void SetAchievementName(string achievementName)
    {
        _achievementName.text = achievementName;
    }

    public void SetAchievementDescription(string achievementDescription)
    {
        _achievementDescription.text = achievementDescription;
    }

    public void SetAchievementImage(bool isUnlocked)
    {
        if (isUnlocked)
        {
            _achievementImage.sprite = _unlockedSprite;
        }
        else
        {
            _achievementImage.sprite = _lockedSprite;
        }
    }

    public void SetAchievementID(string achievementID)
    {
        _achievementID = achievementID;
    }

    public string GetAchievementID()
    {
        return _achievementID;
    }
}
