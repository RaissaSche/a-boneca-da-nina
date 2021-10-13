using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float speed;
    public int move;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(transform.position.x - move, transform.position.y, transform.position.z),
                 Time.deltaTime * speed);
        }
    }
}