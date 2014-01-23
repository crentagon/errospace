using UnityEngine;
using System.Collections;
using System.IO;

public class SelectStage_script : MonoBehaviour {

	string curLevel;
	int numStages;

	//Get current working directory + Text folder
	string path = Directory.GetCurrentDirectory () + "\\Text";

	// Use this for initialization
	void Start () {


		//lvl.sav contains the curLevel selectedby user in the previous script (SelectLevel_script)
		curLevel = File.ReadAllText (path + "\\lvl.sav");


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){

		var buttonHeight = 100;
		var buttonWidth = 100;

		if (GUI.Button (new Rect ( 15 + buttonWidth*(1-1), 20, 40, 40), "1")) {
			File.WriteAllText(path+"\\stg.sav", "1"); //stage 1
//			Application.LoadLevel("GameScene");
			Application.LoadLevel("Level01");
		}

		if(GUI.Button (new Rect(20, Screen.height-buttonHeight-20, 35, 35), "<-")) {
			Application.LoadLevel("SelectLevel");
		}

		/*
		//GUILayout.BeginArea(new Rect(Screen.width-120, Screen.height-150, 100, 200));
		for(int s = 1; s <= numStages; s++){
			//if (GUI.Button (new Rect (10,(Screen.height-100)-50,40,40), ToString(s))) {
			if (GUI.Button (new Rect ( 15 + buttonWidth*(s-1), 20, 40, 40), s.ToString())) {
				Application.LoadLevel("GameScene");
			}
		}
		*/
	}
}
