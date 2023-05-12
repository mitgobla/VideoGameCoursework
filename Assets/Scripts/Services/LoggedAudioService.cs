using UnityEngine;

public class LoggedAudioService : IAudioService
{
    private readonly IAudioService _wrappedService;
    public LoggedAudioService(IAudioService wrappedService)
    {
        _wrappedService = wrappedService;
    }

    public void SetSoundAudioSource(AudioSource source)
    {
        Debug.Log("Setting sound audio source to " + source.name);
        _wrappedService.SetSoundAudioSource(source);
    }

    public void SetMusicAudioSource(AudioSource audioSource)
    {
        Debug.Log("Setting music audio source to " + audioSource.name);
        _wrappedService.SetMusicAudioSource(audioSource);
    }

    public void PlayMusic(AudioClip music)
    {
        Debug.Log("Playing music " + music);
        _wrappedService.PlayMusic(music);
    }

    public void PlayMusic(AudioClip music, float volume)
    {
        Debug.Log("Playing music" + music + " at volume " + volume);
        _wrappedService.PlayMusic(music, volume);
    }

    public void PlaySound(AudioClip sound)
    {
        Debug.Log("Playing sound " + sound);
        _wrappedService.PlaySound(sound);
    }

    public void PlaySound(AudioClip sound, float volume)
    {
        Debug.Log("Playing sound " + sound + " at volume " + volume);
        _wrappedService.PlaySound(sound, volume);
    }

    public void SetMusicVolume(float volume)
    {
        Debug.Log("Setting music volume to " + volume);
        _wrappedService.SetMusicVolume(volume);
    }

    public void SetSoundVolume(float volume)
    {
        Debug.Log("Setting sound volume to " + volume);
        _wrappedService.SetSoundVolume(volume);
    }

    public void StopAllMusic()
    {
        Debug.Log("Stopping all music");
        _wrappedService.StopAllMusic();
    }

    public void StopAllSounds()
    {
        Debug.Log("Stopping all sounds");
        _wrappedService.StopAllSounds();
    }

    public void StopMusic(AudioClip music)
    {
        Debug.Log("Stopping music " + music);
        _wrappedService.StopMusic(music);
    }

    public void StopSound(AudioClip sound)
    {
        Debug.Log("Stopping sound " + sound);
        _wrappedService.StopSound(sound);
    }
}