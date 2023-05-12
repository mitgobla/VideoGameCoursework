using TMPro;
using UnityEngine;

public class AddressText : MonoBehaviour
{

    [SerializeField] private TMP_Text addressText;

    private IPlayerDataService playerDataService;

    public void Start()
    {
        playerDataService = ServiceLocator.Instance.Get<IPlayerDataService>();
        SetAddressText(playerDataService.GetCurrentPlayerData().playerName);
    }

    public void SetAddressText(string address)
    {
        string format = "C:\\Users\\{0}\\files";
        address = string.Format(format, address);
        addressText.SetText(address);
    }
}
