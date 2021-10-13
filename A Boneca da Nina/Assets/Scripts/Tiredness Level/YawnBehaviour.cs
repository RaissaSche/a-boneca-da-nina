using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YawnBehaviour : MonoBehaviour
{
    [SerializeField] private float lifeTime = 2.0f;
    [SerializeField] private float speed = 10.0f;
    private Vector2 direction;

    public void setDirection(Vector2 d)
    {
        direction = d;
    }

    private void destroySelf()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroySelf", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = direction * speed * Time.deltaTime;
        transform.Translate(move);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.tag);
        if(col.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
