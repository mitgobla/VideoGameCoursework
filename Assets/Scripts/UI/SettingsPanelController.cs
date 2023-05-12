using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsPanelController : MonoBehaviour
{
    [SerializeField] private TMP_Text musicVolumeLabel;
    [SerializeField] private TMP_Text sfxVolumeLabel;

    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    private IAudioService _audioService;

    private void Start()
    {
        _audioService = ServiceLocator.Instance.Get<IAudioService>();
        float prefMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        float prefSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        _audioService.SetMusicVolume(prefMusicVolume);
        _audioService.SetSoundVolume(prefSFXVolume);
        musicVolumeSlider.value = prefMusicVolume * 100;
        sfxVolumeSlider.value = prefSFXVolume * 100;
        Debug.Log(prefMusicVolume);
        UpdateMusicLabel();
        UpdateSFXLabel();

        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicSliderChanged(); });
        sfxVolumeSlider.onValueChanged.AddListener(delegate { OnSFXSliderChanged(); });
    }

    private void UpdateMusicLabel() 
    {
        musicVolumeLabel.text = musicVolumeSlider.value + "%";
    }

    private void UpdateSFXLabel()
    {
        sfxVolumeLabel.text = sfxVolumeSlider.value + "%";
    }

    private void OnMusicSliderChanged()
    {
        _audioService.SetMusicVolume(musicVolumeSlider.value / 100f);
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value / 100f);
        UpdateMusicLabel();
    }

    private void OnSFXSliderChanged()
    {
        _audioService.SetSoundVolume(sfxVolumeSlider.value / 100f);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value / 100f);
        UpdateSFXLabel();
    }

}
