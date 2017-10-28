using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabControl : MonoBehaviour {
	public FbxControl control;

	// Use this for initialization
	void Start () {
		
		
		
		control.SetScene ("Shell");
//		control.Play ("Sea_Idle");

	}
	
	// Update is called once per frame
	void Update () {
		
		
	}

	public void PlayTalkingAnimation(){
		
		control.SetScene ("ShellWithCrab");
		control.Play ("Shell_GoOut");
	

	}

}
