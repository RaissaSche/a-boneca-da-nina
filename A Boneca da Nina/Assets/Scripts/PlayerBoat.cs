using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBoat : MonoBehaviour {
	
	public float offset = 0f;
	public float speed = 20;

	private Vector3 destination;
	private float step;
	private float speedX = 10;

	private bool inBossPlace = false;
    private bool collided = false;
    private bool returning = false;

    GameObject bt;

	void Start() {
		bt = GameObject.Find ("SkipButton");
		bt.SetActive (false);
	}

	public void ClickUp() {
		if (destination.y <= 0)
			destination += new Vector3 (0, offset);
	}

	public void ClickDown() {
		if (destination.y >= 0)
			destination -= new Vector3 (0, offset);		
	}

	void Update() {
		if (inBossPlace) return;  

        transform.localPosition += new Vector3(speedX * Time.deltaTime, 0);

        if (!collided)
        {
            destination.x = transform.localPosition.x;
        } else if(!returning)
        {
            destination.x -= 3;
            returning = true;
        }

		transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, speed * Time.deltaTime);

        if (destination.x == transform.localPosition.x)
        {
            collided = false;
            returning = false;
        }
	}

	void OnTriggerEnter2D(Collider2D other) {
		inBossPlace = other.gameObject.name == "BossPlace";

        if (inBossPlace)
		    bt.SetActive(true);

        if (other.gameObject.tag == "Log")
            collided = true;
    }
}
