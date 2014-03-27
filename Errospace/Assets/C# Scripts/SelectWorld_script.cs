using UnityEngine;
using System.Collections;
using System.IO;

public class SelectWorld_script : MonoBehaviour {

	//string curLevel;
	int numStages;
	
	//Get current working directory + Text folder
	string path = Directory.GetCurrentDirectory () + "\\Text";

	public Texture2D iconWorldA;
	public Texture2D iconWorldB;
	public Texture2D iconWorldC;
	public Texture2D iconWorldD;
	public Texture2D iconLockedLevel;
	
	GUIContent buttonWorldA = new GUIContent();
	GUIContent buttonWorldB = new GUIContent();
	GUIContent buttonWorldC = new GUIContent();
	GUIContent buttonWorldD = new GUIContent();
	GUIContent buttonLockedLevel = new GUIContent();
	
	public Font gameFont;
	GUIStyle textStyle = new GUIStyle();

	private int stageCountWorldA = 4;
	private int stageCountWorldB = 2;
	private int stageCountWorldC = 2;
	private int stageCountWorldD = 1;
	
	// Use this for initialization
	void Start () {
		buttonWorldA.image = iconWorldA;
		buttonWorldB.image = iconWorldB;
		buttonWorldC.image = iconWorldC;
		buttonWorldD.image = iconWorldD;
		buttonLockedLevel.image = iconLockedLevel;
		
		textStyle.font = gameFont;
		textStyle.normal.textColor = Color.white;

		//lvl.sav contains the curLevel selectedby user in the previous script (SelectLevel_script)
		//curLevel = File.ReadAllText (path + "\\lvl.sav");
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI(){

		GUIStyle guiStyle = new GUIStyle();
		guiStyle.padding = new RectOffset(0,0,0,0);

		int[] scores;

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
		}
		else{
			scores = new int[1];
			scores[0] = 0;
		}

		Rect coordWorldA = new Rect (((Screen.width/2)-(Screen.width*33/96)),((Screen.height/2)-(Screen.height*3/8)),Screen.width*2/7,Screen.height*2/7);
		Rect coordWorldB = new Rect (((Screen.width/2)-(Screen.width*8/96)),((Screen.height/2)-(Screen.height*3/8)),Screen.width*2/7,Screen.height*2/7);
		Rect coordWorldC = new Rect (((Screen.width/2)-(Screen.width*-17/96)),((Screen.height/2)-(Screen.height*3/8)),Screen.width*2/7,Screen.height*2/7);
		Rect coordWorldD = new Rect (((Screen.width/2)-(Screen.width*21/96)),((Screen.height/2)-(Screen.height*-1/40)),Screen.width*2/7,Screen.height*2/7);

		bool willShowScoreB = false;
		bool willShowScoreC = false;
		bool willShowScoreD = false;

		if(GUI.Button (coordWorldA, buttonWorldA, guiStyle)){
			Application.LoadLevel("WorldA");
		}

		if(scores.Length >= stageCountWorldA){
			willShowScoreB = true;
			if(GUI.Button (coordWorldB, buttonWorldB, guiStyle)){
				Application.LoadLevel("WorldB");
			}

			if(scores.Length >= stageCountWorldA+stageCountWorldB){
				willShowScoreC = true;
				if(GUI.Button (coordWorldC, buttonWorldC, guiStyle)){
					Application.LoadLevel("WorldC");
				}

				if(scores.Length >= stageCountWorldA+stageCountWorldB+stageCountWorldC){
					willShowScoreD = true;
					if(GUI.Button (coordWorldD, buttonWorldD, guiStyle)){
						Application.LoadLevel("WorldD");
					}
				}
				else{
					GUI.Label (coordWorldD, buttonLockedLevel, guiStyle);
				}
			}
			else{
				GUI.Label (coordWorldC, buttonLockedLevel, guiStyle);
				GUI.Label (coordWorldD, buttonLockedLevel, guiStyle);
			}
		}
		else{
			GUI.Label (coordWorldB, buttonLockedLevel, guiStyle);
			GUI.Label (coordWorldC, buttonLockedLevel, guiStyle);
			GUI.Label (coordWorldD, buttonLockedLevel, guiStyle);
		}

		if(GUI.Button (new Rect (((Screen.width/2)-(Screen.width*-4/96)),((Screen.height/2)-(Screen.height*-1/40)),Screen.width*2/7,Screen.height*2/7), buttonLockedLevel, guiStyle)){
			
		}


		int totalScore = 0;
		for(int i = 0; i<stageCountWorldA; i++){
			if(i<scores.Length)
				totalScore+=scores[i];
			else {
				break;
			}
//				print (scores[i]);			
		}
		System.String totalScoreText = "";
		if(totalScore < 10){
			totalScoreText = "0"+totalScore.ToString();
		}
		else
			totalScoreText = totalScore.ToString();


		textStyle.fontSize = 20*Screen.width/700;
		GUI.Label(new Rect (((Screen.width/2)-(Screen.width*31/96)),((Screen.height/2)-(Screen.height*5/31)),Screen.width*1/7,Screen.height*1/7), totalScoreText+"/"+stageCountWorldA*3, textStyle);

		//Scores for B, C, and D
		if(willShowScoreB){
			totalScore = 0;
			for(int i = 0; i<stageCountWorldB; i++){
				if(stageCountWorldA+i < scores.Length)
					totalScore+=scores[stageCountWorldA+i];
				else
					break;
			}

			totalScoreText = "";
			if(totalScore < 10){
				totalScoreText = "0"+totalScore.ToString();
			}
			else
				totalScoreText = totalScore.ToString();

			System.String stageStarCount = "";
			if(stageCountWorldB*3 < 10){
				stageStarCount = "0"+(stageCountWorldB*3).ToString();
			}
			else
				stageStarCount = (stageCountWorldB*3).ToString();

			GUI.Label(new Rect (((Screen.width/2)-(Screen.width*6/96)),((Screen.height/2)-(Screen.height*5/31)),Screen.width*1/7,Screen.height*1/7), totalScoreText+"/"+stageStarCount, textStyle);
		} 
		if(willShowScoreC){
			totalScore = 0;
			for(int i = 0; i<stageCountWorldC; i++){
				if(stageCountWorldA+stageCountWorldB+i < scores.Length)
					totalScore+=scores[stageCountWorldA+stageCountWorldB+i];
				else
					break;
			}

			totalScoreText = "";
			if(totalScore < 10){
				totalScoreText = "0"+totalScore.ToString();
			}
			else
				totalScoreText = totalScore.ToString();
			
			System.String stageStarCount = "";
			if(stageCountWorldC*3 < 10){
				stageStarCount = "0"+(stageCountWorldC*3).ToString();
			}
			else
				stageStarCount = (stageCountWorldC*3).ToString();

			GUI.Label(new Rect (((Screen.width/2)-(Screen.width*-19/96)),((Screen.height/2)-(Screen.height*5/31)),Screen.width*1/7,Screen.height*1/7), totalScoreText+"/"+stageStarCount, textStyle);
		}
		if(willShowScoreD){
			totalScore = 0;
			for(int i = 0; i<stageCountWorldD; i++){
				if(stageCountWorldA+stageCountWorldB+stageCountWorldC+i < scores.Length)
					totalScore+=scores[stageCountWorldA+stageCountWorldB+stageCountWorldC+i];
				else
					break;
			}

			totalScoreText = "";
			if(totalScore < 10){
				totalScoreText = "0"+totalScore.ToString();
			}
			else
				totalScoreText = totalScore.ToString();
			
			System.String stageStarCount = "";
			if(stageCountWorldD*3 < 10){
				stageStarCount = "0"+(stageCountWorldD*3).ToString();
			}
			else
				stageStarCount = (stageCountWorldD*3).ToString();

			GUI.Label(new Rect (((Screen.width/2)-(Screen.width*19/96)),((Screen.height/2)-(Screen.height*-19/80)),Screen.width*1/7,Screen.height*1/7), totalScoreText+"/"+stageStarCount, textStyle);
		}
	}
}