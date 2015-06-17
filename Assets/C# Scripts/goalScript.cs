using UnityEngine;
using System.Collections;
using System.IO;

public class goalScript : MonoBehaviour {
		
	Collider2D firstCollider;
	public bool showNextLevel = false;
	public ParticleSystem goalEffect;

	public Transform stars;

	public Texture2D finishWindow;
	public Texture2D starIconFull;
	public Texture2D starIconEmpty;
	
	public Texture2D iconNextStage;
	public Texture2D iconRedoStage;
	public Texture2D iconViewStages;

	public Transform[] planets;
	private static Vector3[] planetPositions;

	GUIContent buttonNextStage = new GUIContent();
	GUIContent buttonRedoStage = new GUIContent();
	GUIContent buttonViewStages = new GUIContent();

	GUIStyle textStyle = new GUIStyle();
	public Font gameFont;
	
	public AudioClip clipWin;
	public AudioClip clipClick;

	private bool willWait = true;
	private bool isSavingNotFinished = true;
	private bool willWaitWin = true;

	public int curLevel;

	// Use this for initialization
	void Start () {
		buttonNextStage.image = iconNextStage;
		buttonViewStages.image = iconViewStages;
		buttonRedoStage.image = iconRedoStage;

		textStyle.font = gameFont;
		textStyle.normal.textColor = Color.white;

		if(planetPositions != null){
			try{
				for(int i=0; i<planets.Length; i++){
					planets[i].transform.localPosition = planetPositions[i];
				}
			}
			catch(UnityException e){
				print (e);
			}
		}
		
		planetPositions = new Vector3[0];
	}

	void OnTriggerEnter2D(Collider2D collider){
		firstCollider = collider;
	}

	
	IEnumerator waitSelectScene(string selectedScene){
		while(willWait){
			audio.PlayOneShot(clipClick);
			yield return new WaitForSeconds(0.3f);
			willWait = false;
			Application.LoadLevel(selectedScene);
		}
	}

	
	IEnumerator waitReloadLevel(int offset){
		while(willWait){
			audio.PlayOneShot(clipClick);
			yield return new WaitForSeconds(0.3f);
			willWait = false;
			int levelToLoad = Application.loadedLevel+offset;
			Application.LoadLevel(levelToLoad);
		}
	}

	IEnumerator waitShowWin(){
		while(willWaitWin){
			goalEffect.Play();
			audio.PlayOneShot(clipWin);
			yield return new WaitForSeconds(2.5f);
			willWaitWin = false;
			showNextLevel = true;
		}
	}

	// Update is called once per frame
	void Update(){

		if(firstCollider != null && firstCollider.transform.name == "Rocket"){

			gameStart gsScript = firstCollider.transform.parent.GetComponent<gameStart>();
			gsScript.showSideButtons = false;

			//Rocket must stop at the goal position
			rocketMover rocketScript = firstCollider.GetComponent<rocketMover>();
			rocketScript.isActive = false;
			rocketScript.rigidbody2D.velocity = new Vector2 (0, 0);
			//rocketScript.transform.position = transform.position - new Vector3 (0,0,1);
			if(!showNextLevel){
				//goalEffect.Play();
				rocketScript.stopTrail();
			}

			if(isSavingNotFinished){
				StartCoroutine(waitShowWin());

				isSavingNotFinished = false;

				//Read from binary file
				int[] scores;
				
				var starCount = stars.childCount;
				starCount = 3-starCount;

				if(File.Exists ("errosave.bin")){
					using(BinaryReader b = new BinaryReader(File.Open("errosave.bin", FileMode.Open))){
						int pos = 0;
						int length = (int)b.BaseStream.Length;

						scores = new int[length/4];

						while(pos<length){
							int v = b.ReadInt32 ();
							
							//Grab all the binary file's contents and store them in an array
							scores[pos/4] = v;

							pos += sizeof(int);
						}
					}

					for(int i = 0; i<scores.Length; i++){
						print (scores[i]+" <3");
					}

					//Search for current level

					//If current level doesn't exist, make a new array with THIS level, 
					if(scores.Length == curLevel){
						int[] newScores = new int[scores.Length+1];
						for(int i=0; i<scores.Length; i++){
							newScores[i] = scores[i]; 
						}
						newScores[curLevel] = starCount;
						scores = newScores;
					}
					else{
						if(scores[curLevel] < starCount)
							scores[curLevel] = starCount;
					}

					//Store score
					using(BinaryWriter w = new BinaryWriter(File.Open ("errosave.bin", FileMode.Create))){
						for(int i=0; i<scores.Length; i++){
							w.Write(scores[i]);
						}
					}
				}
				else{
					using(BinaryWriter w = new BinaryWriter(File.Open ("errosave.bin", FileMode.Create))){
						w.Write (starCount);
					}
				}

			}
//			print("Game finished!");
		}
	}

