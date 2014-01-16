using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class rocketMover : MonoBehaviour {

	public static float range = 10000;
	public bool isActive;

	Collider2D firstCollider;

	void OnTriggerEnter2D(Collider2D collider){
		firstCollider = collider;
	}
	/*
	void Start(){
		rigidbody2D.velocity = new Vector2 (0, 2);
	}*/
	void Update(){
		if(firstCollider != null && firstCollider.transform.name != "LevelBoundaries" && isActive){
			Vector3 offset = transform.position - firstCollider.transform.position;
			float mag = offset.magnitude;
			offset.Normalize ();
			print(offset);
			Vector2 force = new Vector2 (offset.x / mag / mag, offset.y / mag / mag);

			rigidbody2D.velocity = rigidbody2D.velocity - force;
		}
	}

}
