using UnityEngine;
using System.Collections;
using System.IO;

public class SelectLevel_script : MonoBehaviour {
	
	//string curLevel;
	public string world;

	public Texture2D iconLevel01;
	public Texture2D iconLevel02;
	public Texture2D iconLevel03;
	public Texture2D iconLevel04;
	public Texture2D iconLockedLevel;

	public Texture2D iconStarsFull;
	public Texture2D iconStarsEmpty;

	public Texture2D iconBack;

	GUIContent buttonLevel01 = new GUIContent();
	GUIContent buttonLevel02 = new GUIContent();
	GUIContent buttonLevel03 = new GUIContent();
	GUIContent buttonLevel04 = new GUIContent();
	GUIContent buttonLockedLevel = new GUIContent();

	GUIContent buttonBack = new GUIContent ();

	private int stageCountWorldA = 4;
	private int stageCountWorldB = 2;
	private int stageCountWorldC = 2;
	private int stageCountWorldD = 1;
//	public Font gameFont;
//	GUIStyle textStyle = new GUIStyle();
	
	// Use this for initialization
	void Start () {
		buttonLevel01.image = iconLevel01;
		buttonLevel02.image = iconLevel02;
		buttonLevel03.image = iconLevel03;
		buttonLevel04.image = iconLevel04;
		buttonLockedLevel.image = iconLockedLevel;

		buttonBack.image = iconBack;

//		textStyle.font = gameFont;
//		textStyle.normal.textColor = Color.white;
//		textStyle.fontSize = 20;
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
			scores = new int[0];
		}

		int adder = 0;
		int numStages = stageCountWorldA;

		if(world == "B"){
			adder = stageCountWorldA;	
			numStages = stageCountWorldB;
		}
		if(world == "C"){
			adder = stageCountWorldA+stageCountWorldB;
			numStages = stageCountWorldC;
		}
		if(world == "D"){
			adder = stageCountWorldA+stageCountWorldB+stageCountWorldC;
			numStages = stageCountWorldD;
		}

		int numCompleted = scores.Length - adder;
		if (numCompleted > numStages)
			numCompleted = numStages;
		
		int[] worldScores = new int[numCompleted];

		for(int i=0; i<numStages; i++){
			if(adder+i < scores.Length)
				worldScores[i] = scores[adder+i];
			else
				break;
		}

		Rect[] starCoords = new Rect[15];

		starCoords[0] = new Rect (((Screen.width/2)-(Screen.width*32/96)),((Screen.height/2)-(Screen.height*17/100)),Screen.width*1/20,Screen.height*1/20);
		starCoords[1] = new Rect (((Screen.width/2)-(Screen.width*29/96)),((Screen.height/2)-(Screen.height*17/100)),Screen.width*1/20,Screen.height*1/20);
		starCoords[2] = new Rect (((Screen.width/2)-(Screen.width*26/96)),((Screen.height/2)-(Screen.height*17/100)),Screen.width*1/20,Screen.height*1/20);
		starCoords[3] = new Rect (((Screen.width/2)-(Screen.width*7/96)),((Screen.height/2)-(Screen.height*17/100)),Screen.width*1/20,Screen.height*1/20);
		starCoords[4] = new Rect (((Screen.width/2)-(Screen.width*4/96)),((Screen.height/2)-(Screen.height*17/100)),Screen.width*1/20,Screen.height*1/20);
		starCoords[5] = new Rect (((Screen.width/2)-(Screen.width*1/96)),((Screen.height/2)-(Screen.height*17/100)),Screen.width*1/20,Screen.height*1/20);
		starCoords[6] = new Rect (((Screen.width/2)-(Screen.width*-18/96)),((Screen.height/2)-(Screen.height*17/100)),Screen.width*1/20,Screen.height*1/20);
		starCoords[7] = new Rect (((Screen.width/2)-(Screen.width*-21/96)),((Screen.height/2)-(Screen.height*17/100)),Screen.width*1/20,Screen.height*1/20);
		starCoords[8] = new Rect (((Screen.width/2)-(Screen.width*-24/96)),((Screen.height/2)-(Screen.height*17/100)),Screen.width*1/20,Screen.height*1/20);
		starCoords[9] = new Rect (((Screen.width/2)-(Screen.width*20/96)),((Screen.height/2)-(Screen.height*-23/100)),Screen.width*1/20,Screen.height*1/20);
		starCoords[10] = new Rect (((Screen.width/2)-(Screen.width*17/96)),((Screen.height/2)-(Screen.height*-23/100)),Screen.width*1/20,Screen.height*1/20);
		starCoords[11] = new Rect (((Screen.width/2)-(Screen.width*14/96)),((Screen.height/2)-(Screen.height*-23/100)),Screen.width*1/20,Screen.height*1/20);
		starCoords[12] = new Rect (((Screen.width/2)-(Screen.width*-5/96)),((Screen.height/2)-(Screen.height*-23/100)),Screen.width*1/20,Screen.height*1/20);
		starCoords[13] = new Rect (((Screen.width/2)-(Screen.width*-8/96)),((Screen.height/2)-(Screen.height*-23/100)),Screen.width*1/20,Screen.height*1/20);
		starCoords[14] = new Rect (((Screen.width/2)-(Screen.width*-11/96)),((Screen.height/2)-(Screen.height*-23/100)),Screen.width*1/20,Screen.height*1/20);

