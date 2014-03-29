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

	private planetMover[] planetScripts;
	private static Vector3[] planetPositions;

	public AudioClip clipStar;
	public AudioClip clipLose;
	public AudioClip clipPortal;

	private bool willWait = true;

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

	IEnumerator waitGameOver(){
		while(willWait){
			audio.PlayOneShot(clipLose);
			this.isActive = false;
			this.stopTrail();
			yield return new WaitForSeconds(1.0f);
			willWait = false;
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	void Start () {
		if(planetPositions != null){
			try{
				for(int i=0; i<planets.Length; i++){
					planets[i].transform.localPosition = planetPositions[i];
				}
			}
			catch(UnityException e){
				print (e);
			}
		}
		
		planetPositions = new Vector3[0];
	}

	void FixedUpdate(){
		//print (firstCollider.transform.name);

		if(isActive){
			
			if (firstCollider != null && firstCollider.transform.name == "Star") {
				audio.PlayOneShot(clipStar);
				starCount+=1;
				Destroy(firstCollider.gameObject);
				firstCollider = null;
			}
			
			else if(firstCollider != null){
				//If it's hit a planet:
				for(int i=0; i<planets.Length; i++){
					if(firstCollider.transform.name == planets[i].transform.name){
						
						planetPositions = new Vector3[planets.Length];
						for(int j=0; j<planets.Length; j++){
							planetPositions[j] = planets[j].transform.localPosition;
						}

						print ("Collided with: "+planets[i].transform.name+" Position: "+planets[i].transform.localPosition);

						StartCoroutine(waitGameOver());
					}
				}

				//If it's hit a blackhole
				for(int i=0; i<holes.Length; i++){
					if(firstCollider.transform.name == holes[i].transform.name){
						planetPositions = new Vector3[planets.Length];
						for(int j=0; j<planets.Length; j++){
							planetPositions[j] = planets[j].transform.localPosition;
						}

						StartCoroutine(waitGameOver());
					}
				}

				//If it's hit a wall
				if(firstCollider.transform.name == "Wall"){
					planetPositions = new Vector3[planets.Length];
					for(int j=0; j<planets.Length; j++){
						planetPositions[j] = planets[j].transform.localPosition;
					}
					
					StartCoroutine(waitGameOver());
				}

				//If it hits a portal
				if(firstCollider.transform.parent.transform.name == "PortalPair"){
					System.String portalName = firstCollider.transform.name;
					//print ("Hit a portal: "+portalName.Substring(9));
					//print ("Hit a portal: "+portalName.Substring(0, 9));
					if(portalName.Substring(9) == "1"){
						var otherPortal = GameObject.Find (portalName.Substring(0, 9)+"2");
						this.transform.localPosition = otherPortal.transform.localPosition;

						audio.PlayOneShot(clipPortal);
					}
				}

			}

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
