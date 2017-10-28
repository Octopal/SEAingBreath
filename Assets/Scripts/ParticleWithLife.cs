using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleWithLife : GParticle {
	public float lifeTime = 0;
	public float lifeTimeMax = 0;
	public float deathTimer = 0;
	public ParticleSystem ps;
	public MeshRenderer render;
	public bool isDead = false;
	public bool markIsDead = false;

	public void Setup(){
		render = obj.GetComponent<MeshRenderer> ();
		ps = obj.GetComponent<ParticleSystem> ();
	}
	public void Exploide(){
		ps.Play ();
		render.enabled = false;
		markIsDead = true;
	}
	public void UpdateLifeTime(){
		if (markIsDead) {
			deathTimer += Time.deltaTime;
			if (deathTimer > 0.5f) {
				Destroy (obj);
				isDead = true;
				markIsDead = false;
			}
		}
		if (radius > radiusInit) {
			lifeTime++;
		}
	}

}
