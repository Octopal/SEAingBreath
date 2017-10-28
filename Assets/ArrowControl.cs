using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArrowControl : MonoBehaviour {
	Vector3 angle;
	public Transform target;
	public float fadeSpeed;
	public float valueMax;
	public float force;
	bool isSolved;
	float counter;

	public UnityEvent onArrowReachEvent;
	// Use this for initialization
	void Start () {
		angle.Set (0, 0, -0.2f);
	}

	public void AddForce(){
		counter += force;
	}
	// Update is called once per frame
	void Update () {
		if (!isSolved) {
			counter -= fadeSpeed * Time.deltaTime;
			counter = Mathf.Clamp (counter, valueMax, 0);
			angle.Set (0, 0, counter);
			transform.Rotate (angle);
		}
		float ang = Quaternion.Angle(transform.rotation, target.rotation);

		if (ang < 2 & !isSolved) {
			print ("arrow isSolved");
			onArrowReachEvent.Invoke ();
			isSolved = true;
		}
	}
}
