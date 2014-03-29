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

	public string levelName;

	public static bool onPause = false;
	public bool showSideButtons = true;
	bool isGoButtonVisible = true;

	GUIContent goContent = new GUIContent();
	GUIStyle textStyle = new GUIStyle();
	GUIStyle textStyleStars = new GUIStyle();
	public Font gameFont;
	private planetMover[] planetScripts;
	private static Vector3[] planetPositions;
	private int oldStarCount;

	public AudioClip clipClick;
	public AudioClip clipPause;

	private bool willWait = true;

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
	}

	void Awake(){
		goContent.image = goIcon;
		textStyle.font = gameFont;
		textStyle.normal.textColor = Color.white;
		//int numPlanets = planets.childCount;
		//planetPositions = new Vector3[numPlanets];

		textStyleStars.font = gameFont;
		textStyleStars.normal.textColor = Color.white;
		textStyleStars.fontSize = 35;
		oldStarCount = 3-stars.childCount;

	}

	IEnumerator waitSelectScene(string selectedScene){
		while(willWait){
			audio.PlayOneShot(clipClick);
			yield return new WaitForSeconds(0.3f);
			willWait = false;
			Application.LoadLevel(selectedScene);
		}
	}
	
	IEnumerator waitReloadLevel(){
		while(willWait){
			audio.PlayOneShot(clipClick);
			yield return new WaitForSeconds(0.3f);
			willWait = false;			
			Application.LoadLevel(Application.loadedLevel);
		}
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

	void OnGUI () {
		int startVelocity = 4;

		//GUI.Label( new Rect (100f,100f,0f, 0f), "x " + oldStarCount);		
		textStyleStars.fontSize = 28*Screen.width/700;
		GUIStyle textStyleLevel = textStyleStars;
		textStyleLevel.fontSize = 20*Screen.width/700;

		GUI.Label (new Rect(Screen.width*2/100, Screen.height*2/100, 1,1), "WORLD "+levelName, textStyleLevel);

		GUI.Label( new Rect (Screen.width*1/12,Screen.height*94/100,Screen.width*1/10, Screen.height*1/12), "x " + oldStarCount, textStyleStars);
		GUI.Label( new Rect (Screen.width*1/35,Screen.height*91/100,Screen.width*1/12, Screen.height*1/12), new GUIContent(starIcon));



		if (onPause) {
			//Planets are no longer movable.
			planetMover[] planetScripts = planets.GetComponentsInChildren<planetMover>();
			foreach(planetMover ps in planetScripts){
				ps.isMovable = false;
			}

			GUIStyle guiStyle = new GUIStyle();
			guiStyle.padding = new RectOffset(0,0,0,0);

			GUI.Label( new Rect (((Screen.width/2)-(Screen.width*25/96)),((Screen.height/2)-(Screen.height*3/8)),Screen.width*4/5,Screen.height*4/5), new GUIContent(pauseWindow));
			if(GUI.Button (new Rect (((Screen.width/2)-(Screen.width*9/72)),((Screen.height/2)-(Screen.height*1/13)),Screen.width*3/8,Screen.height*3/25), buttonResume, guiStyle)){
				audio.PlayOneShot(clipClick);
				onPause = false;
				Time.timeScale = 1;
			}
			if(GUI.Button (new Rect (((Screen.width/2)-(Screen.width*9/72)),((Screen.height/2)-(Screen.height*-1/18)),Screen.width*3/8,Screen.height*3/25), buttonStages, guiStyle)){
				onPause = false;
				Time.timeScale = 1;
				StartCoroutine(waitSelectScene("SelectWorld"));
			}
			if(GUI.Button (new Rect (((Screen.width/2)-(Screen.width*9/72)),((Screen.height/2)-(Screen.height*-27/144)),Screen.width*3/8,Screen.height*3/25), buttonMainMenu, guiStyle)){
				onPause = false;
				Time.timeScale = 1;
				StartCoroutine(waitSelectScene("MainMenu"));
			}
//			GUILayout.BeginArea (new Rect (Screen.width / 4, Screen.height / 4, Screen.width - Screen.width / 2, Screen.height / 2));
//			if (GUILayout.Button ("Resume")) {
//				onPause = false;
//				Time.timeScale = 1;
//			}
//			if (GUILayout.Button ("Galaxies")) {
//				onPause = false;
//				Time.timeScale = 1;
//				Application.LoadLevel ("SelectStage");
//			}
//			if (GUILayout.Button ("Main Menu")) {
//				Time.timeScale = 1;
//				onPause = false;
//				Application.LoadLevel ("MainMenu");
//			}
//			GUILayout.EndArea ();
		} else if(showSideButtons){
			GUIStyle guiStyle = new GUIStyle();
			guiStyle.padding = new RectOffset(0,0,0,0);


			//The pause/refresh icons
			if (GUI.Button (new Rect (Screen.width*92/100,Screen.height*23/100,Screen.width*1/12,Screen.height*1/12), new GUIContent(refreshIcon), guiStyle)) {
				//Grab the positions of the planets, and reload the level, assigning the positions of the planets
				//The following works, but it's a very super brute force implementation.
				/*int numPlanets = 0;
				planetScripts = planets.GetComponentsInChildren<planetMover>();
				//Count the number of planets
				foreach(planetMover ps in planetScripts){
					//print (ps.transform.localPosition);
					numPlanets += 1;
				}
				*/


				int numPlanets = planets.childCount;
				//Initialize array with that much number of planets, and store the planet's position.
				planetPositions = new Vector3[numPlanets];
				int i=0;
				foreach(planetMover ps in planetScripts){
					planetPositions[i] = ps.transform.localPosition;
					i+=1;
				}

				//Reload the level
				
				StartCoroutine(waitReloadLevel());
			}
			if (GUI.Button (new Rect (Screen.width*92/100,Screen.height*33/100,Screen.width*1/12,Screen.height*1/12), new GUIContent(pauseIcon), guiStyle)) { //this shouldn't really be here
				audio.PlayOneShot(clipPause);
				onPause = true;
			}
			
			//The zoomin, zoomout icons
			if (GUI.RepeatButton (new Rect (Screen.width*92/100,Screen.height*3/100,Screen.width*1/12,Screen.height*1/12), new GUIContent(zoomoutIcon), guiStyle)) {
				audio.PlayOneShot(clipClick);
				mainCamera.orthographicSize+=smooth;
			}
			if (GUI.RepeatButton (new Rect (Screen.width*92/100,Screen.height*13/100,Screen.width*1/12,Screen.height*1/12), new GUIContent(zoominIcon), guiStyle)) {
				if(mainCamera.orthographicSize>maxZoom){
					audio.PlayOneShot(clipClick);
					mainCamera.orthographicSize-=smooth;
				}
			}
			
			//The "GO" Button!
			if(isGoButtonVisible){
				GUIStyle goGuiStyle = new GUIStyle();
				goGuiStyle.padding = new RectOffset(0,0,0,0);

				planetMover[] planetScripts = planets.GetComponentsInChildren<planetMover>();
				foreach(planetMover ps in planetScripts){
					ps.isMovable = true;
				}

				if (GUI.Button (new Rect (Screen.width-Screen.width*22/100,Screen.height*88/100,Screen.width*1/3,Screen.height*1/9), goContent, goGuiStyle)) {
					audio.PlayOneShot(clipClick);

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
					planetScripts = planets.GetComponentsInChildren<planetMover>();
					foreach(planetMover ps in planetScripts){
						ps.isMovable = false;
					}

					//Display starcount at bottomright corner
					//int starCount = stars.childCount;
					//print ("Number of stars: "+starCount);
					//GUI.Label(new Rect(Screen.width/2, Screen.height*0.2f, 150f, 50f), OldStarCount);
					//int Money = 3;

					print ("x " + oldStarCount);
					//GUI.Label(new Rect(10, 10, 140, 20), "YEAH!");
				}
				
				//textStyle.fontSize = 20*Screen.width/700;
				//GUI.Label(new Rect (Screen.width*167/200,Screen.height*90/100,Screen.width*6/24,Screen.height*3/24), "LAUNCH", textStyle);

			}
		}
	}
}
