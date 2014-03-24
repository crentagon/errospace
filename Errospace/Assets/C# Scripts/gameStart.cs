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
	public Texture2D refreshIcon;
	public Texture2D pauseIcon;

	static bool onPause = false;
	bool isGoButtonVisible = true;

	GUIContent goContent = new GUIContent();
	GUIStyle textStyle = new GUIStyle();
	public Font gameFont;
	private planetMover[] planetScripts;
	private static Vector3[] planetPositions;

	// Use this for initialization
	void Start () {
		//Set the local positions of the planets.
		planetScripts = planets.GetComponentsInChildren<planetMover>();
		int i=0;

		if(planetPositions != null){
			foreach(planetMover ps in planetScripts){
				ps.transform.localPosition = planetPositions[i];
				i+=1;
			}
		}
	}

	void Awake(){
		goContent.image = goIcon;
		textStyle.font = gameFont;
		textStyle.normal.textColor = Color.white;
	}
	
	void FixedUpdate () {
		rocketMover rocketScript = rocket.GetComponent<rocketMover>();
		if(onPause) {
			Time.timeScale = 0;
		}
		else {
			Time.timeScale = 1;
		}

		if (Input.GetMouseButtonDown (1)) {
			prevCamPos = mainCamera.transform.position;
			mouseDownPos = Input.mousePosition;
		}

		if (Input.GetMouseButton (1)) {
			Vector3 pointInSpace = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			Vector3 difference = pointInSpace - mainCamera.ScreenToWorldPoint(mouseDownPos);
			mainCamera.transform.position = prevCamPos - difference;
		}
	}

	void OnGUI () {
		int buttonWidth = 100;
		int buttonHeight = 50;
		int startVelocity = 4;
		
		int miniButtonWidth = 32;
		int miniButtonHeight = 32;

		if (onPause) {
			GUILayout.BeginArea (new Rect (Screen.width / 4, Screen.height / 4, Screen.width - Screen.width / 2, Screen.height / 2));
			if (GUILayout.Button ("Resume")) {
				onPause = false;
				Time.timeScale = 1;
			}
			if (GUILayout.Button ("Galaxies")) {
				onPause = false;
				Time.timeScale = 1;
				Application.LoadLevel ("SelectStage");
			}
			if (GUILayout.Button ("Settings")) {
				//gawa ng settings
			}
			if (GUILayout.Button ("Main Menu")) {
				Time.timeScale = 1;
				onPause = false;
				Application.LoadLevel ("MainMenu");
			}
			GUILayout.EndArea ();
		} else {
			GUIStyle guiStyle = new GUIStyle();
			guiStyle.padding = new RectOffset(0,0,0,0);
			
			//The pause/refresh icons
			if (GUI.Button (new Rect (Screen.width-miniButtonWidth-10,2*miniButtonHeight+20,miniButtonWidth,miniButtonHeight), new GUIContent(refreshIcon), guiStyle)) {
				//Grab the positions of the planets, and reload the level, assigning the positions of the planets
				//The following works, but it's a very super brute force implementation.
				int numPlanets = 0;
				planetScripts = planets.GetComponentsInChildren<planetMover>();
				//Count the number of planets
				foreach(planetMover ps in planetScripts){
					//print (ps.transform.localPosition);
					numPlanets += 1;
				}
				
				//Initialize array with that much number of planets, and store the planet's position.
				planetPositions = new Vector3[numPlanets];
				int i=0;
				foreach(planetMover ps in planetScripts){
					planetPositions[i] = ps.transform.localPosition;
					print (planetPositions[i]);
					i+=1;
				}
				print ("Number of planets: "+numPlanets);

				//Reload the level
				Application.LoadLevel(Application.loadedLevel);
				//planetMover[] planetScripts = planets.GetComponentsInChildren<planetMover>();



				print ("Reloaded the level!");
			}
			if (GUI.Button (new Rect (Screen.width-miniButtonWidth-10,3*miniButtonHeight+25,miniButtonWidth,miniButtonHeight), new GUIContent(pauseIcon), guiStyle)) { //this shouldn't really be here
				onPause = true;
			}
			
			//The zoomin, zoomout icons
			if (GUI.RepeatButton (new Rect (Screen.width-miniButtonWidth-10,10,miniButtonWidth, miniButtonWidth), new GUIContent(zoomoutIcon), guiStyle)) {
				mainCamera.orthographicSize+=smooth;
			}
			if (GUI.RepeatButton (new Rect (Screen.width-miniButtonWidth-10,miniButtonHeight+15, miniButtonWidth, miniButtonHeight), new GUIContent(zoominIcon), guiStyle)) {
				if(mainCamera.orthographicSize>maxZoom)
					mainCamera.orthographicSize-=smooth;
			}
			
			//The "GO" Button!
			if(isGoButtonVisible){
				GUIStyle goGuiStyle = new GUIStyle();
				goGuiStyle.padding = new RectOffset(0,0,0,0);

				if (GUI.Button (new Rect ((Screen.width-buttonWidth-10),(Screen.height-buttonHeight-10),buttonWidth, buttonHeight), goContent, goGuiStyle)) {
					//Hide this button.
					isGoButtonVisible = false;				
					//print ("After: "+isGoButtonVisible);
					
					//Accessing the scripts of the other components.
					rocketMover rocketScript = rocket.GetComponent<rocketMover>();
					
					//Make the rocket respond to the gravity of other planets.
					rocketScript.isActive = true;
					
					//Rocket lift off!
					rocketScript.rigidbody2D.velocity = new Vector2 (0, startVelocity);

					//Rocket fire and smoke!
					rocketScript.startTrail();
					
					//Planets are no longer movable.
					planetMover[] planetScripts = planets.GetComponentsInChildren<planetMover>();
					foreach(planetMover ps in planetScripts){
						ps.isMovable = false;
					}

				}
				GUI.Label(new Rect ((Screen.width-buttonWidth+7),(Screen.height-buttonHeight+10),buttonWidth, buttonHeight), "LAUNCH", textStyle);
			}
		}
	}
}
