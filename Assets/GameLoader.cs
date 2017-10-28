using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour {
	public GameObject gameManagerPrefab;
	public GameObject preloadCanvas;
	public GameObject pointer;
	private Slider slider;

	GameManager gameManager;
	GameObject gameManagerObj;
	private float delay = 0;

	[System.Serializable]
	public class OnLoading : UnityEvent<float> {};
	public OnLoading onLoadingEvent;

	// Use this for initialization
	void Awake () {
		if (GameManager.instance == null) {
			gameManagerObj = Instantiate (gameManagerPrefab); 
			gameManagerObj.name = "gameManager";
		} else {
			gameManagerObj = GameObject.Find("gameManager");
		}
		gameManager = gameManagerObj.GetComponent<GameManager> ();
		gameManager.onLoadingEvent += OnLoadingFunc;
	}
	void OnDestroy(){
		gameManager.onLoadingEvent -= OnLoadingFunc;
	}
	public void OnLoadingFunc(float pct){
		if (slider != null) {
			slider.value = pct;
		}
		onLoadingEvent.Invoke (pct);
	}
	public void LoadScene(int num){
		HideController ();
		CreatePreloader ();
		gameManager.LoadScene (num);
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
	void HideController(){
		pointer.SetActive (false);
	}
	void CreatePreloader(){
		if (slider == null) {
			GameObject canvas = Instantiate (preloadCanvas)as GameObject;
			canvas.transform.parent = Camera.main.transform;
			canvas.transform.localRotation = Quaternion.identity;
			canvas.transform.localPosition = new Vector3(0, 0, 2);
			slider = canvas.GetComponentInChildren<Slider>();
		}
	}
}
