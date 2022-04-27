using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{
    public CanvasFade fade;

    public void ChangeScene(string sceneName)
    {
        fade.FadeOut();
        LevelManager.instance.OpenScene(sceneName);
    }
}
