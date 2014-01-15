using UnityEngine;
using System.Collections;

public class allLevels : MonoBehaviour {

	public Transform rocket;

	// Use this for initialization
	void Start () {
	
	}

	//If rocket exits collider, game over.
	void OnTriggerExit2D(Collider2D coll) {
		if (coll.transform.name == "Rocket")
			print ("Game over.");
		//print ("No longer in contact with "+coll.transform.name);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
