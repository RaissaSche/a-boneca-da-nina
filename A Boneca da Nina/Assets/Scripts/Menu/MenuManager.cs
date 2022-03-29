using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject creditsPanel;
    private SoundManager soundManager;

    private void Start()
    {
        Cursor.visible = true;
        soundManager = SoundManager.instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ScreenCapture.CaptureScreenshot("Nina.png");
        }
    }

    public void OpenScene(string scene)
    {
        SceneManager.LoadScene(scene);
        soundManager.PlaySFX(SoundManager.SFXType.CLICK, 0.5f);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CreditsClicked()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void BackButtonClicked()
    {
        mainMenuPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }
}
