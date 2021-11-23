using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform objectToFollow;
    public Vector3 offsetOriginal;
    public int delay;
    public float moveSpeed;
    private Vector3 offset;
    private float move;
    private float newX;
    private int time = 0;

    void Start()
    {
        offset = offsetOriginal;
        move = Time.deltaTime * moveSpeed;
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
                offset.x -= delay;
                time = 0;
            }
        }

        //colocar "gradiente" pra ir ficando pra trás aos poucos
        //debugar valor do j aqui
        if (transform.position.x >= 100 && offset.x <= offsetOriginal.x)
        {
            Debug.Log("original:" + offsetOriginal.x);
            Debug.Log("novo:" + offset.x);
            newX = transform.position.x + delay;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(newX, transform.position.y, transform.position.z), move);
            offset.x = newX;
        }
        //se ficar parada (mesma posição por x tempo), somar ao offset até o valor original
        else
        {
            transform.position = objectToFollow.position + offset;
        }
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform objectToFollow;
    public Vector3 offsetOriginal;
    public int delay;
    public float moveSpeed;
    private Vector3 offset;
    private float move;
    private float newX;
    private int time = 0;

    void Start()
    {
        offset = offsetOriginal;
        move = Time.deltaTime * moveSpeed;
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag.Equals("SlowMom") && offset.x <= offsetOriginal.x){ 
            //Debug.Log("original:" + offsetOriginal.x);
            //Debug.Log("novo:" + offset.x);
            newX = transform.position.x + delay;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(newX, transform.position.y, transform.position.z), move);
            offset.x = newX;
        }
        else if(other.tag.Equals("SlowerMom") && offset.x <= offsetOriginal.x){
            //Debug.Log("original:" + offsetOriginal.x);
            //Debug.Log("novo:" + offset.x);
            newX = transform.position.x + (delay * 2);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(newX, transform.position.y, transform.position.z), move);
            offset.x = newX;
        }
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
                offset.x -= delay;
                time = 0;
            }
        }

        //colocar "gradiente" pra ir ficando pra trás aos poucos
        //debugar valor do j aqui

    
        /*if (Input.GetKeyDown("j") && offset.x <= offsetOriginal.x)
        {
            Debug.Log("original:" + offsetOriginal.x);
            Debug.Log("novo:" + offset.x);
            newX = transform.position.x + delay;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(newX, transform.position.y, transform.position.z), move);
            offset.x = newX;
        }
        //se ficar parada (mesma posição por x tempo), somar ao offset até o valor original
        else
        {
            transform.position = objectToFollow.position + offset;
        }*/
    //}
//}
