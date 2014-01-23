using UnityEngine;
using System.Collections;

public class Pause_script : MonoBehaviour {

	//GUITexture pauseGUI; //The lighter gray GUITexture
//	pauseGUI.enabled = false;

	static bool onPause = false;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(onPause)
			Time.timeScale = 0;
		else {

			Time.timeScale = 1;
		}
	}

	void OnGUI () {
		if(onPause){
			GUILayout.BeginArea(new Rect( Screen.width/4, Screen.height/4, Screen.width-Screen.width/2, Screen.height/2 ));
				if(GUILayout.Button("Resume")){
					onPause = false;
				}
				if(GUILayout.Button("Galaxies")){
					Application.LoadLevel("SelectStage");
				}
				if(GUILayout.Button("Settings")){
					//gawa ng settings
				}
				if(GUILayout.Button("Main Menu")){
					Application.LoadLevel("MainMenu");
				}
			GUILayout.EndArea();
		}
		else {
			if (GUI.Button (new Rect (Screen.width-100,10,40,40), "Pause")) {
				onPause = true;
			}
			if (GUI.Button (new Rect (Screen.width-150,10,40,40), "Refresh")) { //this shouldn't really be here
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}
}
