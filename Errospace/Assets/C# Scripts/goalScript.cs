using UnityEngine;
using System.Collections;

public class goalScript : MonoBehaviour {
		
	Collider2D firstCollider;

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter2D(Collider2D collider){
		firstCollider = collider;
	}

	// Update is called once per frame
	void Update(){
		if(firstCollider != null && firstCollider.transform.name == "Rocket"){
			rocketMover rocketScript = firstCollider.GetComponent<rocketMover>();
			rocketScript.isActive = false;
			rocketScript.rigidbody2D.velocity = new Vector2 (0, 0);
			rocketScript.transform.position = transform.position - new Vector3 (0,0,1);

			print("Game finished!");
		}
	}
}
