using UnityEngine;
using System.Collections;

public class MainMenu_script : MonoBehaviour {

	public Texture2D titleBanner;
	public Texture2D buttonExit;
	public Texture2D buttonPlay;

	private GUIContent title = new GUIContent();
	private GUIContent play = new GUIContent();
	private GUIContent exit = new GUIContent();

	private float width;
	private float height;

	private Rect labelRect;
	private Rect playRect;
	private Rect exitRect;
		 
	public AudioClip clipClick;
	//private AudioSource sourceClick;

	private bool willWait = true;

	// Use this for initialization
	void Start () {
		//sourceClick.clip = clipClick;
		title.image = titleBanner;
		play.image = buttonPlay;
		exit.image = buttonExit;

		width = Screen.width;
		height = Screen.height;

		labelRect = new Rect(
			(width - titleBanner.width)/2,
			(height - titleBanner.height)/2 - 100,
			titleBanner.width,
			titleBanner.height);
		playRect = new Rect(
			(width - buttonPlay.width)/2,
			(height - buttonPlay.height)/2,
			buttonPlay.width,
			buttonPlay.height);
		exitRect = new Rect(
			(width - buttonExit.width)/2,
			(height - buttonExit.height)/2 + 125,
			buttonExit.width,
			buttonExit.height);
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
		GUIStyle noStyle = new GUIStyle();

		float rx = Screen.width / width;
		float ry = Screen.height / height;

		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(ry, ry, 1));
		float adjustedWidth = width * (rx/ry);

		GUI.Label(labelRect, title);
		if(GUI.Button(playRect, play, noStyle))
			StartCoroutine(waitSelectWorld());
		if(GUI.Button(exitRect, exit, noStyle))
			Application.Quit();
	}
}
