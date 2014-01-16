using UnityEngine;
using System.Collections;

public class startMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		var buttonWidth = 150;
		var buttonHeight = 30;

		//New Game
		if (GUI.Button (new Rect ((Screen.width-buttonWidth)/2,(Screen.height-buttonHeight+50)/2,buttonWidth, buttonHeight), "New Game")) {
			Application.LoadLevel("Level01");
		}
		//Select Level
		if (GUI.Button (new Rect ((Screen.width-buttonWidth)/2,(Screen.height+100)/2,buttonWidth, buttonHeight), "Continue")) {
			print ("You clicked the CONTINUE button!");
		}
		if (GUI.Button (new Rect ((Screen.width-buttonWidth)/2,(Screen.height+180)/2,buttonWidth, buttonHeight), "Quit")) {
			Application.Quit();
		}
	}
}
