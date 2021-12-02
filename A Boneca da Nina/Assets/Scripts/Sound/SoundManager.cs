using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    #region Singleton
    static private SoundManager _instance;
    static public SoundManager instance {
        get {
            if (_instance == null) {
                _instance = new GameObject ("SoundManager", typeof (SoundManager)).GetComponent<SoundManager> ();
                DontDestroyOnLoad (_instance);
            }
            return _instance;
        }
    }
    #endregion

    public enum BGMType {
        BENSOUND_CUTE,
        SAD_UKULELE_SONG,
        FOR_A_BONECA_DA_NINA,
        NONE

    }

    public enum SFXType {
        CLICK,
        JUMP,
        ENEMY_SHOOT,
        DEFEND,
        PLAYER_ATTACK,
        ENEMY_HIT,
        GABI_SFX,
        PAINTING_SFX,
        SILENCE_SIGN_SFX,
        SEAT_SFX,
        DOOR_SFX
    }

    public List<AudioClip> bgmClips = new List<AudioClip> ();
    public List<AudioClip> enemyHitClips = new List<AudioClip> ();
    public List<AudioClip> lvl1Clips = new List<AudioClip> ();
    public List<AudioClip> boingClips = new List<AudioClip> ();
    public AudioClip clickClip;
    public AudioClip jumpClip;
    public AudioClip enemyShootClip;
    public AudioClip defendClip;
    public AudioClip playerAttackClip;
    public AudioSource bgmSource;

    private BGMType currentBGMType = BGMType.NONE;

    private void Awake () {
        bgmSource = new GameObject ("BGMAudioSource", typeof (AudioSource)).GetComponent<AudioSource> ();
        bgmSource.transform.parent = transform;
        bgmSource.transform.localPosition = Vector3.zero;
        bgmSource.playOnAwake = false;
        bgmSource.loop = true;

        bgmClips.Add (Resources.Load<AudioClip> ("Music/BensoundCute"));
        bgmClips.Add (Resources.Load<AudioClip> ("Music/SadUkuleleSong"));
        bgmClips.Add (Resources.Load<AudioClip> ("Music/ForABonecadaNina"));

        enemyHitClips.Add (Resources.Load<AudioClip> ("SFX/EnemyHit1"));
        enemyHitClips.Add (Resources.Load<AudioClip> ("SFX/EnemyHit2"));

        lvl1Clips.Add (Resources.Load<AudioClip> ("SFX/Giggle"));
        lvl1Clips.Add (Resources.Load<AudioClip> ("SFX/Beach"));
        lvl1Clips.Add (Resources.Load<AudioClip> ("SFX/Shush"));
        lvl1Clips.Add (Resources.Load<AudioClip> ("SFX/KnockingOnDoor"));

        boingClips.Add (Resources.Load<AudioClip> ("SFX/Boing1"));
        boingClips.Add (Resources.Load<AudioClip> ("SFX/Boing2"));
        boingClips.Add (Resources.Load<AudioClip> ("SFX/Boing3"));
        boingClips.Add (Resources.Load<AudioClip> ("SFX/Boing4"));
        boingClips.Add (Resources.Load<AudioClip> ("SFX/Boing5"));
        boingClips.Add (Resources.Load<AudioClip> ("SFX/Boing6"));

        clickClip = Resources.Load<AudioClip> ("SFX/ButtonClick");
        jumpClip = Resources.Load<AudioClip> ("SFX/Jump");
        playerAttackClip = Resources.Load<AudioClip> ("SFX/PlayerAttack");
        defendClip = Resources.Load<AudioClip> ("SFX/Defend");
        enemyShootClip = Resources.Load<AudioClip> ("SFX/EnemyShot");
        //buttonPressClip = Resources.Load<AudioClip>("Sounds/SFX/Other SFX/Button Press");
    }

    public void InvertSound () {
        AudioListener.volume = 1f - AudioListener.volume;
    }

    public void PlayBackgroundMusic (BGMType bgType, float volume) {
        if (currentBGMType == bgType)
            return;

        currentBGMType = bgType;
        bgmSource.clip = bgmClips[(int) bgType];
        bgmSource.volume = volume;
        bgmSource.Play ();
    }

    public void PlaySFX (SFXType sfxType, float volume = 1f) {
        AudioSource.PlayClipAtPoint (GetClip (sfxType), Camera.main.transform.position, volume);
    }

    public void PlaySFXAtPosition (SFXType sfxType, Vector3 position, float volume = 1f) {
        position.z = Camera.main.transform.position.z;
        AudioSource.PlayClipAtPoint (GetClip (sfxType), position, volume);
    }

    private AudioClip GetClip (SFXType sfxType) {
        if (sfxType == SFXType.CLICK)
            return clickClip;
        else if (sfxType == SFXType.JUMP)
            return jumpClip;
        else if (sfxType == SFXType.ENEMY_SHOOT)
            return enemyShootClip;
        else if (sfxType == SFXType.DEFEND)
            return defendClip;
        else if (sfxType == SFXType.PLAYER_ATTACK)
            return playerAttackClip;
        else if (sfxType == SFXType.ENEMY_HIT)
            return enemyHitClips[Random.Range (0, enemyHitClips.Count)];
        else if (sfxType == SFXType.GABI_SFX)
            return lvl1Clips[0];
        else if (sfxType == SFXType.PAINTING_SFX)
            return lvl1Clips[1];
        else if (sfxType == SFXType.SILENCE_SIGN_SFX)
            return lvl1Clips[2];
        else if (sfxType == SFXType.SEAT_SFX)
            return boingClips[Random.Range (0, boingClips.Count)];
        else if (sfxType == SFXType.DOOR_SFX)
            return lvl1Clips[3];
        return clickClip;
    }
}