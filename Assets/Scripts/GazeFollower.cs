using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeFollower : MonoBehaviour {
	public GameObject cam;
	public float speed = 5;
	public float angleMax = 12;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = cam.transform.position;
		Quaternion camRot = cam.transform.rotation;
		float angle = Quaternion.Angle (camRot, transform.rotation);
		if (angle > angleMax) {
			float accel = angle - angleMax + 1;
			float step = (speed*accel) * Time.deltaTime;
			transform.rotation = Quaternion.RotateTowards (transform.rotation, camRot, step );
		}
	}
	public void Center(){
		transform.rotation = cam.transform.rotation;
	}
	IEnumerator Rotate(Quaternion curRotation, Quaternion targetRotation, float time){
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time){
			transform.rotation = Quaternion.Lerp (curRotation, targetRotation, t);
			yield return null;
		}
	}
}
