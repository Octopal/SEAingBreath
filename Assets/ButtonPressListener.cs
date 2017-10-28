using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonPressListener : MonoBehaviour {
	bool isListen = false;

	public UnityEvent onButtonPress;

	void Start () {
		
	}

	public void EnableListener(){
		isListen = true;
	}

	void Update () {
		if (isListen && GvrController.ClickButtonDown) {
			onButtonPress.Invoke ();
			isListen = false;
		}
	}
}
