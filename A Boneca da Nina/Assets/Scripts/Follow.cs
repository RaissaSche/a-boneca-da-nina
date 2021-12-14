using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform objectToFollow;
    public Vector3 offsetOriginal;
    private Vector3 offset;
    public float delaySlow, delaySlower, moveSpeed;
    private float move, newX;
    private int time = 0;

    void Start()
    {
        offset = offsetOriginal;
        move = Time.deltaTime * moveSpeed;

        var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (!Input.anyKey)
        {
            time = time + 1;
        }
        else
        {
            time = 0;
        }

        if (time == 100)
        {
            if (offset.x < offsetOriginal.x)
            {
                offset.x -= delaySlow;
                time = 0;
            }
        }

        transform.position = objectToFollow.position + offset;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (offset.x <= offsetOriginal.x)
        {
            if (collider.CompareTag("SlowMom") && !collider.gameObject.GetComponent<SlowMom>().getHasPassed())
            {
                newX = transform.position.x + delaySlow;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(newX, transform.position.y, transform.position.z), move);
                offset.x = newX;
                collider.gameObject.GetComponent<SlowMom>().setHasPassed(true);
            }
            else if (collider.CompareTag("SlowerMom") && !collider.gameObject.GetComponent<SlowMom>().getHasPassed())
            {
                newX = transform.position.x + delaySlower;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(newX, transform.position.y, transform.position.z), move);
                offset.x = newX;
                collider.gameObject.GetComponent<SlowMom>().setHasPassed(true);
            }
        }
        //se ficar parada (mesma posição por x tempo), somar ao offset até o valor original
        else
        {
            transform.position = objectToFollow.position - offset;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("SlowMom") || collider.CompareTag("SlowerMom"))
        {
            transform.position = objectToFollow.position - offset;
        }
    }
}
