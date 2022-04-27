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

    private SoundManager _soundManager;

    private void Start()
    {
        _soundManager = SoundManager.Instance;
    }
    public void ChangeImages()
    {
        _soundManager.PlaySfx(SoundManager.SfxType.CLICK_SFX, 0.8f);

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
