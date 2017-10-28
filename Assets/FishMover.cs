using UnityEngine;
using System.Collections;

public class FishMover : MonoBehaviour {

	Vector3 startPosition; 

	int seed;
	int seedX;
	int seedY;
	public float minAngle;
	public float maxAngle;
	public float speedRotation;
	float interval;

	public float distanceMax;
	public float distanceMin;
	public float speedTranslation;

	void Awake(){
		speedRotation = 0.4f; // sets default values for rotation
		speedTranslation = 0.4f;// sets default values for translation
	}



	void Start(){
		seed = Random.Range (-1000, 1000);
		seedX = Random.Range (-800, 800);
		seedY =  Random.Range (-500, 500);
		startPosition = transform.localPosition; // grabs starting position of the object


	}

	void Update() {

		interval = maxAngle - minAngle;
		float randomRotation = interval*noise(speedRotation,seed)+minAngle; //rotation angle range
		transform.localEulerAngles = new Vector3(0.0f,0.0f,randomRotation);

		float randX = distanceMax*noise(speedTranslation,seedX)+distanceMin;
		float randY = distanceMax*noise(speedTranslation,seedY)+distanceMin;
		transform.localPosition = new Vector3(startPosition.x+randX,startPosition.y+randY,0.0f);
	}
	float noise (float _speed,float _seed){
		return Mathf.PerlinNoise ((Time.time + _seed) * _speed, (Time.time + _seed + 100) * _speed); // generates random values based on PerlinNoise algorithm
	}
}
