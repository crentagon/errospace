using UnityEngine;
using System.Collections;

public class scrollScript : MonoBehaviour {

	public float speed = 0.2f;
	const float smoothDrag = 0.01f;

	private Vector2 camPos = new Vector2(0, 0);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
//		renderer.material.mainTextureOffset = new Vector2 (Time.time*speed, 0f);



		if (Input.GetMouseButton (1)) {

			float MouseX;
			float MouseY;

			MouseX = Input.GetAxis ("Mouse X");
			MouseY = Input.GetAxis ("Mouse Y");

			camPos += new Vector2 (-MouseX, -MouseY);
			renderer.material.mainTextureOffset = camPos*smoothDrag;
			//print (camPos);

//			print (Time.time);
//			renderer.material.mainTextureOffset = new Vector2 (Time.time*speed, 0f);
			//renderer.material.mainTextureOffset = renderer.material.GetTextureOffset("_MainTex") + CameraPos;
//			//print (renderer.material.GetTextureOffset("_MainTex"));
//			//this.transform.position += CameraPos * smoothDrag;
		}
//		//renderer.material.mainTextureOffset = new Vector2 (Time.time*speed, 0f);
	}
}
