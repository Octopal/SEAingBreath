using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour {
	private SpriteRenderer sprite;
	public float fadeSpeed = 0.8f;

	private float alpha = 1.0f; 
	private int fadeDir = -1;
	private bool isFade = false;

	public UnityEvent onFadeInFinishedEvent;
	public UnityEvent onFadeOutFinishedEvent;

	private float delay = 0;

	void Start(){
		sprite = GetComponent<SpriteRenderer> ();
		FadeIn ();
	}
	// Use this for initialization
	void Update () {
		if (isFade) {
			alpha += fadeDir * fadeSpeed * Time.deltaTime;  
			alpha = Mathf.Clamp01(alpha);   

			if (alpha == 0) {
				onFadeInFinishedEvent.Invoke ();
				isFade = false;
			}
			if (alpha == 1) {
				onFadeOutFinishedEvent.Invoke ();
				isFade = false;
			}
		}

		sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
	}

	public void FadeOut(){
		alpha = 0;
		fadeDir = 1;
		isFade = true;
	}
	public void FadeIn(){
		alpha = 1;
		fadeDir = -1;
		isFade = true;
	}
	public void LoadScene(int num){
		SceneManager.LoadScene (num);
	}

	public void SetDelay(float delay){
		this.delay = delay;
	}
	public void LoadSceneWithDelay(int num){
		StartCoroutine (LoadSceneC(num));
	}
	IEnumerator LoadSceneC(int num){
		yield return new WaitForSeconds (delay);
		LoadScene (num); 
	}
}
