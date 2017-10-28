using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {


	public string sceneName;
	//public float timeSec = 0;









	public void LoadNextScene (){




			SceneManager.LoadScene (sceneName);
		
	}

	public void WaitForLoading(){

		StartCoroutine (WaitFor ());


	}

	IEnumerator WaitFor(){
		yield return new WaitForSeconds (5);
	}





}
