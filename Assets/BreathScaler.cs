using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathScaler : MonoBehaviour {
	public float scaleMin = 0.5f;
	public float scaleMax = 4;
	// Use this for initialization
	void Start () {
		
	}
	public void SetScale(float scale){
		float s = scale.Remap (0, 1, scaleMin, scaleMax);
		transform.localScale = new Vector3 (s, s, s);
	}
}
