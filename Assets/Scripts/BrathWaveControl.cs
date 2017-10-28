using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using UnityEngine.Events;

[Serializable]
public class OscBreathData{
	public List<float> data = new List<float>();
	public int counter;
}
public class BreathPoint{
	public float value;
	public float time;
}

[System.Serializable]
public enum BreathState{
	BreathIn, BreathOut, BreathInHold, BreathOutHold, Move
}

public enum BreathTrackingMode{
	OSC, OSCPlayer, Controller
}

public class BrathWaveControl : MonoBehaviour {
	// BreathState
	[HideInInspector]
	public BreathState breathState;
	BreathState breathStatePrev;

	// osc manage
	bool isRecord = false;
	bool isDataLoaded = false;
	float time = 0; // osc can't get Time.time;

	[Header("Breath Tracking")]
	public BreathTrackingMode breathTrackingMode;
	bool isOsc{ get{ return breathTrackingMode == BreathTrackingMode.OSC ? true : false;} }
	bool isOscPlayer{ get{ return breathTrackingMode == BreathTrackingMode.OSCPlayer ? true : false;} }
	bool isController{ get{ return breathTrackingMode == BreathTrackingMode.Controller ? true : false;} }
	public float normForce = 0.99f;
	int dataCounter = 0;
	string fileName = "/oscBreathData.dat";
	OscBreathData oscBreathData = new OscBreathData();

	
	// osc input
	public OSCGeneralManager manager;
	public string address = "/Gyro";
	float[] values = new float[3]; // gyro x, y, z

	[Header("Render")]
	// LineRenderer
	// plotter
	public LineRenderer line;
	public bool isLineRenderer = false;

	// distance to check vector
	[Header("Parameters"), Tooltip("The cayan line with two points you can see in editor")]
	public int breathLineDistance = 5;
	[Tooltip("Affects the difference betwen two points")]
	public float breathLineTreshold = 0.02f;
	[Tooltip("Clean the jitter in case of movement")]
	public float timeTreshold = 0.02f;
	[HideInInspector]
	public float breathValue = 0;
	[Tooltip("If abs posY is bigger than 'movementTreshold' it's been detected as movement")]
	public float movementTreshold = 1.5f;
	[Tooltip("Zeno smooth")]
	public float smoothBreathValue = 0.9f;

	// _-_ check the difference
	[HideInInspector]
	public bool isUp = false;
	bool isPrevUp = false;

	// range values, we gonna store them on each isUp change
	List<BreathPoint> breathPoints = new List<BreathPoint>();

	// gyro
	float gyroY;

	[Header("Points")]
	public int pointsMax = 500;
	public float plotStep = 1;
	[HideInInspector]
	public Vector3 plotter;

	[HideInInspector]
	public List<Vector3> points ;

	// Events
	bool isBreathIn = false;
	bool isBreathInPrev = false;

	[System.Serializable]
	public class BreathEvent : UnityEvent<float>{};
	public BreathEvent onBreathValueEvent;

	[System.Serializable]
	public class BreathStateEvent : UnityEvent<BreathState>{};
	public BreathStateEvent onBreathStateEvent;

	public UnityEvent onBreathInEvent;
	public UnityEvent onBreathOutEvent;

	// Use this for initialization
	public void Start () {
		points = new List<Vector3>();

		plotter = new Vector3 (0, 0, 0);

		if(isLineRenderer) line.positionCount = pointsMax;

		if (isOsc) manager.handler.SetAllMessageHandler(AllMessageHandler);

		if (isOscPlayer) Load ();
	}

