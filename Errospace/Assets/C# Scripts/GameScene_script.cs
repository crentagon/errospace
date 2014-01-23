using UnityEngine;
using System.Collections;
using System.IO;

/*
 * This script manages the requisites for the game flow.
 * Loads positions, etc.
 * 
 */

public class GameScene_script : MonoBehaviour {

	string curLevel;
	string curStage;

	//Get current working directory + Text folder
	string path = Directory.GetCurrentDirectory () + "\\Text";

	// Use this for initialization
	void Start () {
		 
		curLevel = File.ReadAllText (path + "\\lvl.sav");
		curStage = File.ReadAllText (path + "\\stg.sav");

		print("Level " + curLevel + " - Stage" + curStage);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){


	}

}
