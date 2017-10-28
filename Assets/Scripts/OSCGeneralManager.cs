using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OSCGeneralManager : MonoBehaviour {
	public string RemoteIP = "127.0.0.1"; //127.0.0.1 signifies a local host (if testing locally
	public int SendToPort = 9000; //the port you will be sending from
	public int ListenerPort = 3334; //the port you will be listening on
	public Osc handler;

	// Use this for initialization
	void Awake () {
		// setup osc
		UDPPacketIO udp = GetComponent<UDPPacketIO>();
		udp.init(RemoteIP, SendToPort, ListenerPort);
		handler = GetComponent<Osc>();
		handler.init(udp);

	}
}
