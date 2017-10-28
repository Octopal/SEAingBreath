using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class VoiceSoundManager : MonoBehaviour {
	public AudioClip[] randomHelpMeCrabClips;//random help screaming
	public AudioClip[] crabWelcomeClips;//sequenced welcomes
	private AudioSource audioSour;
	private AudioReverbFilter filter;
	private AudioDistortionFilter distFilter;
	bool isTimeForRandomHelp = true;
	private float timeForOcean;
	private GameObject oceanBox;
	public Animator crabAnim;
	bool isTimeToRise = false;

	private float minVal;

	public AudioSource ocean;

	public UnityEvent onFinishTalkingEvent;
	// Use this for initialization

	void Start () {
		oceanBox = GameObject.Find ("OceanBox");
//		filter = gameObject.GetComponent<AudioReverbFilter> ();
		distFilter = ocean.GetComponent<AudioDistortionFilter> ();
		audioSour = gameObject.GetComponent<AudioSource> ();
		audioSour.loop = false;
		StartCoroutine (InvokePlayRandomHelp ());
		PlayRandomHelp ();
		
	}
	public void PlayRandomHelp(){
		isTimeForRandomHelp = true ;
		// start playing random help screaming
	}
	public void StopRandomHelp(){
		isTimeForRandomHelp = false;
//		filter.enabled = false;
	}
	public void PlayWelcomeSequence(){
	
		StartCoroutine (PlayWelcomeSequenceWithPauses ());
		
	}
	// Update is called once per frame
	void Update () {
//		DetermineLevelRiseAndIncreaseVolume ();
		
	}

	private AudioClip GetRandomHelpMeClip(){
		return randomHelpMeCrabClips [Random.Range (0, randomHelpMeCrabClips.Length)];
	}
	private AudioClip CrabTextWelcome(){
		return crabWelcomeClips [0];

	}
	IEnumerator FinishTalking(){
		yield return new WaitForSeconds (5);
		onFinishTalkingEvent.Invoke ();
	}
	IEnumerator PlayWelcomeSequenceWithPauses(){
		for(int i = 0; i < crabWelcomeClips.Length; i++){
			audioSour.clip=crabWelcomeClips[i];
			if (audioSour.clip == crabWelcomeClips [9]) {
//				filter.enabled = true;
				distFilter.enabled = true;
				StartCoroutine (FadeVolume(ocean, 1, 7));
				StartCoroutine (FadeDist (distFilter, 0.5f, 7));
				StartCoroutine(FinishTalking ());
			}

			if (audioSour.clip == crabWelcomeClips [3]||audioSour.clip == crabWelcomeClips [6]) {
				crabAnim.Play ("Sea_Idle 0");
			}
			if (audioSour.clip == crabWelcomeClips [5]) {
				oceanBox.gameObject.GetComponent<Animator> ().Play ("OceanRising");
			}
			if (audioSour.clip == crabWelcomeClips [8]) {
				crabAnim.SetTrigger ("CrabFinishedTalking");
			}
		
			audioSour.Play ();
			yield return new WaitForSeconds (crabWelcomeClips[i].length);

		}
	}
	IEnumerator InvokePlayRandomHelp(){
		while (isTimeForRandomHelp) {
			audioSour.clip = GetRandomHelpMeClip ();
			audioSour.Play ();
			yield return new WaitForSeconds (Random.Range (GetRandomHelpMeClip().length+1, 4));			
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

	IEnumerator FadeDist(AudioDistortionFilter audioDist, float maxValue, float fadeTime)
	{
		// run once when calling start couroutine
		float curDist = audioDist.distortionLevel;

		// updates every frame untill it reaches the exact fadeTime
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / fadeTime)
		{
			audioDist.distortionLevel = Mathf.Lerp (curDist, maxValue, t);


			yield return null;
		}
	}

//	 void CalculateTimeOfLast5Lines(){
//		timeForOcean = crabWelcomeClips [5].length + crabWelcomeClips [6].length +
//			crabWelcomeClips [7].length + crabWelcomeClips [8].length;
//	}



}
