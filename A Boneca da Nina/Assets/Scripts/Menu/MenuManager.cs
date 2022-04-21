using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;


public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject creditsPanel;

    private void Start()
    {
        Cursor.visible = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ScreenCapture.CaptureScreenshot("Nina.png");
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
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


/*
    public CanvasFade fade;
    public Player player;

    public CustomTrigger2D doorToNextLevel;
    public string sceneToLoad;
    private bool changingScene = false;

    void Start () {
        fade.OnFadeEnd += FadeFinished;
        fade.FadeIn ();
        doorToNextLevel.OnCustomTriggerEnter2D += DoorTriggered;
        player.OnDied += OnPlayerDied;
    }

    private void OnPlayerDied (Player obj) {
        if (!changingScene) {
            changingScene = true;
            sceneToLoad = "Level2";
            fade.FadeOut (2f, 1f);
        }
    }

    private void FadeFinished (CanvasFade arg1, bool arg2) {
        if (!arg2)
            SceneManager.LoadScene (sceneToLoad);
    }

    private void DoorTriggered (Collider2D obj) {
        if (obj.gameObject.layer == LayerMask.NameToLayer ("Player") && !changingScene) {
            doorToNextLevel.OnCustomTriggerEnter2D -= DoorTriggered;
            changingScene = true;
            sceneToLoad = "Cutscene3";
            fade.FadeOut (2f, 1f);
        }
    }
    public void ExitButtonClicked () {
        if (!changingScene) {
            doorToNextLevel.OnCustomTriggerEnter2D -= DoorTriggered;
            changingScene = true;
            sceneToLoad = "Menu";
            fade.FadeOut (2f, 1f);
        }
    }*/
