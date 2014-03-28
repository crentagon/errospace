using UnityEngine;
using System.Collections;

public class MainMenu_script : MonoBehaviour {

	public Texture2D titleBanner;
	public Texture2D buttonExit;
	public Texture2D buttonPlay;
	
	GUIContent title = new GUIContent();
	GUIContent play = new GUIContent();
	GUIContent exit = new GUIContent();
	 
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
		GUIStyle centeredStyle = GUI.skin.GetStyle("Label");
		centeredStyle.alignment = TextAnchor.UpperCenter;

		GUI.Label (new Rect(Screen.width/4, Screen.height/4, Screen.width/2, Screen.height/2), title, centeredStyle);

		if(GUI.Button(new Rect(Screen.width/3, Screen.height/3, Screen.width/4, Screen.height/4), play, centeredStyle)){
			Application.LoadLevel("SelectWorld");
		}

		if(GUI.Button(new Rect(Screen.width/3, Screen.height/2, Screen.width/4, Screen.height/4), exit, centeredStyle)){
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
