using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour {
	public float minDelaySec = 1.0f;
	public float maxDelaySec = 2.0f;

	public AudioClip[] clips;
	AudioSource audioSource;

	bool isRandomSeqPlay = false;

	int prevIndex = 0;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}

	public void PlaySequence(){
		isRandomSeqPlay = true;
		StartCoroutine (InvokePlayRandom ());
	}

	public void StopSequence(){
		isRandomSeqPlay = false;
		StopAllCoroutines ();
	}

	AudioClip GetRandomClip(){
		int index = Random.Range (0, clips.Length);
		while (prevIndex == index) {
			index = Random.Range (0, clips.Length);
		}
		prevIndex = index;
		return clips [index];
	}

	IEnumerator InvokePlayRandom(){
		while (isRandomSeqPlay) {
			AudioClip clip = GetRandomClip ();
			audioSource.clip = clip;
			audioSource.Play ();
			yield return new WaitForSeconds (Random.Range (clip.length+minDelaySec, clip.length+maxDelaySec));			
		}
	}

	public void Play(){
		audioSource.clip = GetRandomClip ();
		audioSource.Play ();
	}
}
