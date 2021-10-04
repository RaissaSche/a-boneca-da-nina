 using System.Collections.Generic;
 using System.Collections;
 using UnityEngine;

 public class TirednessCollider : MonoBehaviour {

 	public Player player;
 	public float newMoveSpeed;
 	public float oldMoveSpeed;

 	void OnTriggerEnter2D (Collider2D collider) {
 		if (collider.CompareTag ("Player")) {
 			player.SetMoveSpeed (newMoveSpeed);
 		}
 	}

 	void OnTriggerExit2D (Collider2D collider) {
 		if (collider.CompareTag ("Player")) {
 			player.SetMoveSpeed (oldMoveSpeed);
 		}
 	}
 }