	void OnGUI () {
		GUIStyle goGuiStyle = new GUIStyle();
		goGuiStyle.padding = new RectOffset(0,0,0,0);

		if (showNextLevel) {
			GUI.Label( new Rect (((Screen.width/2)-(Screen.width*25/96)),((Screen.height/2)-(Screen.height*3/8)),Screen.width*4/5,Screen.height*4/5), new GUIContent(finishWindow));

			Rect star01 = new Rect (((Screen.width/2)-(Screen.width*2/12)),((Screen.height/2)-(Screen.height*9/32)),Screen.width*1/6,Screen.height*1/6);
			Rect star02 = new Rect (((Screen.width/2)-(Screen.width*1/18)),((Screen.height/2)-(Screen.height*9/32)),Screen.width*1/6,Screen.height*1/6);
			Rect star03 = new Rect (((Screen.width/2)-(Screen.width*-1/18)),((Screen.height/2)-(Screen.height*9/32)),Screen.width*1/6,Screen.height*1/6);

			var starCount = stars.childCount;
			starCount = 3-starCount;

			if(starCount > 0){
				GUI.Label(star01, new GUIContent(starIconFull));
				starCount--;
				if(starCount > 0){
					GUI.Label(star02, new GUIContent(starIconFull));
					starCount--;
					if(starCount == 1){
						GUI.Label( star03, new GUIContent(starIconFull));
					}
					else{
						GUI.Label( star03, new GUIContent(starIconEmpty));
					}
				}
				else{
					GUI.Label( star02, new GUIContent(starIconEmpty));
					GUI.Label( star03, new GUIContent(starIconEmpty));
				}
			} else {
				GUI.Label( star01, new GUIContent(starIconEmpty));
				GUI.Label( star02, new GUIContent(starIconEmpty));
				GUI.Label( star03, new GUIContent(starIconEmpty));
			}

			//print (stars.childCount+"<<<");
		}

		//GUI.Button (new Rect ((Screen.width-buttonWidth-10),(Screen.height-buttonHeight-10),buttonWidth, buttonHeight), goContent, goGuiStyle);
		//GUI.Button(new Rect (((Screen.width/2)-(Screen.width*1/18)),((Screen.height/2)-(Screen.height*2/8)),Screen.width*1/6,Screen.height*1/6), "Next Level2", textStyle);
		//GUI.Button (new Rect (0f, 0f, 100f, 100f), "YEAH", goGuiStyle);
		//Display next level button
		if (showNextLevel && GUI.Button (new Rect (((Screen.width/2)-(Screen.width*9/72)),((Screen.height/2)-(Screen.height*1/13)),Screen.width*3/8,Screen.height*3/25), buttonNextStage, goGuiStyle)) {
			StartCoroutine(waitReloadLevel(1));
		}

		if (showNextLevel && GUI.Button (new Rect (((Screen.width/2)-(Screen.width*9/72)),((Screen.height/2)-(Screen.height*-1/18)),Screen.width*3/8,Screen.height*3/25), buttonRedoStage, goGuiStyle)) {
			planetPositions = new Vector3[planets.Length];
			for(int j=0; j<planets.Length; j++){
				planetPositions[j] = planets[j].transform.localPosition;
			}

			StartCoroutine(waitReloadLevel(0));
		}
		
		
		if (showNextLevel && GUI.Button (new Rect (((Screen.width/2)-(Screen.width*9/72)),((Screen.height/2)-(Screen.height*-27/144)),Screen.width*3/8,Screen.height*3/25), buttonViewStages, goGuiStyle)) {
			StartCoroutine(waitSelectScene("SelectWorld"));
		}
	}
}
