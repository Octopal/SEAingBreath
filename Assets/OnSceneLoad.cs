using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnSceneLoad : MonoBehaviour {
	public UnityEvent onSceneLoadEvent;

	void Start () {
		onSceneLoadEvent.Invoke ();
	}
}