	public void Save(){
		Debug.Log ("save to " + Application.persistentDataPath + fileName);
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + fileName);
		bf.Serialize (file, oscBreathData);
		file.Close ();
	}

	public void Load(){
		if (File.Exists (Application.persistentDataPath + fileName)) {
			Debug.Log ("load file " + Application.persistentDataPath + fileName);
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open(Application.persistentDataPath + fileName, FileMode.Open);

			oscBreathData = (OscBreathData)bf.Deserialize (file);
			file.Close ();
			isDataLoaded = true;
		}
	}

	public void AllMessageHandler(OscMessage oscMessage){
		if (oscMessage.Address == address) {	
			values [0] = Convert.ToSingle ( oscMessage.Values [0] );
			values [1] = Convert.ToSingle ( oscMessage.Values [1] );
			values [2] = Convert.ToSingle ( oscMessage.Values [2] );
			UpdatePoints (values[1]);
		}
	}

	public void UpdatePoints(float y){
		recordOsc (y);
		addPoint (y);
		breathStateCheck ();
		setBreathValue ();
		clearOldPoints ();
	}

	void setBreathValue(){
		if (points.Count > 0 && breathPoints.Count>1) {
			BreathPoint p = breathPoints [breathPoints.Count - 1];
			BreathPoint pp = breathPoints [breathPoints.Count - 2];

			float breathDiff = points [points.Count - 1].y - p.value;
			float timeDiff = p.time - pp.time;

			float prevPointY = points [points.Count - breathLineDistance].y;
			float curPointY = points [points.Count - 1].y;
			float lineDiff = curPointY - prevPointY;

			if (Mathf.Abs (breathDiff) < movementTreshold && Mathf.Abs (timeDiff) > timeTreshold) {
				if (lineDiff > 0 && Mathf.Abs (lineDiff) > breathLineTreshold) {
					breathState = BreathState.BreathIn;
					isBreathIn = true;
				} else if (Mathf.Abs (lineDiff) > breathLineTreshold) {
					breathState = BreathState.BreathOut;
					isBreathIn = false;
				} else if (isUp) {
					breathState = BreathState.BreathInHold;
					isBreathIn = true;
				} else {
					breathState = BreathState.BreathOutHold;
					isBreathIn = false;
				}
				breathValue = (1-smoothBreathValue)*breathDiff + smoothBreathValue*breathValue;
				
			} else {
				// movement
				breathState = BreathState.Move;
			}

			UpdateBreathStateChange ();
		}
	}
	void UpdateBreathStateChange(){
		if (breathStatePrev != breathState) {
			onBreathStateEvent.Invoke (breathState);
		}
		if (isBreathInPrev != isBreathIn) {
			if (breathState == BreathState.BreathIn || breathState == BreathState.BreathInHold) {
				onBreathInEvent.Invoke ();
			} else {
				onBreathOutEvent.Invoke ();
			}
		}
		breathStatePrev = breathState;
		isBreathInPrev = isBreathIn;
	}
	void recordOsc(float y){
		if(isRecord) oscBreathData.data.Add(y);
	}

	void addPoint(float y){
		// add point
		Vector3 point = new Vector3 (0, y, 0);
		plotter = new Vector3 (0, y + plotter.y, 0);
		if (points.Count > 1) {
			point = new Vector3 (points[points.Count-1].x + plotStep, plotter.y, plotter.z);
		}
		points.Add (point);
	}
	void clearOldPoints(){
		if (points.Count > pointsMax) {
			for(int i = 0 ; i < points.Count; i++) {
				points[i] = new Vector3 (points[i].x-plotStep, points[i].y, points[i].z);
			}
			points.RemoveAt (0);
		}
	}
	void breathStateCheck(){
		// check direction up or down
		if (points.Count > breathLineDistance - 1) {
			float prevPointY = points [points.Count - breathLineDistance].y;
			float curPointY = points [points.Count - 1].y;

			float diff = curPointY - prevPointY;

			if (diff > 0 && Mathf.Abs (diff) > breathLineTreshold) {
				isUp = true;
			} else if (Mathf.Abs (diff) > breathLineTreshold) {
				isUp = false;
			}
				
			if (isUp != isPrevUp) {
				OnUpChange ();
			}

			isPrevUp = isUp;
		}
	}

	void OnUpChange(){
		if (points.Count > 0) {
			BreathPoint p = new BreathPoint();
			p.value = points [points.Count - 1].y;
			p.time = time;
			breathPoints.Add (p);
		}
	}

	// Update is called once per frame
	public void Update () {
		time = Time.time;

		// osc player
		if (isOscPlayer && isDataLoaded) {
			UpdatePoints (oscBreathData.data[dataCounter]);
			dataCounter++;
			if (dataCounter > oscBreathData.data.Count - 1) {
				dataCounter = 0;
			}
		}

		// controller
		if (isController) {
			UpdatePoints (GvrController.Gyro.y);
		}
			
		float normValue = breathValue.Remap (-1, 1, 0, 1);
		onBreathValueEvent.Invoke (normValue);

		if (Input.GetKeyUp (KeyCode.R)) {
			isRecord ^= true;
		}
		if (Input.GetKeyUp (KeyCode.S)) {
			Save ();
		}
		if (Input.GetKeyUp (KeyCode.L)) {
			Load ();
		}

		// add points to line
		if(isLineRenderer && points.Count>pointsMax-1){
			for(int i = 0 ; i < points.Count; i++) {
				line.SetPosition (i, points [i]);
			}
		}


	}

}
