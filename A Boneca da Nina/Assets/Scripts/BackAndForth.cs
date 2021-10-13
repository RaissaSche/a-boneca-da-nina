using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour {

    [SerializeField]
    private Vector2 moveSpeed;
    [SerializeField]
    [Range (0f, 10f)]
    private float movementDuration;
    private bool isForward;
    [SerializeField]
    private bool flip = true;

    void Start () {
        if (movementDuration >= 0.1f)
            InvokeRepeating ("Turn", movementDuration, movementDuration);
    }

    void Update () {
        transform.Translate (moveSpeed * Time.deltaTime);
    }

    private void Turn () {
        if (flip)
            transform.localScale = new Vector2 (-transform.localScale.x, transform.localScale.y);
        isForward = !isForward;
        moveSpeed *= -1f;
    }
}