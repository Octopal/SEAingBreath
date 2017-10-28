using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ChangeVideos : MonoBehaviour {

	public GameObject [] vObjects;
	public VideoPlayer [] vPlayers;
	private VideoPlayer curVplayer;
	public int vidNum;
	public GameObject panel;

	void Start () {




	}

	// Use this for initialization
	//надо чтобы в этой функции в event sys можно было самому вводить int. Как?
	public void PlayVid(int vidNum){
		
			panel.SetActive (false);

		
		foreach (VideoPlayer obj in vPlayers) {
			obj.Stop ();

		}

		foreach (GameObject obj in vObjects) {
			obj.SetActive (false);

		}



		vObjects [vidNum].SetActive (true);

		vPlayers [vidNum].Prepare ();
		vPlayers [vidNum].prepareCompleted += playWhenPrepeared;

	}
	void playWhenPrepeared(VideoPlayer p){
		p.Play ();
	}






}
