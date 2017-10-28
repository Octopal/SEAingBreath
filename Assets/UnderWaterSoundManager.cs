using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnderWaterSoundManager : MonoBehaviour {
	public AudioClip[] instructionsClips;//random help screaming
	public AudioClip[] randomCrabVoiceClips;//sequenced welcomes
	private AudioSource audioSour;
	bool isTimeForInstructions = true;
	int LastRandomCrabVoiceIndex = 0;

	private float minVal;

	public AudioSource bgMusic;
	public UnityEvent onInstructionEnd;


	// Use this for initialization
	void Awake () {
		audioSour = gameObject.GetComponent<AudioSource> ();
		audioSour.loop = false;
	}
	public void PlayRandomCrabComments(){
		isTimeForInstructions = true ;
		StartCoroutine (InvokePlayRandomInstructions());
		// start playing random help screaming
	}
	public void StopPlayingRandomCrabComments(){
		isTimeForInstructions = false;
	}
	public void PlayInstructions(){
		StartCoroutine (PlayWelcomeSequenceWithPauses ());
	}

	private AudioClip GetRandomCrabVoiceClip(){
		int index = Random.Range (0, randomCrabVoiceClips.Length);
		while (index == LastRandomCrabVoiceIndex) {
			index = Random.Range (0, randomCrabVoiceClips.Length);
		}
		LastRandomCrabVoiceIndex = index;
		return randomCrabVoiceClips [LastRandomCrabVoiceIndex];
	}
	private AudioClip getCrabVoiceSeq(){
		return instructionsClips [0];
	}
	public void StopAll(){
		StopAllCoroutines ();
	}
	IEnumerator PlayWelcomeSequenceWithPauses(){
		for(int i = 0; i < instructionsClips.Length; i++){
			audioSour.clip = instructionsClips[i];
			audioSour.Play ();
			yield return new WaitForSeconds (instructionsClips[i].length);
		}
		onInstructionEnd.Invoke ();
	}
	IEnumerator InvokePlayRandomInstructions(){
		while (isTimeForInstructions) {
			print ("invoke");
			AudioClip clip = GetRandomCrabVoiceClip ();
			audioSour.clip = clip;
			audioSour.Play ();
			yield return new WaitForSeconds (Random.Range (clip.length+1, clip.length+2));			
		}
	}


	IEnumerator FadeVolume(AudioSource audio, float maxValue, float fadeTime)
	{
		// run once when calling start couroutine
		float curVolume = audio.volume;

		// updates every frame untill it reaches the exact fadeTime
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeTime)
		{
			audio.volume = Mathf.Lerp (curVolume, maxValue, t);
			yield return null;
		}
	}
}
