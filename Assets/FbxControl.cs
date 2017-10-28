using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Scene{
	public string name;
	public GameObject[] objects;
}

public class FbxControl : MonoBehaviour {
	public Animator animator;
	public List<Scene> scenes;
	public List<string> clipsNames;
	// Use this for initialization
	void Start () {
		
	}
	public void Play (string clipName){
		animator.Play (clipName);
	}
	public void SetScene(string name){
		HideOthersExcept(name);
		ShowScene (name);
	}
	void HideOthersExcept(string name){
		foreach (Scene sc in scenes) {
			if (sc.name != name) {
				HideScene (sc.name);	
			}
		}
	}
	public void ShowScene(string name){
		Scene sc = scenes.Find (obj => obj.name==name);
		foreach (GameObject obj in sc.objects) {
			obj.SetActive (true);	
		}
	}

	public void HideScene(string name){
		Scene sc = scenes.Find (obj => obj.name==name);
		foreach (GameObject obj in sc.objects) {
			obj.SetActive (false);	
		}
	}

	public void SetTrigger(){
		animator.SetTrigger (1);
	}

}
