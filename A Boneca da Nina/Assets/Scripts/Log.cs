using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour {

	private float offset = 5;
	public float speed = 10;

	private Vector2 destination;
	private Vector2 initialPos;
	private bool direction;

	void Start () {
		initialPos = transform.position;
		destination = initialPos;
		destination += new Vector2 (0, offset);
		direction = true;

	}

	void Update () {
		if (direction) {
			if (transform.position.y >= destination.y) {
				destination = initialPos;
				destination -= new Vector2 (0, offset);
				direction = false;
			}
		} else {
			if (transform.position.y <= destination.y) {
				destination = initialPos;
				destination += new Vector2 (0, offset);
				direction = true;
			}
		}

		transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);	
	}
}