		Rect coordsLevel01 = new Rect (((Screen.width/2)-(Screen.width*33/96)),((Screen.height/2)-(Screen.height*3/8)),Screen.width*2/7,Screen.height*2/7);
		Rect coordsLevel02 = new Rect (((Screen.width/2)-(Screen.width*8/96)),((Screen.height/2)-(Screen.height*3/8)),Screen.width*2/7,Screen.height*2/7);
		Rect coordsLevel03 = new Rect (((Screen.width/2)-(Screen.width*-17/96)),((Screen.height/2)-(Screen.height*3/8)),Screen.width*2/7,Screen.height*2/7);
		Rect coordsLevel04 = new Rect (((Screen.width/2)-(Screen.width*21/96)),((Screen.height/2)-(Screen.height*-1/40)),Screen.width*2/7,Screen.height*2/7);
		Rect coordsLevel05 = new Rect (((Screen.width/2)-(Screen.width*-4/96)),((Screen.height/2)-(Screen.height*-1/40)),Screen.width*2/7,Screen.height*2/7);

		Rect coordButtonBack = new Rect (10, (Screen.height - Screen.height * 1 / 9 - 10), Screen.width * 1/3, Screen.height * 1/9);

		if (GUI.Button (coordButtonBack, buttonBack, guiStyle)) {
			Application.LoadLevel("SelectWorld");
		}

		if(GUI.Button (coordsLevel01, buttonLevel01, guiStyle)){
			Application.LoadLevel("Level"+world+"1");
		}

		if(worldScores.Length >= 1 && numStages > 1){
			if(GUI.Button (coordsLevel02, buttonLevel02, guiStyle)){
				Application.LoadLevel("Level"+world+"2");
			}

			if(worldScores.Length >= 2 && numStages > 2){
				if(GUI.Button (coordsLevel03, buttonLevel03, guiStyle)){
					Application.LoadLevel("Level"+world+"3");
				}

				if(worldScores.Length >= 3 && numStages > 3){
					if(GUI.Button (coordsLevel04, buttonLevel04, guiStyle)){
						Application.LoadLevel("Level"+world+"4");
					}

					if(worldScores.Length >= 4 && numStages > 4){
						if(GUI.Button (coordsLevel05, buttonLevel04, guiStyle)){ //TODO: buttonLevel05
							Application.LoadLevel("Level"+world+"5");
						}
					}
					else{
						GUI.Button (coordsLevel05, buttonLockedLevel, guiStyle);
					}
				}
				else{
					GUI.Button (coordsLevel04, buttonLockedLevel, guiStyle);
					GUI.Button (coordsLevel05, buttonLockedLevel, guiStyle);
				}
			}
			else{
				GUI.Button (coordsLevel03, buttonLockedLevel, guiStyle);
				GUI.Button (coordsLevel04, buttonLockedLevel, guiStyle);
				GUI.Button (coordsLevel05, buttonLockedLevel, guiStyle);
			}
		}
		else{
			GUI.Button (coordsLevel02, buttonLockedLevel, guiStyle);
			GUI.Button (coordsLevel03, buttonLockedLevel, guiStyle);
			GUI.Button (coordsLevel04, buttonLockedLevel, guiStyle);
			GUI.Button (coordsLevel05, buttonLockedLevel, guiStyle);
		}

		print ("WORLD SCORES LENGTH: "+worldScores.Length);

		for(int i=0; i<worldScores.Length; i++){
			if(worldScores.Length < i+1){
				GUI.Label (starCoords[3*i], iconStarsEmpty, guiStyle);
				GUI.Label (starCoords[3*i+1], iconStarsEmpty, guiStyle);
				GUI.Label (starCoords[3*i+2], iconStarsEmpty, guiStyle);
			}
			else{
				if(worldScores[i] == 0){
					GUI.Label (starCoords[3*i], iconStarsEmpty, guiStyle);
					GUI.Label (starCoords[3*i+1], iconStarsEmpty, guiStyle);
					GUI.Label (starCoords[3*i+2], iconStarsEmpty, guiStyle);
				}
				else if(worldScores[i] == 1){
					GUI.Label (starCoords[3*i], iconStarsFull, guiStyle);
					GUI.Label (starCoords[3*i+1], iconStarsEmpty, guiStyle);
					GUI.Label (starCoords[3*i+2], iconStarsEmpty, guiStyle);
				}
				else if(worldScores[i] == 2){
					GUI.Label (starCoords[3*i], iconStarsFull, guiStyle);
					GUI.Label (starCoords[3*i+1], iconStarsFull, guiStyle);
					GUI.Label (starCoords[3*i+2], iconStarsEmpty, guiStyle);
				}
				else if(worldScores[i] == 3){
					GUI.Label (starCoords[3*i], iconStarsFull, guiStyle);
					GUI.Label (starCoords[3*i+1], iconStarsFull, guiStyle);
					GUI.Label (starCoords[3*i+2], iconStarsFull, guiStyle);
				}
			}
		}




//		GUI.Label(new Rect (((Screen.width/2)-(Screen.width*31/96)),((Screen.height/2)-(Screen.height*5/31)),Screen.width*1/7,Screen.height*1/7), "00/12", textStyle);
//		GUI.Label(new Rect (((Screen.width/2)-(Screen.width*6/96)),((Screen.height/2)-(Screen.height*5/31)),Screen.width*1/7,Screen.height*1/7), "00/06", textStyle);
//		GUI.Label(new Rect (((Screen.width/2)-(Screen.width*-19/96)),((Screen.height/2)-(Screen.height*5/31)),Screen.width*1/7,Screen.height*1/7), "00/06", textStyle);
//		GUI.Label(new Rect (((Screen.width/2)-(Screen.width*19/96)),((Screen.height/2)-(Screen.height*-19/80)),Screen.width*1/7,Screen.height*1/7), "00/03", textStyle);
	}
}