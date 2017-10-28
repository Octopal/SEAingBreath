using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeLightControl : MonoBehaviour {
	Material material;
	public float speed = 2;
	float counter;
	// Use this for initialization
	void Start () {
		material = GetComponent<Renderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {
		counter += speed*Time.deltaTime;
		material.SetFloat ("_Counter", counter);
	}
}
