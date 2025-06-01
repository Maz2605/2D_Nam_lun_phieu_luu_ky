using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    
    [Header("Audio Souce")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundSource;

    [Header("Audio Clip")]
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip toggleClickOn;
    [SerializeField] private AudioClip toggleClickOff;

    [Space] [Header("Audio in Game")]
    [SerializeField] private AudioClip musicBG;
    [SerializeField] private AudioClip musicInGame;
    [SerializeField] private AudioClip soundConnect;
    [SerializeField] private AudioClip soundWin;
    [SerializeField] private AudioClip soundFail;
    

    protected override void Awake()
    {
        base.KeepAlive(false);
        base.Awake();
    }

    private void Start()
    {
        SetMuteSound();
        SetMuteMusic();
    }

    public void SetMuteSound()
    {
        if (UIController.Instance.UISetting.IsMuteSound)
        {
            soundSource.mute = true;
            return;
        }
        soundSource.mute = false;
    }

    public void SetMuteMusic()
    {
        if (UIController.Instance.UISetting.IsMuteMusic)
        {
            soundSource.mute = true;
        }
        soundSource.mute = false;
    }

    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.DOFade(0f, 0.5f).OnComplete(() =>
            {
                musicSource.Stop();
            }).SetUpdate(true);
        }
    }

    public void SetVolume(float volume)
    {
        
    }

    public void PlaySFX(AudioClip sound, bool repeat = false)
    {
        if (UIController.Instance.UISetting.IsMuteSound) return;

        if (sound != null)
        {
            if (repeat)
            {
                soundSource.loop = true;
                soundSource.clip = sound;   
                soundSource.Play();
            }
            else
            {
                soundSource.loop = false;
                soundSource.PlayOneShot(sound);
            }
        }
    }

    public void PlaySoundButtonClick()
    {
        PlaySFX(buttonClick);
    }

    public void PlaySFXMovement()
    {
        
    }   
    
    public void PlaySFXJump()
    {
        
    }    

    public void PlaySFXGetItem()
    {
        
    }    

    public void PlaySoundWin()
    {
        PlaySFX(soundWin);
    }    

    public void PlaySoundFail()
    {
        PlaySFX(soundFail);
    }  
    
}

