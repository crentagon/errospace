using UnityEngine;
using System.Collections;

public class planetMover : MonoBehaviour {

	public Transform BlackBall;
	public bool isMovable = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Vector3 offset = Rocket.position;
		//rigidbody2D.velocity = rigidbody2D.velocity - (Vector2)offset;
	}

	void OnMouseDrag(){
		if(isMovable){
			float mousex, mousey;
			Vector2 mousepos;
			mousex = Input.mousePosition.x;
			mousey = Input.mousePosition.y;
			mousepos = Camera.main.ScreenToWorldPoint(new Vector2 (mousex,mousey));
			print(mousepos);
			BlackBall.localPosition = mousepos;
		}
	}

}
