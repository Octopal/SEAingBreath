using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SliderControl : MonoBehaviour {
	Slider slider;
	BreathState breathState = BreathState.BreathOut ;
	public float speed = 0.001f;
	bool isSolved = false;
	public UnityEvent onSolveEvent;
	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider> ();
	}
	public void SetState(BreathState state){
		breathState = state;
	}
	// Update is called once per frame
	void Update () {
		if (!isSolved) {
			if (breathState == BreathState.BreathIn || breathState == BreathState.BreathInHold) {
				slider.value -= speed;
				if (slider.value <= 0) {
					print ("Solved!");
					onSolveEvent.Invoke ();
					isSolved = true;
				}
			}
		}
	}
}
