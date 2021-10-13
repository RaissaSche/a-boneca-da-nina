using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    public float targetTimeOriginal;
    [SerializeField]
    private float targetTime;
    private bool flag = true;
    private Color color;

    void Start()
    {
        color = GetComponent<SpriteRenderer>().color;

        if (flag)
        {
            //set alpha to zero
            color.a = 0f;
            GetComponent<SpriteRenderer>().color = color;
        }
        else
        {
            //set alpha to 1
            color.a = 1f;
            GetComponent<SpriteRenderer>().color = color;
        }
    }

    void Update()
    {
        targetTime -= Time.deltaTime;
        //Debug.Log(targetTime);

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }
    }

    void timerEnded()
    {
        flag = !flag;
        targetTime = targetTimeOriginal;

        if (flag)
        {
            //set alpha to 1
            color.a = 1f;
            GetComponent<SpriteRenderer>().color = color;
        }
        else
        {
            //set alpha to 0
            color.a = 0f;
            GetComponent<SpriteRenderer>().color = color;
        }
    }
}
