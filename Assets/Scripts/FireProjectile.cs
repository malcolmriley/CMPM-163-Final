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
	public float projectileSpeed = 5.0F;

	public void Fire(Vector3 vector) {
		// Instantiate
		GameObject instance = Instantiate(projectilePrefab);

		// Set position and rotation
		instance.transform.LookAt(vector);
		instance.transform.position = projectileOrigin.position;

		// Apply force
		Rigidbody body = instance.GetComponent<Rigidbody>();
		if (body != null) {
			body.velocity = (vector - projectileOrigin.position).normalized * projectileSpeed;
		}
	}
}