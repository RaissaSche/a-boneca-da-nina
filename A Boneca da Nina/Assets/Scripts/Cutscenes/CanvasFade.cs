using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFade : MonoBehaviour {

    #region Events
    public event Action<CanvasFade, bool> OnFadeBegin;
    public event Action<CanvasFade, bool> OnFadeEnd;
    public event Action<CanvasFade, float> OnFadeUpdate;
    #endregion

    [SerializeField]
    private Image fadeImage;

    #region Properties
    private Color _fadeColor = Color.black;

    public Color FadeColor {
        get => _fadeColor;
        set {
            _fadeColor.r = value.r;
            _fadeColor.g = value.g;
            _fadeColor.b = value.b;
        }
    }

    private float _progress = 0f;

    public float Progress => _progress;

    private bool _fading = false;

    public bool Fading => _fading;

    #endregion

    void Awake () {
        // Try to find the Image Component
        if (fadeImage == null)
            fadeImage = GetComponent<Image> ();
        if (fadeImage == null)
            Debug.LogError ("Image not found. This Script requires an Image Component!");
    }

    // Change the Fade alpha
    private void ChangeFadeAlpha (float alpha) {
        _fadeColor.a = alpha;
        fadeImage.color = _fadeColor;
    }

    #region FadeCalls
    // Fade Calls
    public void Fade (bool fadeIn, float duration = 1f, float endDelay = 0f) {
        StartCoroutine (FadeImage (fadeIn, duration, endDelay));
    }

    public void FadeIn (float duration = 1f, float endDelay = 0f) {
        StartCoroutine (FadeImage (true, duration, endDelay));
    }

    public void FadeOut (float duration = 1f, float endDelay = 0f) {
        StartCoroutine (FadeImage (false, duration, endDelay));
    }
    #endregion

    // Fade Function
    IEnumerator FadeImage (bool fadeIn, float duration, float endDelay) {
        _fading = true;
        // Begin Action
        if (OnFadeBegin != null)
            OnFadeBegin (this, fadeIn);

        // Counter from 0 to 1
        for (_progress = 0f; _progress < 1f; _progress += Time.deltaTime / duration) {
            if (fadeIn)
                ChangeFadeAlpha (1f - _progress);
            else
                ChangeFadeAlpha (_progress);

            // Update Action
            if (OnFadeUpdate != null)
                OnFadeUpdate (this, _progress);
            yield return null;
        }

        // Another Update to avoid iteration errors
        ChangeFadeAlpha(fadeIn ? 0f : 1f);

        // Addition delay after the fade
        if (endDelay > 0.001f)
            yield return new WaitForSeconds (endDelay);

        _fading = false;
        // End Action
        if (OnFadeEnd != null)
            OnFadeEnd (this, fadeIn);
    }
}