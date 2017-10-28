using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	int curSceneNum = 0;
	bool isLoading =  false;
	public static GameManager instance = null;
	public static GameObject gameObj = null;
	public delegate void LoaderDelegate(float num);
	public LoaderDelegate onLoadingEvent;

	// Use this for initialization
	void Awake () { 
		//Check if instance already exists
		if (instance == null) {
			instance = this;
			gameObj = gameObject;
		} else if (instance != this) {
			
		}

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);
	}
	public void NextScene(){
		curSceneNum++;
		curSceneNum = Mathf.Clamp (curSceneNum, 0, SceneManager.sceneCountInBuildSettings);
		SceneManager.LoadScene (curSceneNum);

	}
	public void LoadScene(int sceneNumber){
//		SceneManager.LoadScene (sceneNumber);
		if(!isLoading)
			StartCoroutine( LoadSceneAsync(sceneNumber));
	}
	IEnumerator LoadSceneAsync(int sceneNum){
		AsyncOperation op = SceneManager.LoadSceneAsync (sceneNum);
		isLoading = true;
		while (!op.isDone) {
			yield return op.isDone;
			print ("%: " + op.progress);
			if (onLoadingEvent != null) {
				onLoadingEvent (op.progress);
			}
		}
		isLoading = false;
		print ("loaded");
	}
	public void PrevScene(){
		curSceneNum--;
		curSceneNum = Mathf.Clamp (curSceneNum, 0, SceneManager.sceneCountInBuildSettings);
		SceneManager.LoadScene (curSceneNum);
	}
}
