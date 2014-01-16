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
		var buttonWidth = 150;
		var buttonHeight = 30;

		if(firstCollider != null && firstCollider.transform.name == "Rocket"){
			//Rocket must stop at the goal position
			rocketMover rocketScript = firstCollider.GetComponent<rocketMover>();
			rocketScript.isActive = false;
			rocketScript.rigidbody2D.velocity = new Vector2 (0, 0);
			rocketScript.transform.position = transform.position - new Vector3 (0,0,1);

			//Display next level button
			if (GUI.Button (new Rect ((Screen.width-buttonWidth)/2,(Screen.height-buttonHeight+50)/2,buttonWidth, buttonHeight), "Next Level")) {
				Application.LoadLevel(Application.loadedLevel+1);
			}
			print("Game finished!");
		}
	}
}
