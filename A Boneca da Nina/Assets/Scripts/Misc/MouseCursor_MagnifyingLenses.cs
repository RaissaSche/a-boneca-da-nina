using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseCursor_MagnifyingLenses : MonoBehaviour {

    public SpriteRenderer redness, sickness, sadness, tiredness;
    public GameObject buttonFinish;
    public string nextScene;
    private Sprite spriteRedness, spriteSickness, spriteSadness, spriteTiredness;
    private bool[] checkDiscoveredSymptom = new bool[4];

    void Start () {
        Cursor.visible = false;
        buttonFinish.SetActive (false);
        spriteRedness = Resources.Load ("Redness", typeof (Sprite)) as Sprite;
        spriteSickness = Resources.Load ("Sickness", typeof (Sprite)) as Sprite;
        spriteSadness = Resources.Load ("Sadness", typeof (Sprite)) as Sprite;
        spriteTiredness = Resources.Load ("Tiredness", typeof (Sprite)) as Sprite;

        for (int i = 0; i < 4; ++i) {
            checkDiscoveredSymptom[i] = false;
        }
    }

    void Update () {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        transform.position = cursorPos;
    }

    void OnCollisionEnter2D (Collision2D col) {
        GameObject symptom = col.gameObject;
        int aux = 0;

        for (int i = 0; i < 4; ++i) {
            if (checkDiscoveredSymptom[i]) {
                aux++;
            }
        }

        if (aux == 4) {
            buttonFinish.SetActive (true);
        }

        switch (symptom.name) {
            case "RednessSymptom":
                redness.sprite = spriteRedness;
                checkDiscoveredSymptom[0] = true;
                break;
            case "SicknessSymptom":
                sickness.sprite = spriteSickness;
                checkDiscoveredSymptom[1] = true;
                break;
            case "SadnessSymptom":
                sadness.sprite = spriteSadness;
                checkDiscoveredSymptom[2] = true;
                break;
            case "TirednessSymptom":
                tiredness.sprite = spriteTiredness;
                checkDiscoveredSymptom[3] = true;
                break;
            case "Finish":
                if (aux == 4) {
                    buttonFinish.SetActive (true);
                }
                SceneManager.LoadScene(nextScene);
                break;
        }

    }
}