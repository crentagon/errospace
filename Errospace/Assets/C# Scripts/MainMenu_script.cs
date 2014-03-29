using UnityEngine;
using System.Collections;

public class MainMenu_script : MonoBehaviour {

	public Texture2D titleBanner;
	public Texture2D buttonExit;
	public Texture2D buttonPlay;

	public AudioClip clipClick;
	//private AudioSource sourceClick;

	GUIContent title = new GUIContent();
	GUIContent play = new GUIContent();
	GUIContent exit = new GUIContent();

	private bool willWait = true;

	// Use this for initialization
	void Start () {
		//sourceClick.clip = clipClick;
		title.image = titleBanner;
		play.image = buttonPlay;
		exit.image = buttonExit;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator waitSelectWorld(){
		while(willWait){
			audio.PlayOneShot(clipClick);
			yield return new WaitForSeconds(0.3f);
			willWait = false;
			Application.LoadLevel("SelectWorld");
		}
	}

	
	void OnGUI () {
		GUIStyle guiStyle = new GUIStyle();
		guiStyle.padding = new RectOffset(0,0,0,0);

		GUI.Label (new Rect (((Screen.width / 2) - (Screen.width * 17 / 64)), ((Screen.height / 2) - (Screen.height * 5 / 13)), Screen.width * 15/16, Screen.height * 3/8), title, guiStyle);

		if(GUI.Button(new Rect (((Screen.width / 2) - (Screen.width * 1 / 8)), ((Screen.height / 2) - (Screen.height * 1 / 16)), Screen.width * 1/2, Screen.height * 1/6), play, guiStyle)){
			StartCoroutine(waitSelectWorld());

		}

		if(GUI.Button(new Rect (((Screen.width / 2) - (Screen.width * 1 / 14)), ((Screen.height / 2) - (Screen.height * -2 / 16)), Screen.width * 1/3, Screen.height * 1/9), exit, guiStyle)){
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
