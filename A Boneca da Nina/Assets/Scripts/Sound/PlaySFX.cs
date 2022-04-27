using UnityEngine;

public class PlaySFX : MonoBehaviour {

    public SoundManager.SfxType sfx;
    public float volume = 1f;

    public void Play () {
        SoundManager.Instance.PlaySfx (sfx, volume);
    }
}