using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    public float targetTimeOriginal;
    [SerializeField]
    private float targetTime;
    private bool _flag = true;
    private Color _color;

    void Start()
    {
        _color = GetComponent<SpriteRenderer>().color;

        if (_flag)
        {
            //set alpha to zero
            _color.a = 0f;
            GetComponent<SpriteRenderer>().color = _color;
        }
        else
        {
            //set alpha to 1
            _color.a = 1f;
            GetComponent<SpriteRenderer>().color = _color;
        }
    }

    void Update()
    {
        targetTime -= Time.deltaTime;
        //Debug.Log(targetTime);

        if (targetTime <= 0.0f)
        {
            TimerEnded();
        }
    }

    void TimerEnded()
    {
        _flag = !_flag;
        targetTime = targetTimeOriginal;

        if (_flag)
        {
            //set alpha to 1
            _color.a = 1f;
            GetComponent<SpriteRenderer>().color = _color;
        }
        else
        {
            //set alpha to 0
            _color.a = 0f;
            GetComponent<SpriteRenderer>().color = _color;
        }
    }
}
