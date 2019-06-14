using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MonoBehavior for controlling the Plasma Bolt prefab.
/// 
/// Written by Malcolm Riley
/// CMPM 163, Spring 2019
/// </summary>
public class PlasmaBoltController : MonoBehaviour {

	[Header("Configuration")]
	public float destroyDelay = 5.0F;
	public bool disablePhysics = true;

	[Header("Internal Use Only")]
	public List<GameObject> destroyImmediately;
	public ParticleSystem sparkleEmitter;
	public ParticleSystem trailEmitter;

	public void InitiateDestruction() {
		foreach (GameObject instance in destroyImmediately) {
			Destroy(instance);
		}
		Invoke("SelfDestruct", destroyDelay);
		if (disablePhysics) {
			Destroy(GetComponent<Rigidbody>());
			Destroy(GetComponent<Collider>());
		}
		HaltEmitter(sparkleEmitter);
		HaltEmitter(trailEmitter);
	}

	// Internal Methods
	private void SelfDestruct() {
		Destroy(this.gameObject);
	}

	private void HaltEmitter(ParticleSystem system) {
		system.Stop(true, ParticleSystemStopBehavior.StopEmitting);
	}
}