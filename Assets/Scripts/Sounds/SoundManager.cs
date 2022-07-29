using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonBase<SoundManager>
{
    public enum AudioSourceList
    {
        BGM = 0,
        Drive,
        Water,
        SFX
    }
    public enum SoundList
    {
        BGM_JumpyGame = 0,
        BGM_SunnyDay,
        ButtonClickSound,
        PlayerMoveSound,
        WaterSound,
        CarDriveSound1,
        CarDriveSound2,
        VanDriveSound1,
        VanDriveSound2,
        CrashCollisionSound,
        OutCollisionSound,
        FlySound
    }

    [SerializeField]
    private Dictionary<SoundList, AudioClip> dictionaryAudioClips = new Dictionary<SoundList, AudioClip>();
    [SerializeField]
    private AudioClip[] audioClips;
    [SerializeField]
    private AudioSource BGMAudioSource;
    [SerializeField]
    private AudioSource DriveAudioSource;
    [SerializeField]
    private AudioSource WaterAudioSource;
    [SerializeField]
    private AudioSource SFXAudioSource;

    private float driveSoundElapsedTime;
    private float waterSoundElapsedTime;
    private float playDriveSoundDelayTime;
    private float playWaterSoundDelayTime;

    private void Awake()
    {
        for(int i = 0; i < audioClips.Length; ++i)
        {
            dictionaryAudioClips.Add((SoundList)i, audioClips[i]);
        }
    }

    private void Start()
    {
        BGMAudioSource.volume = 0.1f;
        BGMAudioSource.loop = true;
        BGMAudioSource.Play();

        DriveAudioSource.volume = 0.15f;
        DriveAudioSource.loop = false;
        WaterAudioSource.volume = 0.15f;
        WaterAudioSource.loop = false;

        driveSoundElapsedTime = 0f;
        waterSoundElapsedTime = 0f;
        playDriveSoundDelayTime = Random.Range(3f, 5f);
        playWaterSoundDelayTime = Random.Range(3f, 5f);
    }

    private void FixedUpdate()
    {
        if (DriveAudioSource.isPlaying)
        {
            driveSoundElapsedTime += Time.fixedDeltaTime;
        }
        else if (driveSoundElapsedTime > playDriveSoundDelayTime || driveSoundElapsedTime == 0)
        {
            driveSoundElapsedTime = 0f;
            DriveAudioSource.clip = dictionaryAudioClips[(SoundList)(Random.Range((int)SoundList.CarDriveSound1, ((int)SoundList.VanDriveSound2) + 1))];
            DriveAudioSource.Play();
        }

        if (WaterAudioSource.isPlaying)
        {
            waterSoundElapsedTime += Time.fixedDeltaTime;
        }
        else if (waterSoundElapsedTime > playWaterSoundDelayTime || waterSoundElapsedTime == 0)
        {
            waterSoundElapsedTime = 0f;
            WaterAudioSource.clip = dictionaryAudioClips[SoundList.WaterSound];
            WaterAudioSource.Play();
        }
    }

    public void PlaySFXSoundByClip(SoundList sound)
    {
        float volume = 0.2f;

        switch (sound)
        {
            case SoundList.BGM_JumpyGame: case SoundList.BGM_SunnyDay:
                volume = 0.1f;
                break;
            case SoundList.ButtonClickSound:
                volume = 0.4f;
                break;
            case SoundList.PlayerMoveSound: case SoundList.CrashCollisionSound:
            case SoundList.OutCollisionSound: case SoundList.FlySound:
                volume = 0.3f;
                break;
            case SoundList.WaterSound:
                volume = 0.15f;
                break;
            case SoundList.CarDriveSound1: case SoundList.CarDriveSound2:
            case SoundList.VanDriveSound1: case SoundList.VanDriveSound2:
                volume = 0.15f;
                break;
        }

        SFXAudioSource.PlayOneShot(dictionaryAudioClips[sound], volume);
    }

    public void PlayBackGroundMusic()
    {
        BGMAudioSource.Play();
    }

    public void StopBackGroundMusic()
    {
        BGMAudioSource.Stop();
    }

    public void PauseBackGroundMusic()
    {
        BGMAudioSource.Pause();
    }
}
