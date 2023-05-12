using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoader : MonoBehaviour
{
    
    private IAudioService _AudioService;

    [SerializeField] private AudioClip menuMusic;

    private void Start()
    {
        _AudioService = ServiceLocator.Instance.Get<IAudioService>();
        _AudioService.PlayMusic(menuMusic);
    }
    
}
