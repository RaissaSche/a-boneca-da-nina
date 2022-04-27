using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour {

    private SoundManager _soundManager;

    public Image iconImage;
    public Sprite soundSprite;
    public Sprite muteSprite;

    void Start () {
        _soundManager = SoundManager.Instance;
        UpdateIcon ();
    }

    void UpdateIcon () {
        iconImage.sprite = AudioListener.volume >= 0.95f ? soundSprite : muteSprite;
    }

    public void InvertAudio () {
        _soundManager.InvertSound ();
        UpdateIcon ();
    }
}