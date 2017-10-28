using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour {
	public AudioSource menuDisappearSound;
	public AudioSource clickSound;
	public AudioSource introSong;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadNext(){
		SceneManager.LoadScene("Beach");
	}

	public void PlayClickSound(){
		clickSound.Play ();
	}

	public void PlayMenuDisappearSound(){
		menuDisappearSound.Play ();
	}




}
