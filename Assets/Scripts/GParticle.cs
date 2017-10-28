using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GParticle : MonoBehaviour{
	public GameObject obj;
	public Vector3 pos;
	public Vector3 posPrev;
	public Vector3 vel;
	public Vector3 frc;
	public float radius;
	public float radiusInit;
	public float damping;
	public bool bFixed;
	public bool isStill;
	public int id;

	public GParticle(){
		Setup ();
	}
	public void Setup(){
		isStill = false;
		damping = 0.03f;
		bFixed = false;
		posPrev = new Vector3 (0, 0, 0);
		pos = new Vector3 (0, 0, 0);
		vel = new Vector3 (0, 0, 0);
		frc = new Vector3 (0, 0, 0);
		radius = 1;
	}

	public void ResetForce(){
		frc = new Vector3 (0, 0, 0);
	}
	public void AddForce(Vector3 f){
		frc += f;
	}
	public void AddAttractionForce(Vector3 force, float radius, float scale){
		Vector3 posOfForce = new Vector3(force.x, force.y, force.z);

		Vector3 diff = pos - posOfForce;
		float length = diff.magnitude;

		bool bAmCloseEnough = true;
		if (radius > 0){
			if (length > radius){
				bAmCloseEnough = false;
			}
		}

		if (bAmCloseEnough == true){
			float pct = 1 - (length / radius);  // stronger on the inside
			diff.Normalize();
			frc = frc - diff * scale * pct;
		}

	}
	public void AddRepulsionForce(ref Vector3 force, Vector3 posOf, float radius, float scale){
		Vector3 posOfForce = new Vector3(posOf.x, posOf.y, posOf.z);

		Vector3 diff = pos - posOfForce;
		float length = diff.magnitude;

		bool bAmCloseEnough = true;
		if (radius > 0){
			if (length > radius){
				bAmCloseEnough = false;
			}
		}

		if (bAmCloseEnough == true){
			float pct = 1 - (length / radius);  // stronger on the inside
			diff.Normalize();

			frc = frc + diff * scale * pct;

			force = force - diff * scale * pct;
		}

	}
	public void Update(){
		if (bFixed == false){
			vel = vel + frc;
			pos = pos + vel;
		}
		obj.transform.localPosition = pos;
//		if (frc.magnitude < 0.0001f)
//			isStill = true;
	}
	public void AddDampingForce(){
		frc = frc - vel * damping;
	}
}

