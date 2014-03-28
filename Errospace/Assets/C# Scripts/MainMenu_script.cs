using UnityEngine;
using System.Collections;

public class MainMenu_script : MonoBehaviour {

	public Texture2D titleBanner;
	public Texture2D buttonExit;
	public Texture2D buttonPlay;
	
	GUIContent title = new GUIContent();
	GUIContent play = new GUIContent();
	GUIContent exit = new GUIContent();

	float nativeWidth = 1280;
	float nativeHeight = 800;
	 
	// Use this for initialization
	void Start () {
		title.image = titleBanner;
		play.image = buttonPlay;
		exit.image = buttonExit;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		GUIStyle noStyle = new GUIStyle();

		float rx = Screen.width / nativeWidth;
		float ry = Screen.height / nativeHeight;

		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(ry, ry, 1));
		float adjustedWidth = nativeWidth * (rx/ry);

		GUI.Label (new Rect((nativeWidth - 500)/2, (nativeHeight - 200)/2 - 100, 500, 200), title);

		if(GUI.Button(new Rect((nativeWidth - 300)/2, (nativeHeight - 100)/2 , 300, 100), play, noStyle)){
			Application.LoadLevel("SelectWorld");
		}
		
		if(GUI.Button(new Rect((nativeWidth - 300)/2, (nativeHeight - 100)/2 + 125, 300, 100), exit, noStyle)){
			Application.Quit ();
		}


//		var buttonWidth = 300;
//		var buttonHeight = 35;
//
//		if (GUI.Button (new Rect ((Screen.width-buttonWidth)/2,(Screen.height-buttonHeight+50)/2, Screen.width/2, buttonHeight), "PLAY")) {
//			Application.LoadLevel("SelectLevel");
//		}
//
//		if(GUI.Button (new Rect(20, Screen.height-buttonHeight-20, 35, 35),"EXIT")){
//			Application.Quit();
//
//		}
	}
}
