using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour {

    #region Singleton

    private static SoundManager _instance;

    public static SoundManager Instance {
        get {
            if (_instance == null) {
                _instance = new GameObject("SoundManager", typeof(SoundManager)).GetComponent<SoundManager>();
                DontDestroyOnLoad(_instance);
            }

            return _instance;
        }
    }

    #endregion

    public enum BGMType {
        FOR_A_BONECA_DA_NINA,
        LEVEL_1,
        CUTSCENE_1,
        LEVEL_2,
        CUTSCENE_2,
        NONE
    }

    public enum SfxType {
        CLICK_SFX,
        JUMP_SFX,
        GIGGLE_SFX,
        SILENCE_SIGN_SFX,
        SEAT_SFX,
        DOOR_SFX
    }

    public List<AudioClip> bgmClips = new List<AudioClip>();
    public List<AudioClip> boingClips = new List<AudioClip>();
    public AudioClip clickClip, jumpClip, giggleClip, shushClip, doorClip;
    public AudioSource bgmSource;
    public AudioLowPassFilter lowPassFilter;

    private BGMType _currentBGMType = BGMType.NONE;

    private void Awake() {
        bgmSource = new GameObject("BGMAudioSource", typeof(AudioSource)).GetComponent<AudioSource>();
        var bgmSourceTransform = bgmSource.transform;
        bgmSourceTransform.parent = transform;
        bgmSourceTransform.localPosition = Vector3.zero;
        bgmSource.playOnAwake = false;
        bgmSource.loop = true;

        bgmSource.AddComponent<AudioLowPassFilter>();
        lowPassFilter = bgmSource.GetComponent<AudioLowPassFilter>();
        bgmSource.GetComponent<AudioLowPassFilter>().cutoffFrequency = 20000;

        bgmClips.Add(Resources.Load<AudioClip>("Music/ForABonecaDaNina"));
        bgmClips.Add(Resources.Load<AudioClip>("Music/ForABonecaDaNina-Level1"));
        bgmClips.Add(Resources.Load<AudioClip>("Music/ForABonecaDaNina"));
        bgmClips.Add(Resources.Load<AudioClip>("Music/ForABonecaDaNina-Level2"));
        bgmClips.Add(Resources.Load<AudioClip>("Music/ForABonecaDaNina-Cutscene2"));

        boingClips.Add(Resources.Load<AudioClip>("SFX/Boing1"));
        boingClips.Add(Resources.Load<AudioClip>("SFX/Boing2"));
        boingClips.Add(Resources.Load<AudioClip>("SFX/Boing3"));
        boingClips.Add(Resources.Load<AudioClip>("SFX/Boing4"));
        boingClips.Add(Resources.Load<AudioClip>("SFX/Boing5"));
        boingClips.Add(Resources.Load<AudioClip>("SFX/Boing6"));

        giggleClip = Resources.Load<AudioClip>("SFX/Giggle");
        shushClip = Resources.Load<AudioClip>("SFX/Shush");
        doorClip = Resources.Load<AudioClip>("SFX/KnockingOnDoor");
        clickClip = Resources.Load<AudioClip>("SFX/ButtonClick");
        jumpClip = Resources.Load<AudioClip>("SFX/Jump");
    }

    public void InvertSound() {
        AudioListener.volume = 1f - AudioListener.volume;
    }

    public AudioLowPassFilter GetLowPassFilter() {
        return lowPassFilter;
    }

    public void PlayBackgroundMusic(BGMType bgType, float volume) {
        if (_currentBGMType == bgType)
            return;

        _currentBGMType = bgType;
        bgmSource.clip = bgmClips[(int) bgType];
        bgmSource.volume = volume;
        bgmSource.Play();
    }

    public void PlaySfx(SfxType sfxType, float volume = 1f) {
        AudioSource.PlayClipAtPoint(GetClip(sfxType), Camera.main.transform.position, volume);
    }

    public void PlaySfxAtPosition(SfxType sfxType, Vector3 position, float volume = 1f) {
        position.z = Camera.main.transform.position.z;
        AudioSource.PlayClipAtPoint(GetClip(sfxType), position, volume);
    }

    private AudioClip GetClip(SfxType sfxType) {
        switch (sfxType) {
            case SfxType.CLICK_SFX: {
                return clickClip;
            }
            case SfxType.JUMP_SFX: {
                return jumpClip;
            }
            case SfxType.GIGGLE_SFX: {
                return giggleClip;
            }
            case SfxType.SILENCE_SIGN_SFX: {
                return shushClip;
            }
            case SfxType.SEAT_SFX: {
                return boingClips[Random.Range(0, boingClips.Count)];
            }
            case SfxType.DOOR_SFX: {
                return doorClip;
            }

            default: return null;
        }
    }
}