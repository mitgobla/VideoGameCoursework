using System.Collections.Generic;
using UnityEngine;

public class WindowsAudioService : IAudioService
{
    private static AudioSource _soundAudioSource;
    private static AudioSource _musicAudioSource;

    private float _soundVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
    private float _musicVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
    
    public void PlayMusic(AudioClip music)
    {
        PlayMusic(music, _musicVolume);
    }

    public void PlayMusic(AudioClip music, float volume)
    {
        if (_musicAudioSource.isPlaying)
        {
            _musicAudioSource.Stop();
        }
        _musicAudioSource.clip = music;
        _musicAudioSource.volume = volume;
        _musicAudioSource.loop = true;
        _musicAudioSource.Play();
    }

    public void PlaySound(AudioClip sound)
    {
        PlaySound(sound, _soundVolume);
    }

    public void PlaySound(AudioClip sound, float volume)
    {
        _soundAudioSource.clip = sound;
        _soundAudioSource.volume = volume;
        _soundAudioSource.Play();
    }

    public void SetMusicAudioSource(AudioSource source)
    {
        _musicAudioSource = source;
    }

    public void SetMusicVolume(float volume)
    {
        _musicVolume = volume;
        _musicAudioSource.volume = volume;
    }

    public void SetSoundAudioSource(AudioSource source)
    {
        _soundAudioSource = source;
    }

    public void SetSoundVolume(float volume)
    {
        _soundVolume = volume;
        _soundAudioSource.volume = volume;
    }

    public void StopAllMusic()
    {
        if (_musicAudioSource.isPlaying)
        {
            _musicAudioSource.Stop();
        }
    }

    public void StopAllSounds()
    {
        throw new System.NotImplementedException();
    }

    public void StopMusic(AudioClip music)
    {
        throw new System.NotImplementedException();
    }

    public void StopSound(AudioClip sound)
    {
        throw new System.NotImplementedException();
    }
}