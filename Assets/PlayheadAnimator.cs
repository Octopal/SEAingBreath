using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayheadAnimator : MonoBehaviour {
	public string clipName;
	public float normalizedTime = 0;
	public Animator animator;
	public bool isEnabled = false;
	public bool isForceBased = true;
	public float playbackSpeed = 0.1f;
	public int direction = -1;

	bool isPlayForward = false;

	public void SetValue(float value){
		normalizedTime = value;
	}
	public void SetEnabled(bool value){
		isEnabled = value;
	}
	public void PlayForward(){
		direction = 1;
	}
	public void PlayBackward(){
		direction = -1;
	}
	// Update is called once per frame
	void Update () {
		if (isEnabled && isForceBased ) {
			if (Input.GetKeyUp (KeyCode.A)) {
				PlayForward ();
			}
			if (Input.GetKeyUp (KeyCode.S)) {
				PlayBackward ();
			}
			normalizedTime += playbackSpeed * Time.deltaTime * direction;
			normalizedTime = Mathf.Clamp (normalizedTime, 0, 1);
		}
		if (isEnabled) {
			float time = Mathf.Clamp (normalizedTime, 0, 1);
			animator.Play (clipName, 0, time);
		}
	}
}
