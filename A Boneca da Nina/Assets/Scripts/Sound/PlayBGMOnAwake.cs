using UnityEngine;

public class PlayBGMOnAwake : MonoBehaviour {

    public SoundManager.BGMType bgm;
    public float volume = 1f;

    private void Awake () {
        SoundManager.Instance.PlayBackgroundMusic (bgm, volume);
    }
}