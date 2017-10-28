using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class breathStateDebug : MonoBehaviour {
	public Text textBreathState;
	public Text textBreathValue;
	// Use this for initialization
	void Start () {

	}
	public void onBreathStateChange(BreathState state){
		textBreathState.text = "BreathState: " + state.ToString();
	}
	public void onBreathValueChange(float value){
		textBreathValue.text = "BreathValue: " + value.ToString();
	}
}
