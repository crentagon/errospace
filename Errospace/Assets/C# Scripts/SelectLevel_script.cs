using UnityEngine;
using System.Collections;
using System.IO;

public class SelectLevel_script : MonoBehaviour {
	
	int buttonWidth = 100;
	int buttonHeight = 30;

	//Get current working directory + Text folder
	string path = Directory.GetCurrentDirectory () + "\\Text";

	// Use this for initialization
	void Start () {


		if (!(Directory.Exists (path))) {
			Directory.CreateDirectory (path);
			print(path);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI () {


		if (GUI.Button (new Rect ( 15 + buttonWidth*(1-1), 20, 100, 40), "Level 1")) {
			File.WriteAllText(path+"\\lvl.sav", "1"); //level 1
			Application.LoadLevel("SelectStage");
		} 

		/*
		if (GUI.Button (new Rect ((Screen.width-buttonWidth)/2,(Screen.height-buttonHeight+50)/2,buttonWidth, buttonHeight), "Level 1")) {
			File.WriteAllText(path+"\\lvl.sav", "1"); //level 1
			Application.LoadLevel("SelectStage");z
		}
		if (GUI.Button (new Rect ((Screen.width-buttonWidth)/2,(Screen.height-buttonHeight+50),buttonWidth, buttonHeight), "Level 2")) {
			File.WriteAllText(path+"\\lvl.sav", "2"); //level 2
			Application.LoadLevel("SelectStage");
		}
		if (GUI.Button (new Rect ((Screen.width-buttonWidth)/2,(Screen.height-buttonHeight+50)/3,buttonWidth, buttonHeight), "Level 3")) {
			File.WriteAllText(path+"\\lvl.sav", "3"); //level 3
			Application.LoadLevel("SelectStage");
		}
		*/

		if(GUI.Button (new Rect(20, Screen.height-buttonHeight-20, 35, 35), "<-")) {
			Application.LoadLevel("MainMenu");
		}
	}
}
