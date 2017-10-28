using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CylRotation : MonoBehaviour {
	[Tooltip("The x and z pos of this object > `GaseFollower>Point`")]
	public GameObject newPos;
	[Tooltip("MainCamera")]
	public GameObject cam;
	[Tooltip("Keeps this object above the ground, it uses scale and pos parameters of the rangeBox object")]
	public GameObject rangeBox;
	[Tooltip("Links rotation to target > `GaseFollower`")]
	public Transform target;
	[Tooltip("Bottom point is a ground border")]
	public Transform bottomPoint;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		float yPos = newPos.transform.position.y;
		float borderBottom = -rangeBox.transform.localScale.y / 2 + rangeBox.transform.position.y - bottomPoint.localPosition.y;
		if (yPos < borderBottom) {
			yPos = borderBottom;
		}
		transform.position = new Vector3(newPos.transform.position.x, yPos, newPos.transform.position.z);

		Quaternion ori = target.rotation;
		transform.rotation = Quaternion.Euler (new Vector3(0, ori.eulerAngles.y, 0));
	}
}
