using UnityEngine;
using System.Collections;

public class gameStart : MonoBehaviour {

	public Transform rocket;
	public Transform planets;
	public Transform stars;

	public Camera mainCamera;
	const float smooth = 0.2f;
	const float smoothDrag = 0.7f;
	const int maxZoom = 2;

	private float width;
	private float height;

	private Vector3 prevCamPos;
	private Vector3 mouseDownPos;

	public Texture2D zoominIcon;
	public Texture2D zoomoutIcon;
	public Texture2D goIcon;
	public Texture2D refreshIcon;
	public Texture2D pauseIcon;
	public Texture2D starIcon;

	public Texture2D pauseWindow;
	public Texture2D buttonResume;
	public Texture2D buttonStages;
	public Texture2D buttonMainMenu;

	public static bool onPause = false;
	bool isGoButtonVisible = true;

	GUIContent goContent = new GUIContent();
	GUIStyle textStyle = new GUIStyle();
	GUIStyle textStyleStars = new GUIStyle();

	public Font gameFont;
	private planetMover[] planetScripts;
	private static Vector3[] planetPositions;
	private int oldStarCount;

	// Use this for initialization
	void Start () {
		//Set the local positions of the planets.
		planetScripts = planets.GetComponentsInChildren<planetMover>();
		int i=0;

		if(planetPositions != null){
			print (Application.loadedLevel+": PLANET POSITIONS NOT NULL!");
			foreach(planetMover ps in planetScripts){
				ps.transform.localPosition = planetPositions[i];
				i+=1;
			}
			planetPositions = new Vector3[0];
		}

		width = 1280;
		height = 800;
	}

	void Awake(){
		goContent.image = goIcon;

		textStyle.font = gameFont;
		textStyle.normal.textColor = Color.white;
		textStyle.fontSize = 18;

		textStyleStars.font = gameFont;
		textStyleStars.normal.textColor = Color.white;
		textStyleStars.fontSize = 35;

		oldStarCount = 3-stars.childCount;
	}

	void Update(){
		//int starCount = stars.childCount;
		int newStarCount = 3-stars.childCount;

		if(newStarCount != oldStarCount){
			oldStarCount = newStarCount;
		}

		//print ("Number of stars: "+starCount);
		//GUI.Label();
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

	void DrawQuad(Rect position, Color color) {
		Texture2D texture = new Texture2D(1, 1);
		texture.SetPixel(0,0,color);
		texture.Apply();
		GUI.skin.box.normal.background = texture;
		GUI.Box(position, GUIContent.none);
	}

	void OnGUI () {
		int startVelocity = 4;
		
		int miniButtonWidth = 32;
		int miniButtonHeight = 32;

		GUIStyle guiStyle = new GUIStyle();
		
		float rx = Screen.width / width;
		float ry = Screen.height / height;
		
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(ry, ry, 1));
		float adjustedWidth = width * (rx/ry);

		GUI.Label(new Rect(55f,20f,50f, 50f), "x " + oldStarCount, textStyleStars);
		GUI.Label(new Rect(10f,10f,40f, 40f), new GUIContent(starIcon));

		if (onPause) {
			// Define pause window bounds
			Rect pauseRect = new Rect(
				(width - pauseWindow.width)/2,
				(height - pauseWindow.height)/2,
				pauseWindow.width,
				pauseWindow.height);

			// Define pause menu buttons' bounds
			Rect resumeRect = new Rect(
				(width - buttonResume.width)/2,
				(height - buttonResume.height)/2 - 60,
				buttonResume.width,
				buttonResume.height);
			Rect stagesRect = new Rect(
				(width - buttonResume.width)/2,
				resumeRect.y + buttonResume.height + 10,
				buttonResume.width,
				buttonResume.height);
			Rect mainmenuRect = new Rect(
				(width - buttonResume.width)/2,
				stagesRect.y + buttonResume.height + 10,
				buttonResume.width,
				buttonResume.height);

			// Faded background
			DrawQuad(new Rect(0,0,width,height), new Color(0,0,0,0.5f));

			// Pause window
			GUI.Label(pauseRect, new GUIContent(pauseWindow));

			// Resume button
			if(GUI.Button(resumeRect, buttonResume, guiStyle)){
				onPause = false;
				Time.timeScale = 1;
			}

			// Stages button
			if(GUI.Button(stagesRect, buttonStages, guiStyle)){
				onPause = false;
				Time.timeScale = 1;
				Application.LoadLevel ("SelectWorld");
			}

			// Main menu button
			if(GUI.Button(mainmenuRect, buttonMainMenu, guiStyle)){
				onPause = false;
				Time.timeScale = 1;
				Application.LoadLevel ("MainMenu");
			}
		}
		else {
			// Define object bounds
			Rect zoomoutRect = new Rect(
				(width - zoomoutIcon.width - 10),
				10,
				zoomoutIcon.width,
				zoomoutIcon.height);
			Rect zoominRect = new Rect(
				(width - zoominIcon.width - 10),
				zoominIcon.height + 20,
				zoominIcon.width,
				zoominIcon.height);
			Rect refreshRect = new Rect(
				(width - refreshIcon.width - 10),
				refreshIcon.height*2 + 30,
				refreshIcon.width,
				refreshIcon.height);
			Rect pauseRect = new Rect(
				(width - pauseIcon.width - 10),
				pauseIcon.height*3 + 40,
				pauseIcon.width,
				pauseIcon.height);
			Rect launchButtonRect = new Rect(
				(width - goIcon.width - 10),
				(height - goIcon.height - 10),
				goIcon.width,
				goIcon.height);
			Rect launchTextRect = new Rect(
				(width - goIcon.width + 12),
				(height - goIcon.height + 18),
				goIcon.width,
				goIcon.height);

			//Zoom out button
			if (GUI.RepeatButton (zoomoutRect, new GUIContent(zoomoutIcon), guiStyle)) {
				mainCamera.orthographicSize += smooth;
			}

			//Zoom in button
			if (GUI.RepeatButton (zoominRect, new GUIContent(zoominIcon), guiStyle)) {
				if(mainCamera.orthographicSize > maxZoom)
					mainCamera.orthographicSize -= smooth;
			}

			//Refresh button
			if (GUI.Button (refreshRect, new GUIContent(refreshIcon), guiStyle)) {
				//Grab the positions of the planets, and reload the level, assigning the positions of the planets
				int numPlanets = planets.childCount;
				//Initialize array with that much number of planets, and store the planet's position.
				planetPositions = new Vector3[numPlanets];
				int i = 0;
				foreach(planetMover ps in planetScripts){
					planetPositions[i] = ps.transform.localPosition;
					i += 1;
				}

				//Reload the level
				Application.LoadLevel(Application.loadedLevel);
				print ("Reloaded the level!");
			}

			//Pause button
			if (GUI.Button (pauseRect, new GUIContent(pauseIcon), guiStyle)) {
				onPause = true;
			}

			//Launch button
			if(isGoButtonVisible){
				if (GUI.Button (launchButtonRect, goContent, guiStyle)) {
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

					print ("x " + oldStarCount);
				}
				GUI.Label(launchTextRect, "LAUNCH!", textStyle);

			}
		}
	}
}
