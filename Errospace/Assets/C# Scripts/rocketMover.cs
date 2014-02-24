using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class rocketMover : MonoBehaviour {

	public static float range = 10000;
	public bool isActive;
	public Transform[] planets;
	public Transform[] holes;
	public Transform trail;
	Collider2D firstCollider;
	public int starCount = 0;

	void OnTriggerEnter2D(Collider2D collider){
		firstCollider = collider;
	}
	
	void OnColliderExit2D(Collider2D collider){
		firstCollider = null;
	}
	void Update(){
		//print (firstCollider.transform.name);
		if (firstCollider != null && firstCollider.transform.name == "Star") {
			starCount+=1;
			Destroy(firstCollider.gameObject);
			firstCollider = null;
		}

		if(isActive){
			if(planets != null){
				for(int i=0; i < planets.Length; i++){
					Vector3 offset = transform.position - planets[i].transform.position;
					float factor = 45.0f;
					Vector2 force = - offset.normalized * factor / offset.sqrMagnitude;
					rigidbody2D.AddForce(force);
				}
			}
			if(holes != null){
				for(int i=0; i < holes.Length; i++){
					Vector3 offset = transform.position - holes[i].transform.position;
					float factor = 65.0f;
					Vector2 force = - offset.normalized * factor / offset.sqrMagnitude;
					force = force - rigidbody2D.velocity * 0.1f;
					rigidbody2D.AddForce(force);
				}
			}
			transform.localRotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg*Mathf.Atan2(rigidbody2D.velocity.y,rigidbody2D.velocity.x)+270);
		}
		else{
			rigidbody2D.velocity = new Vector2 (0, 0);
		}
	}

}
