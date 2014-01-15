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
			rigidbody2D.velocity = rigidbody2D.velocity - (Vector2)offset;
		}
	}

}
