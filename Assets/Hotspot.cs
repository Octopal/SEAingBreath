using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hotspot : MonoBehaviour {
	public UnityEvent onCollide;

	// Use this for initialization
	void Start () {
		
	}

	public void OnCollide(){
		onCollide.Invoke ();	
	}

	public void OnCollisionEnter(Collision other){
		print ("trigger");
		OnCollide ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	// to do: collider event

}
