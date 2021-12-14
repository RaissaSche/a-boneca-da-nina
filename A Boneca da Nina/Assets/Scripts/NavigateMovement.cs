using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateMovement : MonoBehaviour {

	public float intensity = 0;

	private float RotateSpeed = 6f;           
	private float _angle;
	private int Direction = 1;

	void Start () {
		transform.eulerAngles = new Vector3 ();
	}
	
	void Update () {
		if (Direction > 0) {
			if (transform.eulerAngles.z > 350 + intensity 
				&& transform.eulerAngles.z <= 357 + intensity
				&& RotateSpeed > 1f)
				RotateSpeed -= .2f;
			else if (Direction > 0 && transform.eulerAngles.z >= 340 + intensity
				&& transform.eulerAngles.z <= 350 + intensity) {
				Direction = -1;
				RotateSpeed = 0;
			}
		} 
		else 
		{
			if (transform.eulerAngles.z > 3 - intensity
				&& transform.eulerAngles.z < 10 - intensity
				&& RotateSpeed > 1f)
				RotateSpeed -= .2f;
			else if (transform.eulerAngles.z <= 20 - intensity 
				&& transform.eulerAngles.z >= 10 - intensity) {
				Direction = 1;
				RotateSpeed = 0;
			}
		}

		transform.Rotate (Vector3.forward *- (5 * Direction * RotateSpeed * Time.deltaTime));
		RotateSpeed += .07f;
	}
}
