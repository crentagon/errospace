using UnityEngine;
using System.Collections;

public class gameStart : MonoBehaviour {

	public Transform rocket;
	public Transform planets;

	bool isGoButtonVisible = true;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI () {
		var buttonWidth = 70;
		var buttonHeight = 30;
		var startVelocity = 4;

		//print ("Before: "+isGoButtonVisible);
		//The "GO" Button!
		//TO-DO: Bug on the button, need to double click before button disappears. :u
		if(isGoButtonVisible){
			if (GUI.Button (new Rect ((Screen.width-buttonWidth-10),(Screen.height-buttonHeight-10),buttonWidth, buttonHeight), "Go!")) {
				//Hide this button.
				isGoButtonVisible = false;				
				//print ("After: "+isGoButtonVisible);

				//Accessing the scripts of the other components.
				rocketMover rocketScript = rocket.GetComponent<rocketMover>();
				planetMover planetScript = planets.GetComponentInChildren<planetMover>();

				//Make the rocket respond to the gravity of other planets.
				rocketScript.isActive = true;

				//Rocket lift off!
				rocketScript.rigidbody2D.velocity = new Vector2 (0, startVelocity);

				//Planets are no longer movable.
				planetScript.isMovable = false;
			}
		}
	}
}
