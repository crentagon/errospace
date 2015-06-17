using UnityEngine;
using System.Collections;

public class trailMaker : MonoBehaviour {

	// Use this for initialization
	public static trailMaker Instance;
	public ParticleSystem trailEffect;
	void Awake(){
		if(Instance != null){
			print("shizz");
			return;
		}
		Instance = this;
	}
	public void makeTrail(Vector3 position){
		instantiate(trailEffect, position);
	}

	private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position){
		ParticleSystem newParticleSystem = Instantiate(
			prefab,
			position,
			Quaternion.identity
			) as ParticleSystem;

		Destroy(
		newParticleSystem.gameObject,
		newParticleSystem.startLifetime
		);
		return newParticleSystem;
	}
	// Update is called once per frame

}
