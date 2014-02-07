using UnityEngine;
using System.Collections;

public class gameStart : MonoBehaviour {

	public Transform rocket;
	public Transform planets;
	public Camera mainCamera;
	const float smooth = 0.2f;
	const float smoothDrag = 0.7f;
	const int maxZoom = 2;

	private Vector3 prevCamPos;
	private Vector3 mouseDownPos;

	public Texture2D zoominIcon;
	public Texture2D zoomoutIcon;
	public Texture2D goIcon;

	bool isGoButtonVisible = true;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (1)) {
			prevCamPos = mainCamera.transform.position;
			mouseDownPos = Input.mousePosition;
			print ("Hey.");
		}

		if (Input.GetMouseButton (1)) {
			Vector3 pointInSpace = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			Vector3 difference = pointInSpace - mainCamera.ScreenToWorldPoint(mouseDownPos);
			mainCamera.transform.position = prevCamPos - difference;
			print (prevCamPos);
		}

//		if(Input.GetMouseButton(1)){
//			RaycastHit R = new Ray();
//
//
//
//
//			Vector3 CameraPos;
//			float MouseX;
//			float MouseY;
//
//			MouseX = Input.GetAxis("Mouse X");
//			MouseY = Input.GetAxis("Mouse Y");
//
//			CameraPos = new Vector3(-MouseX, -MouseY, 0);
//
//			mainCamera.transform.position += CameraPos*smoothDrag;
//		}
	}

	void OnGUI () {
		int buttonWidth = 80;
		int buttonHeight = 40;
		int startVelocity = 4;
		
		int miniButtonWidth = 40;
		int miniButtonHeight = 40;

		GUIStyle guiStyle = new GUIStyle();
		guiStyle.padding = new RectOffset(0,0,0,0);

		if (GUI.RepeatButton (new Rect (Screen.width-50,60,miniButtonWidth, miniButtonWidth), new GUIContent(zoomoutIcon), guiStyle)) {
			mainCamera.orthographicSize+=smooth;
		}
		if (GUI.RepeatButton (new Rect (Screen.width-100,60, miniButtonWidth, miniButtonHeight), new GUIContent(zoominIcon), guiStyle)) {
			if(mainCamera.orthographicSize>maxZoom)
				mainCamera.orthographicSize-=smooth;
		}


		//The "GO" Button!
		if(isGoButtonVisible){
			if (GUI.Button (new Rect ((Screen.width-buttonWidth),(Screen.height-buttonHeight),buttonWidth, buttonHeight), new GUIContent(goIcon), guiStyle)) {
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
