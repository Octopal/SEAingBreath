using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBubblePlay : MonoBehaviour {
	private AudioSource auds;
	private float timeMin = 5f;
	private float timeMax=12f;
	private float ranTime = 1f;
	private bool isPlaying = true;

	

	// Use this for initialization
	void Start () {
		auds = gameObject.GetComponent<AudioSource>() ;
		StartCoroutine (WaitForRanSecondsAndPlay());
	}
	
	// Update is called once per frame
	void Update () {
		
	

		
	}

	IEnumerator WaitForRanSecondsAndPlay(){
		while (isPlaying) {
			ranTime = Random.Range (timeMin, timeMax);
			yield return new WaitForSecondsRealtime (ranTime);
			auds.Play ();
			Debug.Log ("is playing");

		}

	}
}
