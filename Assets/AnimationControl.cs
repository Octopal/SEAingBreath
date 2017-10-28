using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour {
	public Animator control;
	// Use this for initialization
	void Start () {
		control = GetComponent<Animator> ();
		AnimatorStateInfo state = control.GetCurrentAnimatorStateInfo(0);
	}
	
	// Update is called once per frame
	void Update () {
		float value = map(Input.mousePosition.x, 0, 1000, 0f, 1f);
		float time = Mathf.Clamp (value, 0, 1);
		control.Play ("C4D Animation Take", 0, time);
	}

	float map(float s, float a1, float a2, float b1, float b2)
	{
		return b1 + (s-a1)*(b2-b1)/(a2-a1);
	}
}
