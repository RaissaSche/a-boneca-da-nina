using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _loadingCanvas;
    [SerializeField] private GameObject _otherCanvas;
    [SerializeField] private Image _progressBar;

    private float _target;
    public static LevelManager Instance;
    private SoundManager soundManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        soundManager = SoundManager.instance;
    }

    public void Update()
    {
        _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _target, 3 * Time.deltaTime);
    }

    public async void OpenScene(string sceneName)
    {
        _target = 0;
        _progressBar.fillAmount = 0;

        soundManager.PlaySFX(SoundManager.SFXType.CLICK, 0.5f);
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        _loadingCanvas.SetActive(true);

        do
        {
            await Task.Delay(100);
            _target = scene.progress / 0.9f;

        } while (scene.progress < 0.89); //0.9 is when the scene is loaded

        //await Task.Delay(2000);

        scene.allowSceneActivation = true;

        _otherCanvas.SetActive(false);
        _loadingCanvas.SetActive(false);
    }
}
