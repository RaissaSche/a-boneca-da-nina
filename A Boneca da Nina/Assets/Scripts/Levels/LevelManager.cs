using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject loadingCanvas;
    [SerializeField] private GameObject otherCanvas;
    [SerializeField] private Image progressBar;

    private float _target;
    public static LevelManager instance;
    private SoundManager _soundManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _soundManager = SoundManager.Instance;
    }

    public void Update()
    {
        progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, _target, 3 * Time.deltaTime);
    }

    public async void OpenScene(string sceneName)
    {
        _target = 0;
        progressBar.fillAmount = 0;

        _soundManager.PlaySfx(SoundManager.SfxType.CLICK_SFX, 0.7f);
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loadingCanvas.SetActive(true);

        do
        {
            await Task.Delay(100);
            _target = scene.progress / 0.9f;

        } while (scene.progress < 0.89); //0.9 is when the scene is loaded

        await Task.Delay(2000);

        scene.allowSceneActivation = true;

        otherCanvas.SetActive(false);
        loadingCanvas.SetActive(false);
    }
}
