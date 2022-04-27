using UnityEngine;

public class MenuManager : MonoBehaviour {
    //public GameObject mainMenuPanel;
    //public GameObject creditsPanel;
    public CanvasFade fade;

    void Start() {
        fade.FadeIn();
        Cursor.visible = true;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            ScreenCapture.CaptureScreenshot("Nina.png");
        }

        if (Input.GetKey("escape")) {
            Application.Quit();
        }
    }

    public void ExitButtonClicked() {
        fade.FadeOut(2f, 1f);
    }

    public void QuitGame() {
        fade.FadeOut(2f, 1f);
        Application.Quit();
    }

    /*public void CreditsClicked(){
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void BackButtonClicked(){
        mainMenuPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }*/
}