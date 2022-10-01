using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [Header("AudioClips")]
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip takeResource;
    [SerializeField] private AudioClip destroyTrashSourceOne;
    [SerializeField] private AudioClip money;
    [SerializeField] private AudioClip recyclingOpenDoor;
    [SerializeField] private AudioClip upgradeSound;

    [SerializeField] private AudioSource _source1;
    [SerializeField] private AudioSource _source2;

    [Header("Volume")]
    [Range(0, 1)]
    [SerializeField] private float volumeBackgrooundMusic = 1;
    [Range(0, 1)]
    [SerializeField] private float volumeFX = 1;

    private AudioSource _soundSource;

    public bool EnableMusic { get; set; } = true;
    public bool EnableSound { get; set; } = true;
    public bool EnableVibro { get; set; } = true;

    #region Injects

    private SignalBus _signalBus;
    private SerialDataManager _serialDataManager;
    private BackgroundMusicFactory _backgroundMusicFactory;

    [Inject]
    private void Construct(SignalBus signalBus, 
                           BackgroundMusicFactory backgroundMusicFactory,
                           SerialDataManager serialDataManager)
    {
        _signalBus = signalBus;
        _backgroundMusicFactory = backgroundMusicFactory;
        _serialDataManager = serialDataManager;
    }

    #endregion

    #region Signals

    private void OnEnable()
    {
        _signalBus.Subscribe<MusicSignal>(MusicSignal);
        _signalBus.Subscribe<SoundSignal>(SoundSignal);
        _signalBus.Subscribe<VibroSignal>(VibroSignal);
        _signalBus.Subscribe<TookPackedTrashSignal>(TookPackedTrashSignal);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<MusicSignal>(MusicSignal);
        _signalBus.Unsubscribe<SoundSignal>(SoundSignal);
        _signalBus.Unsubscribe<VibroSignal>(VibroSignal);
        _signalBus.Unsubscribe<TookPackedTrashSignal>(TookPackedTrashSignal);
    }

    private void TookPackedTrashSignal()
    {
       // TakeResource();
    }

    private void MusicSignal()
    {
        UpdateEnableMusic();
    }

    private void UpdateEnableMusic()
    {
        if (EnableMusic)
        {
            EnableMusic = false;

            if (_soundSource != null)
                _soundSource.Pause();
        }
        else
        {
            EnableMusic = true;

            if (_soundSource != null)
                _soundSource.Play();               
        }

        _serialDataManager.Data.Music = EnableMusic;
    }

    private void SoundSignal()
    {
        UpdateEnableSound();
    }
        
    private void UpdateEnableSound()
    {
        if (EnableSound)
        {
            EnableSound = false;
        
        }
        else
        {
            EnableSound = true;
        }

        _serialDataManager.Data.Sound = EnableSound;
    }

    private void VibroSignal()
    {
        UpdateEnableVibro();
    }

    private void UpdateEnableVibro()
    {
        if (EnableVibro)
        {
            EnableVibro = false;

        }
        else
        {
            EnableVibro = true;
        }

        _serialDataManager.Data.Vibro = EnableSound;
    }

    #endregion

    public void StartLocal()
    {
        _soundSource = _backgroundMusicFactory.Create();
    }

    public void LoadMusicAndSound()
    {
        EnableMusic = _serialDataManager.Data.Music;

        if (EnableMusic)
        {
            _soundSource.Play();
        }
        else
        {
            if (_soundSource != null)
                _soundSource.Stop();
        }

        EnableSound = _serialDataManager.Data.Sound;

        if (_soundSource != null)
            _soundSource.volume = volumeBackgrooundMusic;

        EnableVibro = _serialDataManager.Data.Vibro;
    }

    private void PlaySound(AudioSource source, AudioClip clip, float volumePercent)
    {
        if (clip && source && EnableSound)
        {
            if (!source.isPlaying)
            {
                source.clip = clip;
                source.volume = volumeFX * volumePercent;
                source.Play();                
            }            
        }
    }

    private void PlaySoundShot(AudioClip clip, float volume)
    {
        if (clip && _soundSource && EnableSound)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position, volumeFX * volume);            
        }
    }

    public void Click()
    {
        PlaySoundShot(click, volumeFX);
    }

    public void TakeResource()
    {
        PlaySound(_source2, takeResource, 0.25f);
    }

    public void DestroyTrashOne()
    {
        PlaySound(_source1, destroyTrashSourceOne, 1f);
    }

    public void PlayDestroyTrash()
    {
        _source1.Play();
    }

    public void StopDestroyTrash()
    {
        _source1.Stop();
    }

    public void Money()
    {
        PlaySound(_source1, money, 1f);
    }

    public void RecyclingOpenDoor()
    {
        PlaySoundShot(recyclingOpenDoor, 1f);
    }

    public void UpgradeSound()
    {
        PlaySoundShot(upgradeSound, 1f);
    }
}
