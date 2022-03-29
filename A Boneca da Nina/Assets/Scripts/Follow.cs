using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform objectToFollow;
    public Vector3 offsetOriginal;
    private Vector3 offset;
    private int time = 0;

    void Start()
    {
        offset = offsetOriginal;
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            time += 1;
        }
        /*if (Input.GetAxis("Horizontal") < 0)
        {
            time = Mathf.Max(0, time - 1);
        }*/
        else
        {
            time = Mathf.Max(0, time - 1);
        }

        offset.x = offsetOriginal.x - time / 1000.0f;

        transform.position = objectToFollow.position + offset;
    }
}
