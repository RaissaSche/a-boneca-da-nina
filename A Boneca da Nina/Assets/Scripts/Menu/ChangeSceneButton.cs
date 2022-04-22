using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{
    public CanvasFade fade;
    public void ChangeScene(string sceneName)
    {
        fade.FadeOut();
        LevelManager.Instance.OpenScene(sceneName);
    }
}
