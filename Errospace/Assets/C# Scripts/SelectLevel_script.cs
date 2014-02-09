using UnityEngine;
using System.Collections;
using System.IO;

public class SelectLevel_script : MonoBehaviour {

	public Texture2D backIcon;
	public Texture2D level01Icon;
	public Texture2D level02Icon;
	public Texture2D level03Icon;
	public Texture2D level04Icon;
	public Texture2D level05Icon;
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
		int buttonWidth = 128;
		int buttonHeight = 64;

		int miniButtonWidth = 32;
		int miniButtonHeight = 32;

		GUIStyle guiStyle = new GUIStyle();
		guiStyle.padding = new RectOffset(0,0,0,0);

		if(GUI.Button (new Rect (10,10,buttonWidth, buttonHeight), new GUIContent(level01Icon), guiStyle)){
			File.WriteAllText(path+"\\lvl.sav", "1"); //level 1
			Application.LoadLevel("SelectStage");
		}

		/*
		if (GUI.Button (new Rect ( 15 + buttonWidth*(1-1), 20, 100, 40), "Level 1")) {
			File.WriteAllText(path+"\\lvl.sav", "1"); //level 1
			Application.LoadLevel("SelectStage");
		} 


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

		if(GUI.Button (new Rect (10,(Screen.height-miniButtonHeight-10),miniButtonWidth, miniButtonHeight), new GUIContent(backIcon), guiStyle)){
			Application.LoadLevel("MainMenu");
		}
	}
}
