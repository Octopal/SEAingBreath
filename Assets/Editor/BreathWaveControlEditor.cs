using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BrathWaveControl))]
public class BrathWaveControlEditor : Editor {
	BrathWaveControl control;
	bool isStart = true;

	private void OnSceneGUI(){
		if (isStart) {
			control = target as BrathWaveControl;
			control.Start ();
			isStart = false;
			Debug.Log ("start");
		}

		Update ();
	}
	void Draw(){
		Handles.color = Color.yellow;
		int pointsSize = control.points.Count;

		if (pointsSize > 1) {
			for (int i = 1; i < pointsSize; i++) {
				Vector3 p1 = control.points[i];
				Vector3 p2 = control.points[i-1];
				Handles.DrawLine (p1, p2);
			}
		}

		// draw plotter
		Vector3 plotter = control.plotter;
		Handles.color = Color.red;
		float boxSize = 2;
		Handles.DrawWireCube (plotter, new Vector3(boxSize, boxSize, boxSize));

		// draw line between plotter and end line
		Vector3 lastPoint = control.points[pointsSize-1];
		Handles.color = Color.green;
		Handles.DrawLine (lastPoint, plotter);	

		// draw check distance
		Vector3 checkDistancePoint = control.points[pointsSize-control.breathLineDistance];
		Handles.color = Color.cyan;
		Handles.DrawLine (lastPoint, checkDistancePoint);	

		// draw last point
		float radius = 1;
		Handles.color = Color.cyan;
		Handles.DrawWireDisc(lastPoint, Vector3.forward, radius);

		// draw check distance
		Handles.color = Color.cyan;
		Handles.DrawWireDisc(checkDistancePoint, Vector3.forward, radius);

		// draw last point value
		string m = lastPoint.y.ToString ();
		DrawText (m, 12, lastPoint);

		// draw status
		m = "Breath State: " + control.breathState + "\n";
		DrawText (m, 12, new Vector3(0, -1, 0));

		// draw breath value
		Vector3 breathPoint = new Vector3(0, control.breathValue, 0);
		Handles.color = Color.green;
		Handles.DrawLine (breathPoint, Vector3.zero);
		Handles.color = Color.green;
		Handles.DrawWireDisc(breathPoint, Vector3.forward, 0.12f);
		m = control.breathValue.ToString ();
		DrawText (m, 12, breathPoint);

	}
	void DrawText(string m, int size, Vector3 pos){
		Handles.color = Color.yellow;
		GUIStyle style = new GUIStyle ();
		style.fontSize = size;
		style.fontStyle = FontStyle.Normal;
		Handles.Label(pos, m , style);
	}
	void Update(){
		control.Update ();

		// Plot the line
		if(control.points.Count>control.breathLineDistance){
			Draw ();
		}
	}
}
