using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SwitchVideos : MonoBehaviour {

	private VideoClip vClip;
	private VideoPlayer vPlayer;
	public VideoClip[] allClips;
	public AudioSource allAudiosources;
 	private int counter = 1;
	private bool wasPlayed = false;

	// Use this for initialization
	void Start () {
		vPlayer = gameObject.GetComponent<VideoPlayer> ();



	}
	
	// Update is called once per frame


	public void PlayClip(){
		wasPlayed = true;
		StartCoroutine (AssignClip ());
	

		
		
}

	public void PlayNextClip(){
		
		if (counter < allClips.Length ) {
			vPlayer.clip = allClips [counter];
		} else if (counter == allClips.Length ) {
			counter = 0;
			vPlayer.clip = allClips [counter];
		}
		vPlayer.Play ();
		Debug.Log (counter);
		counter++;
		counter = Mathf.Clamp (counter, 0, allClips.Length);
		
	}

	public void PlayPreviousClip(){
		counter--;
		counter = Mathf.Clamp (counter, 0, allClips.Length);
		if (counter >= 0) {
			vPlayer.clip = allClips [counter];
		} else if (counter == 0) {
			counter = allClips.Length-1;
			vPlayer.clip = allClips [counter];
		} 
		vPlayer.Play ();
		Debug.Log (counter);
	}

	public void StopClip(){
		Debug.Log ("button presses");
		vPlayer.Stop ();
	}

	IEnumerator AssignClip(){
		for(int i = 0; i < allClips.Length; i++){
			vPlayer.clip = allClips[i];
			vPlayer.Play ();
			yield return new WaitForSeconds ((float)allClips[i].length);
		}
	}



	public void PlayFirst(){
		Debug.Log ("button presses");
		vPlayer.clip = allClips[0];

		vPlayer.Play ();
	}
}



