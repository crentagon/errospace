using UnityEngine;
using System.Collections;

public class planetMover : MonoBehaviour {
	
	public Vector3 jumpVelocity;
	public Transform BlackBall;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Vector2 position = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
		//BlackBall.localPosition = position;
		if (Input.GetMouseButtonDown (0)) {
			float mousex, mousey;
			Vector2 mousepos;
			mousex = Input.mousePosition.x;
			mousey = Input.mousePosition.y;
			mousepos = Camera.main.ScreenToWorldPoint(new Vector2 (mousex,mousey));
			print(mousepos);
			BlackBall.localPosition = mousepos;
		}

		//if (Input.GetButtonDown ("Jump"))
			//rigidbody2D.AddForce(jumpVelocity);
	}
}
