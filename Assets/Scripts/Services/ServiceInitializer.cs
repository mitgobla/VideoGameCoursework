using UnityEngine;

public class ServiceInitializer : MonoBehaviour
{

    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource soundAudioSource;
    private void Awake()
    {
        ServiceLocator.Initialize();

        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            WindowsAudioService audioService = new WindowsAudioService();
            float prefMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            float prefSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
            musicAudioSource.volume = prefMusicVolume;
            soundAudioSource.volume = prefSFXVolume;
            audioService.SetSoundAudioSource(soundAudioSource);
            audioService.SetMusicAudioSource(musicAudioSource);
            
            audioService.SetMusicVolume(prefMusicVolume);
            audioService.SetSoundVolume(prefSFXVolume);
            ServiceLocator.Instance.Register<IAudioService>(audioService);

            WindowsPlayerDataService playerDataService = new WindowsPlayerDataService();
            ServiceLocator.Instance.Register<IPlayerDataService>(new LoggedPlayerDataService(playerDataService));
            playerDataService.LoadData();
        } 
        else
        {
            NullAudioService service = new NullAudioService();
            ServiceLocator.Instance.Register<IAudioService>(new LoggedAudioService(service));

            NullPlayerDataService playerDataService = new NullPlayerDataService();
            ServiceLocator.Instance.Register<IPlayerDataService>(new LoggedPlayerDataService(playerDataService));
        }
    }
} 