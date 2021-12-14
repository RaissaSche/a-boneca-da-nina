using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject mainMenuPanel;
    public GameObject creditsPanel;

    private void Start()
    {
        Cursor.visible = true;
    }

    //private void Update () {
    /*if (Input.GetKeyDown(KeyCode.Return))
    {
        ScreenCapture.CaptureScreenshot("Nina.png");
    }*/
    //}

    public void OpenScene(string scene)
    {
        SceneManager.LoadScene(scene);
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