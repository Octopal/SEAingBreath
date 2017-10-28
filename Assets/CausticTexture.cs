using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CausticTexture : MonoBehaviour {

	public float fps = 30.0f;
	public Texture2D[] frames;
	private int frameIndex;
	private Material mat;


	void Start()
	{
		mat = gameObject.GetComponent<Renderer>().material;
		InvokeRepeating("NextFrame", 0, 1 / fps);
	}

	void NextFrame()
	{
		mat.SetTexture("_EmissionMap", frames[frameIndex]);
		frameIndex = (frameIndex + 1) % frames.Length;
	}
}
