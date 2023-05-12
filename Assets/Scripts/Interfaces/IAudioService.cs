using UnityEngine;

public interface IAudioService : IGameService
{
    void SetSoundAudioSource(AudioSource source);

    void SetMusicAudioSource(AudioSource source);

    void PlaySound(AudioClip sound);

    void PlaySound(AudioClip sound, float volume);

    void StopSound(AudioClip sound);

    void StopAllSounds();

    void SetSoundVolume(float volume);

    void PlayMusic(AudioClip music);

    void PlayMusic(AudioClip music, float volume);

    void StopMusic(AudioClip music);

    void StopAllMusic();

    void SetMusicVolume(float volume);
}