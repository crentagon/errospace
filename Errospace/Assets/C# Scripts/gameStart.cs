using UnityEngine;
using System.Collections;

public class gameStart : MonoBehaviour {

	public Transform rocket;
	public Transform planets;
	public Camera mainCamera;
	const float smooth = 0.2f;
	const float smoothDrag = 0.7f;
	const int maxZoom = 2;

	bool isGoButtonVisible = true;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(1)){
			Vector3 CameraPos;
			float MouseX;
			float MouseY;

			MouseX = Input.GetAxis("Mouse X");
			MouseY = Input.GetAxis("Mouse Y");

			CameraPos = new Vector3(-MouseX, -MouseY, 0);

			mainCamera.transform.position += CameraPos*smoothDrag;
		}
	}

	void OnGUI () {
		int buttonWidth = 70;
		int buttonHeight = 30;
		int startVelocity = 4;
		
		int miniButtonWidth = 40;
		int miniButtonHeight = 40;


		if (GUI.RepeatButton (new Rect (Screen.width-50,50,miniButtonWidth, miniButtonWidth), "out")) {
			mainCamera.orthographicSize+=smooth;
		}
		if (GUI.RepeatButton (new Rect (Screen.width-100,50, miniButtonWidth, miniButtonHeight), "in")) {
			if(mainCamera.orthographicSize>maxZoom)
				mainCamera.orthographicSize-=smooth;
		}


		//The "GO" Button!
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
		if (GUI.Button (new Rect (Screen.width-100,10,40,40), "Refresh")) { //should be an icon
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
