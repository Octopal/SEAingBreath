using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;
using System.IO;
using System.Collections.Generic;


#if UNITY_EDITOR
//#if UNITY_STANDALONE_OSX
using System.IO.Ports;


public class ArduinoInput : MonoBehaviour {
	SerialPort serialPort;
	float[] lastRot = { 0, 0, 0 };
	float xPos = 0;
	bool isTouch;
	bool isButton;
	float xPosStep = 0.07f;
	bool onceTouch = true;
	bool onceTouch2 = true;
	bool onceButton = true;
	bool onceButton2 = true;
	public string serialPathStartWith = "/dev/tty.usbmodem";

	public float airFlowMin = 0;
	public float airFlowMax = 100;

	public bool isDebug = false;
	// event
	[System.Serializable]
	public class FloatEvent : UnityEvent<float>{};
	public FloatEvent onAirflow;

	void Awake(){
		isTouch = false;
		isButton = false;

		serialPort = new SerialPort (getPortName (serialPathStartWith), 38400);
	}

	string getPortName (string contains)
	{
		int p = (int)Environment.OSVersion.Platform;
		List<string> serial_ports = new List<string> ();

		// Are we on Unix?
		if (p == 4 || p == 128 || p == 6) {
			string[] ttys = Directory.GetFiles ("/dev/", "tty.*");
			foreach (string dev in ttys) {
				if (dev.StartsWith (contains)) {
					return dev;
				}
			}
		}
		return "not found";
	}

	public void OnDestroy (){
		if (serialPort != null) {
			if (serialPort.IsOpen) {
				print ("closing serial port");
				serialPort.Close ();
			}
			serialPort = null;
		}
	}

	void Start () {
		if (serialPort != null) {
		
			serialPort.Open ();
			serialPort.ReadTimeout = 30;
//		serialPort.ReadBufferSize = 1024;
		}
	}

	void Update () {
		if (serialPort != null && serialPort.BytesToRead > 0 && serialPort.IsOpen) {
			try{
				string line = serialPort.ReadLine();
				if(isDebug){
					print(line);
				}
				float a = float.Parse(line);
				float b = Map(a, airFlowMin, airFlowMax, 0, 1);
				onAirflow.Invoke(b);
			}
			catch(System.Exception e){
				print ("System.Exception in serial.ReadLine: " + e.ToString ());
			}
		}
	}
	void TouchBegin(){
		if (onceTouch) {
			onceTouch = false;
		}
	}
	void TouchEnd(){
		if (onceTouch2) {
			onceTouch2 = false;
		}
	}
	void ButtonBegin(){
		if (onceButton) {
			onceButton = false;
		}
	}
	void ButtonEnd(){
		if (onceButton2) {
			onceButton2 = false;
		}
	}
	public float Map( float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}
}
#endif
//#endif
