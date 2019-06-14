using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// MonoBehavior for assisting with collisions
/// 
/// Written by Malcolm Riley
/// CMPM 163, Spring 2019
/// </summary>
public class CollisionHandler : MonoBehaviour {

	// Public Fields
	[Header("Configuration")]
	public string monitoredTag;

	[Header("References")]
	public OnCollideEvent onCollide;

	public void OnCollisionEnter(Collision collision) {
		GameObject other = collision.collider.gameObject;
		if (other.CompareTag(monitoredTag)) {
			PlasmaBoltController controller = other.GetComponent<PlasmaBoltController>();
			if (controller != null) {
				controller.InitiateDestruction();
			}
			onCollide.Invoke(collision.transform.position);
		}
	}
}

[System.Serializable]
public class OnCollideEvent : UnityEvent<Vector3> { }