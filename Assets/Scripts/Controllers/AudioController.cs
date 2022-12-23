using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    [SerializeField] private AudioClip _tapClip;
    [SerializeField] private AudioClip _clickClip;
    [SerializeField] private AudioClip _gemClip;
    [SerializeField] private AudioClip _failClip;
    [SerializeField] private AudioSource _effectsAudioSource;
    public bool IsSoundOn { get; private set; } = true;

    public void Start()
    {
    }

    public void PlayTapSound()
    {
        _effectsAudioSource.PlayOneShot(_tapClip);
    }

    public void PlayClickSound()
    {
        _effectsAudioSource.PlayOneShot(_clickClip);
    }

    public void PlayGemSound()
    {
        _effectsAudioSource.PlayOneShot(_gemClip);
    }

    public void PlayFailSound()
    {
        _effectsAudioSource.PlayOneShot(_failClip);
    }

    public void SwitchSound()
    {
        IsSoundOn = !IsSoundOn;
        _effectsAudioSource.mute = !IsSoundOn;
    }


}
