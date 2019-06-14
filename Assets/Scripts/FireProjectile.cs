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
		instance.transform.position = projectileOrigin.position;
		instance.transform.LookAt(vector);

		// Apply force
		Rigidbody body = instance.GetComponentInChildren<Rigidbody>();
		if (body != null) {
			// Debug.DrawRay(projectileOrigin.position, vector - projectileOrigin.position, Color.red, 5.0F);
			body.velocity = (vector - projectileOrigin.position).normalized * projectileSpeed;
		}
	}
}