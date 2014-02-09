using UnityEngine;
using System.Collections;

public class menuScreenScrollScript : MonoBehaviour {

	public float speedx = 0;
	public float speedy = 0;
	const float smoothDrag = 0.01f;
	
	private Vector2 camPos = new Vector2(0, 0);
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		renderer.material.mainTextureOffset = new Vector2 (Time.time*speedx, Time.time*speedy);
	}
}
