using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColor : MonoBehaviour
{

    [SerializeField] private Image _image;
    private IPlayerDataService _playerDataService;

    private void Awake()
    {
        _playerDataService = ServiceLocator.Instance.Get<IPlayerDataService>();
    }

    void Start()
    {
        
        PlayerData playerData = _playerDataService.GetCurrentPlayerData();
        _image.color = new Color(playerData.playerColor.r, playerData.playerColor.g, playerData.playerColor.b);
    }
}
