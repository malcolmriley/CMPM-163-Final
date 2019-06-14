using UnityEngine;

/// <summary>
/// MonoBehavior for controlling the UFO object - Launches a projectile at a point when invoked.
/// 
/// Written by Malcolm Riley
/// CMPM 163, Spring 2019
/// </summary>
public class FireProjectile : MonoBehaviour {

	// Public Fields
	[Header("References")]
	public GameObject projectilePrefab;
	public Transform projectileOrigin;

	[Header("Configuration")]
	public float projectileSpeed;

	public void Fire(Vector3 vector) {

	}
}