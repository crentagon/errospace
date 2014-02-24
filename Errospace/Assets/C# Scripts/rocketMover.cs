using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class rocketMover : MonoBehaviour {

	public static float range = 10000;
	public bool isActive;
	public Transform[] planets;
	public Transform[] holes;
	public ParticleSystem trail;
	Collider2D firstCollider;
	public int starCount = 0;

	public void startTrail(){
		trail.Play();
	}

	public void stopTrail(){
		trail.Stop();
	}

	void OnTriggerEnter2D(Collider2D collider){
		firstCollider = collider;
	}
	
	void OnColliderExit2D(Collider2D collider){
		firstCollider = null;
	}

	void FixedUpdate(){
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
					float planetFactor = 45.0f;
					Vector2 force = - offset.normalized * planetFactor / offset.sqrMagnitude;
					rigidbody2D.AddForce(force);
				}
			}
			if(holes != null){
				for(int i=0; i < holes.Length; i++){
					Vector3 offset = transform.position - holes[i].transform.position;
					float holeFactor = 65.0f;
					float drag = 0.9f;
					Vector2 force = - offset.normalized * holeFactor / offset.sqrMagnitude;
					if(offset.magnitude < 4.0){ // If close enough, start culling speed
						print("decreasing speed");
						force = force - rigidbody2D.velocity * drag / offset.magnitude;
					}					
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
