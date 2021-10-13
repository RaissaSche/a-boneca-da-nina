using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Status : byte
{
    SLEEPING = 0,
    WAKINGUP = 1,
    AWAKEN = 2,
    FALLINGASLEEP = 3
};

public class SleepyBehaviour : MonoBehaviour
{
    private const float wakingThreshold = 3.0f;
    private const float fallasleepThreshold = 8.0f;
    private const float shootCooldown = 3.0f;

    [SerializeField] private Status status;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Vector2 moveSpeed;
    private Transform player;
    private float shootRecharge;



    private void setStatusAwaken()
    {
        status = Status.AWAKEN;
    }

    private void setStatusSleeping()
    {
        status = Status.SLEEPING;
    }

    private void spawnYawn()
    {
        if (player != null)
        {
            Vector2 vec = player.position - transform.position;
            vec.Normalize();

            GameObject yawn = Instantiate(projectile, transform.position, transform.rotation);
            yawn.GetComponent<YawnBehaviour>().setDirection(vec);
        }

    }

    private void shoot()
    {
        shootRecharge = shootCooldown;
        spawnYawn();
        Invoke("spawnYawn", 0.4f);
        Invoke("spawnYawn", 0.8f);
    }

    private void turn()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        moveSpeed *= -1.0f;
    }

    private void checkDistance()
    {
        if (player != null)
        {
            Vector2 aux = transform.position - player.position;
            float distance = aux.magnitude;

            if(status == Status.SLEEPING)
            {
                if (distance < wakingThreshold)
                {
                    status = Status.WAKINGUP;
                    Invoke("setStatusAwaken", 1.0f);
                }
                else
                {
                    if (shootRecharge < 0.0f)
                    {
                        shoot();
                    }
                }
            }
            else if (status == Status.WAKINGUP)
            {

            }
            else if (status == Status.AWAKEN)
            {
                if (distance > fallasleepThreshold)
                {
                    status = Status.FALLINGASLEEP;
                    Invoke("setStatusSleeping", 1.0f);
                }
                else
                {
                    if(aux.x * moveSpeed.x > 0.0f)
                    {
                        turn();
                    }
                }
            }
            else if (status == Status.FALLINGASLEEP)
            {
                GetComponent<BackAndForth>().enabled = false;
            }

            Invoke("checkDistance", 0.5f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        status = Status.SLEEPING;
        shootRecharge = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        shootRecharge -= Time.deltaTime;
        if(status == Status.AWAKEN)
        {
            transform.Translate(moveSpeed*Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.transform;
            checkDistance();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = null;
        }
    }
}
