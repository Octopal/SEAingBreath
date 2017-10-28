using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GParticleSystem : MonoBehaviour {
	List<ParticleWithLife> particles;
	public GameObject particlePrefab;
	public GameObject spawnObject;
	public GameObject attractor;
	public float spawnRadius = 2.0f;
	public float spawnSpeed = 0.02f;
	public float attractionRadius = 10;
	public float attractionForce = 0.01f;
	public float radiusOffset = 0.0f;
	public float radiusOffsetMax = 5.0f;
	public int count = 100;
	int popedBubblesCount = 0;
	BreathState breathState;
	bool isSolved = false;
	bool isParticlesActive = false;

	public UnityEvent onSolveBubbles;
	public UnityEvent onBubblePop;

	// Use this for initialization
	void Start () {
		particles = new List<ParticleWithLife> ();
	}
	public void CreateParticles(){
		StartCoroutine (InvokeParticles());
	}
	public void Reset(){
		isParticlesActive = false;
		particles.Clear ();
		popedBubblesCount = 0;
		isSolved = false;
	}
	IEnumerator InvokeParticles(){
		for (int i = 0; i < count; i++) {
			// create object
			GameObject obj = Instantiate (particlePrefab) as GameObject;

			// radius
			float radius = Random.Range(0.2f, 1.0f);
			obj.transform.localScale = new Vector3 (radius, radius, radius);

			// particle
			ParticleWithLife particle = new ParticleWithLife ();
			particle.damping = Random.Range(0.01f, 0.06f);
			particle.lifeTimeMax = Random.Range(400, 900);
			particle.radius = radius;
			particle.radiusInit = radius;
			particle.obj = obj;
			particle.Setup ();
			particle.id = i;
			Vector3 p = spawnObject.transform.position;
			Vector3 newPos = new Vector3 (Random.Range(0,spawnRadius)+p.x, Random.Range(0, spawnRadius)+p.y, Random.Range(0, spawnRadius)+p.z);
			particle.pos = newPos;
			obj.transform.position = newPos;
			particles.Add (particle);
			isParticlesActive = true;
			yield return new WaitForSeconds (spawnSpeed);
		}
	}
	public void OnBreathStateChange(BreathState state){
		breathState = state;
	}
	public void SetRadiusOffset(float r){
		radiusOffset = r.Remap(-1, 1, 0, radiusOffsetMax);
	}
	public void DestroyAllParticles(){
		foreach (ParticleWithLife p in particles) {
			Destroy(p.obj);
		}
		particles.Clear ();
	}
	public void SolveBubbles(){
		onSolveBubbles.Invoke ();
		Reset ();
	}
	// Update is called once per frame
	void Update () {
		if (isParticlesActive) {
			if (particles.Count == 0) {
				onSolveBubbles.Invoke ();
				isSolved = true;
				Reset ();
				print("Particles Solved");
			}
		}
		foreach (ParticleWithLife p in particles) {			
			if (p.lifeTime == p.lifeTimeMax) {
				onBubblePop.Invoke ();
				p.Exploide ();
				popedBubblesCount++;
			}
		}
		particles.RemoveAll (p=> {
			return p.isDead;
			;});
		foreach (ParticleWithLife p in particles) {
			if (!p.isStill) {
				p.ResetForce ();
				if (breathState == BreathState.BreathIn || breathState == BreathState.BreathInHold) {
					p.radius = p.radiusInit + radiusOffset;
				} else {
					p.radius = p.radiusInit;
				}
			}
		}
		foreach (ParticleWithLife p in particles) {
			if (!p.isStill) {
				Vector3 frc = new Vector3 (0, 0, 0);
				p.AddAttractionForce (attractor.transform.position, attractionRadius, attractionForce);
				foreach (ParticleWithLife p2 in particles) {
					p.AddRepulsionForce (ref p2.frc, p2.pos, p.radius, 0.01f);
				}
			}
		}
		foreach (ParticleWithLife p in particles) {
			if (!p.isStill) {
				p.AddDampingForce ();
				p.Update ();
				if(breathState == BreathState.BreathIn || breathState == BreathState.BreathInHold) p.UpdateLifeTime ();
			}
		}
	}
}
