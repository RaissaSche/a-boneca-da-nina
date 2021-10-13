using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : MonoBehaviour {

	//Rotation
	private float RotateSpeed = 3f;     
	private float Radius = .5f;
	private Vector2 _centre;
	private float _angle;

	private void Start()
	{         
		_centre = transform.position;
	}   

	private void Update()     
	{          
		CircleMovement ();
	} 

	private void CircleMovement() {
		_angle += RotateSpeed * Time.deltaTime;          
		var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;         
		transform.position = _centre + offset;     
	}

}
