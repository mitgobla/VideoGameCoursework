using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProfilePanelController : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private Image _image;
    [SerializeField] private Slider _redSlider;
    [SerializeField] private Slider _greenSlider;
    [SerializeField] private Slider _blueSlider;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _addButton;
    [SerializeField] private Button _deleteButton;
    [SerializeField] private Button _saveButton;

    private IPlayerDataService _playerDataService;

    private void Awake()
    {
        _playerDataService = ServiceLocator.Instance.Get<IPlayerDataService>();
    }

    private void Start()
    {
        UpdateDropDown();
        UpdateElements();

        _redSlider.onValueChanged.AddListener(delegate { OnRedSliderChange(); });
        _greenSlider.onValueChanged.AddListener(delegate { OnGreenSliderChange(); });
        _blueSlider.onValueChanged.AddListener(delegate { OnBlueSliderChange(); });

        _closeButton.onClick.AddListener(delegate { OnCloseButton(); });
        _addButton.onClick.AddListener(delegate { OnAddButton(); });
        _deleteButton.onClick.AddListener(delegate { OnDeleteButton(); });
        _saveButton.onClick.AddListener(delegate { OnSaveButton(); });

        _dropdown.onValueChanged.AddListener(delegate { OnDropDownChange(); });
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
        UpdateElements();
    }

    private void UpdateElements()

    {
        PlayerData currentPlayerData = _playerDataService.GetCurrentPlayerData();
        _dropdown.value = currentPlayerData.playerID;

        _redSlider.value = currentPlayerData.playerColor.r;
        _greenSlider.value = currentPlayerData.playerColor.g;
        _blueSlider.value = currentPlayerData.playerColor.b;

        UpdateSpriteColor();

        _inputField.text = currentPlayerData.playerName;
    }

    private void UpdateSpriteColor()
    {
        _image.color = new Color(_redSlider.value, _greenSlider.value, _blueSlider.value);
    }

    private void OnDropDownChange()
    {
        SaveCurrentPlayer();
        _playerDataService.SetCurrentPlayerID(_dropdown.value);
        UpdateElements();
    }

    public void OnRedSliderChange()
    {
        UpdateSpriteColor();
    }

    public void OnGreenSliderChange()
    {
        UpdateSpriteColor();
    }

    public void OnBlueSliderChange()
    {
        UpdateSpriteColor();
    }

    public void OnCloseButton()
    {
        SaveCurrentPlayer();
        gameObject.SetActive(false);
    }

    public void OnSaveButton()
    {
        SaveCurrentPlayer();
        UpdateDropDown();
    }

    public void SaveCurrentPlayer()
    {
        PlayerData currentPlayerData = _playerDataService.GetCurrentPlayerData();
        currentPlayerData.playerName = ValidateInputField();
        currentPlayerData.playerColor.r = _redSlider.value;
        currentPlayerData.playerColor.g = _greenSlider.value;
        currentPlayerData.playerColor.b = _blueSlider.value;
        _playerDataService.SaveData(currentPlayerData);
    }

    public void OnAddButton()
    {
        _playerDataService.AddNewPlayerData();
        UpdateDropDown();
        UpdateElements();
        _dropdown.value = _dropdown.options.Count - 1;
    }

    public void OnDeleteButton()
    {
        if (_dropdown.options.Count == 1)
        {
            return;
        }

        _playerDataService.RemovePlayerData(_dropdown.value);
        UpdateDropDown();
        UpdateElements();
    }

    public string ValidateInputField()
    {
        // check if the input field is empty or only contains spaces
        if (_inputField.text.Length == 0 || _inputField.text.Trim().Length == 0)
        {
            return "Player " + (_dropdown.value + 1);
        }
        else
        {
            return _inputField.text;
        }
    }

}
