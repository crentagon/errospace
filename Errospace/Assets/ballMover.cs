using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ballMover : MonoBehaviour {

	public static float range = 10000;
	Collider2D firstCollider;

	void OnTriggerEnter2D(Collider2D collider)
	{
		firstCollider = collider;

	}
	void Start(){
		rigidbody2D.velocity = new Vector2 (0, 2);
	}
	void Update(){
		Vector3 offset = transform.position - firstCollider.transform.position;
		rigidbody2D.velocity = rigidbody2D.velocity - (Vector2)offset;
	}
}
