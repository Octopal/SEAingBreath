using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GParticleSystem))]
public class GParticleSystemInspector : Editor {

	public override void OnInspectorGUI()
	{
		GParticleSystem ps = (GParticleSystem)target;
		if(GUILayout.Button("CreateParticles")){
			ps.CreateParticles ();
		}
		if(GUILayout.Button("Solve")){
			ps.DestroyAllParticles ();
			ps.SolveBubbles ();
		}
		DrawDefaultInspector();
	}
}
