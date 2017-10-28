using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Hotspot))]
public class HotspotInspector : Editor {

	public override void OnInspectorGUI()
	{
		Hotspot hs = (Hotspot)target;
		if(GUILayout.Button("OnCollide")){
			hs.OnCollide ();
		}
		DrawDefaultInspector();
	}
}
