using UnityEngine;
using System.Collections;

public class MainMenu_script : MonoBehaviour {



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		var buttonWidth = 300;
		var buttonHeight = 30;

		if (GUI.Button (new Rect ((Screen.width-buttonWidth)/2,(Screen.height-buttonHeight+50)/2, buttonWidth, buttonHeight), "PLAY")) {
			Application.LoadLevel("SelectLevel");
		}

		if(GUI.Button (new Rect(20, Screen.height-buttonHeight-20, 35, 35),"EXIT")){
			Application.Quit();

		}
	}
}
