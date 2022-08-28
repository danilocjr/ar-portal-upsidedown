using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {

	ParticleSystem ps;
	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem> ();
		SampleParticles ();
	}

	public void SampleParticles(){
		StartCoroutine (SampleParticlesRoutine ());
	}

	IEnumerator SampleParticlesRoutine(){
		var main = ps.main;
		main.simulationSpeed = 1000f;
		ps.Play ();
		yield return new WaitForEndOfFrame ();
		main.simulationSpeed = .1f;
	}
}
