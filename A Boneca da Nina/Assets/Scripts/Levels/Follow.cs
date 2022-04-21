using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform objectToFollow;
    public Vector3 offsetOriginal;
    private Vector3 offset;
    private int time = 0;
    public SpriteRenderer sprite;

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

        offset.x = offsetOriginal.x - time / 10000.0f;

        transform.position = objectToFollow.position + offset;


        float h = Input.GetAxis("Horizontal");

        if (h >= 0.1f)
        {           
            sprite.flipX = false;
        }

        else if (h <= -0.1f)
        {
            sprite.flipX = true;
        }
    }
}
