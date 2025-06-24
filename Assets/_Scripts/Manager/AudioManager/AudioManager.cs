using System;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Audio Manager")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    
    [Header("Audio Clips")]
    public AudioClip musicBg01;
    public AudioClip musicBg02;
    public AudioClip musicBg03;
    public AudioClip musicBg04;
    public AudioClip musicWin;
    public AudioClip musicLose;
    [Header("Player Sfx Clips")] 
    public AudioClip sfxJump;
    public AudioClip sfxHurt;
    public AudioClip sfxFire;
    public AudioClip sfxGetItem;
    public AudioClip sfxCheckPoint;
    [Header("UI Sfx Clips")]
    public AudioClip sfxButtonClick;
    public AudioClip sfxPopUp;

    private readonly string _fileName = "AudioData.json";
    private string _filePath;
    private AudioDataManager _audioDataManager;
    
    protected override void Awake()
    {
        KeepAlive(true);
        base.Awake();
        _filePath = Path.Combine(Application.persistentDataPath, _fileName);
        ResetData();
        LoadData(); 
    }

    #region Json Manager
    public void SaveData()
    {
        string json = JsonUtility.ToJson(_audioDataManager, true);
        File.WriteAllText(_filePath, json);
        Debug.Log("Save Data at " + _filePath );
    }

    public void LoadData()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            _audioDataManager = JsonUtility.FromJson<AudioDataManager>(json);
        }
        else
        {
            _audioDataManager = new AudioDataManager();
            Debug.Log("Load new data");
            SaveData();
        }
    }

    public void ResetData()
    {
        _audioDataManager = new AudioDataManager();
        _audioDataManager.musicVolume = 0.5f;
        _audioDataManager.sfxVolume = 0.5f;
        _audioDataManager.musicMuted = false;
        _audioDataManager.sfxMuted = false;
        SaveData();
    }
    public void ApplySettings()
    {
        musicSource.volume = _audioDataManager.musicVolume;
        sfxSource.volume = _audioDataManager.sfxVolume;
        musicSource.mute = _audioDataManager.musicMuted;
        sfxSource.mute = _audioDataManager.sfxMuted;
    }
    

    #endregion
    
    #region Manager
    
    public void PlayMusic(AudioClip clip, bool loop = false)
    {
        if(clip == null) return;
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySfx(AudioClip clip)
    {
        if (clip == null || sfxSource.mute) return;
        sfxSource.PlayOneShot(clip);
    }

    public void SetMusicVolume(float volume)
    {
        _audioDataManager.musicVolume = Mathf.Clamp01(volume);
        musicSource.volume = _audioDataManager.musicVolume;
        SaveData();
    }

    public void SetSfxVolume(float volume)
    {
        _audioDataManager.sfxVolume = Mathf.Clamp01(volume);
        sfxSource.volume = _audioDataManager.sfxVolume;
        SaveData();
    }

    public void MuteMusic(bool mute)
    {
        _audioDataManager.musicMuted = mute;
        musicSource.mute = mute;
        SaveData();
    }

    public void MuteSfx(bool mute)
    {
        _audioDataManager.sfxMuted = mute;
        sfxSource.mute = mute;
        SaveData();
    }
    #endregion

    #region Sfx
    public void PlaySfxJump() => PlaySfx(sfxJump);
    public void PlaySfxHurt() => PlaySfx(sfxHurt);
    public void PlaySfxFire() => PlaySfx(sfxFire);
    public void PlaySfxGetItem() => PlaySfx(sfxGetItem);
    public void PlaySfxButtonClick() => PlaySfx(sfxButtonClick);
    public void PlaySfxPopUp() => PlaySfx(sfxPopUp);
    public void PlaSfxGetCheckPoint() => PlaySfx(sfxCheckPoint);
    
    #endregion

    #region Music

    public void PlayMusicBg(string level = "Level_1")
    {
        switch (level)
        {
            case "Level_1":
                PlayMusic(musicBg01, true);
                break;
            case "Level_2":
                PlayMusic(musicBg02, true);
                break;
            case "Level_3":
                PlayMusic(musicBg03, true);
                break;
            case "Level_4":
                PlayMusic(musicBg04, true);
                break;
        }
    } 
    
    public void PlayMusicWin() => PlayMusic(musicWin);
    public void PlayMusicLose() => PlayMusic(musicLose);
    
    #endregion

    #region Properties
    public float MusicVolume => _audioDataManager.musicVolume;
    public float SfxVolume => _audioDataManager.sfxVolume;
    public bool IsMusicMuted => _audioDataManager.musicMuted;
    public bool IsSfxMuted => _audioDataManager.sfxMuted;
    
    #endregion
}