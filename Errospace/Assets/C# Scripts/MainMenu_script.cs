using UnityEngine;
using System.Collections;

public class MainMenu_script : MonoBehaviour {

	public Texture2D titleBanner;
	public Texture2D buttonExit;
	public Texture2D buttonPlay;
	
	GUIContent title = new GUIContent();
	GUIContent play = new GUIContent();
	GUIContent exit = new GUIContent();

	private float width;
	private float height;
		 
	// Use this for initialization
	void Start () {
		title.image = titleBanner;
		play.image = buttonPlay;
		exit.image = buttonExit;

		width = 1280;
		height = 800;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		GUIStyle noStyle = new GUIStyle();

		float rx = Screen.width / width;
		float ry = Screen.height / height;

		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(ry, ry, 1));
		float adjustedWidth = width * (rx/ry);

		Rect labelRect = new Rect(
			(width - titleBanner.width)/2,
			(height - titleBanner.height)/2 - 100,
			titleBanner.width,
			titleBanner.height);
		Rect playRect = new Rect(
			(width - buttonPlay.width)/2,
			(height - buttonPlay.height)/2,
			buttonPlay.width,
			buttonPlay.height);
		Rect exitRect = new Rect(
			(width - buttonExit.width)/2,
			(height - buttonExit.height)/2 + 125,
			buttonExit.width,
			buttonExit.height);

		GUI.Label(labelRect, title);
		if(GUI.Button(playRect, play, noStyle))
			Application.LoadLevel("SelectWorld");
		if(GUI.Button(exitRect, exit, noStyle))
			Application.Quit();
	}
}
