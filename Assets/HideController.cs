using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideController : MonoBehaviour {
	Quaternion q = Quaternion.identity;
	public GameObject laser;
	LineRenderer line;
	public GameObject camera;
	public float angleMax = 50;
	// Use this for initialization
	void Start () {
		line = laser.GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		float dot = Quaternion.Dot(laser.transform.rotation, camera.transform.rotation);

		if (dot < 0.6) {
			line.enabled = false;
		} else {
			line.enabled = true;
		}

	}
}
