using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for causing an object to hover.
/// 
/// Written by Malcolm Riley
/// CMPS 163, Spring 2019
/// </summary>
public class Hover : MonoBehaviour {

	// Public Fields
	public float hoverSpeed = 1.0F;
	public float hoverIntensity = 1.0F;

	// Internal Fields
	private Vector3 _startPosition;
	private Vector3 _shadowScale;

	public void Start() {
		_startPosition = transform.localPosition;
	}

	public void OnDisable() {
		transform.localPosition = _startPosition;
	}

	public void Update() {
		float sine = Mathf.Sin(Time.realtimeSinceStartup * hoverSpeed);
		float hoverOffset = hoverIntensity * (1 + sine);
		transform.localPosition = new Vector3(transform.localPosition.x, _startPosition.y + hoverOffset, transform.localPosition.z);
	}
}