using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitAndRun : MonoBehaviour {
	public UnityEvent OnRun;

	public void Run(float time){
		StartCoroutine (RunC(time));
	}

	IEnumerator RunC(float time){
		yield return new WaitForSeconds (time);
		OnRun.Invoke ();
	}
}
