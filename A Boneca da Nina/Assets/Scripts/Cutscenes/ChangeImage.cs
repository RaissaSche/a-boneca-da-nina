using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{

    public Sprite cutscene1;
    public Sprite cutscene2;
    public Sprite cutscene3;
    public Sprite cutscene4;
    public GameObject proceedButton;

    public int imgNumberCount;

    private SoundManager soundManager;

    private void Start()
    {
        soundManager = SoundManager.instance;
    }
    public void changeImages()
    {
        soundManager.PlaySFX(SoundManager.SFXType.CLICK, 0.8f);

        switch (imgNumberCount)
        {

            case 0:
                GetComponent<Image>().sprite = cutscene1;
                imgNumberCount++;
                break;
            case 1:
                GetComponent<Image>().sprite = cutscene2;
                imgNumberCount++;
                break;
            case 2:
                GetComponent<Image>().sprite = cutscene3;
                imgNumberCount++;
                break;
            case 3:
                GetComponent<Image>().sprite = cutscene4;
                imgNumberCount++;
                imgNumberCount = 0; //Reset it to 0              
                proceedButton.SetActive(true); //Set proceed button as active
                break;
            default:
                Debug.Log("Error");
                break;
        }
    }
}
