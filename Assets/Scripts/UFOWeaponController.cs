using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// MonoBehavior for controlling the UFO object - invokes the UFO Weapon when the mouse is clicked.
/// 
/// Written by Malcolm Riley
/// CMPM 163, Spring 2019
/// </summary>
public class UFOWeaponController : MonoBehaviour {

	// Public Fields
	[Header("References")]
	public FireWeaponEvent onFireWeapon;

	[Header("Configuration")]
	public float cooldownTime = 0.5F;
	public float maxDistance = 50.0F;

	// Internal Fields
	private const float EPSILON = 0.01F;
	private float _cooldown;

	public void Update() {
		UpdateCooldown();
		if (ShouldFire()) {
			Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(raycast, out RaycastHit hit, maxDistance)) {
				_cooldown = 1.0F;
				onFireWeapon.Invoke(hit.point);
			}
		}
	}

	public void PrintDebugInfo(Vector3 vector) {
		Debug.Log(vector);
	}

	// Internal Methods
	private void UpdateCooldown() {
		_cooldown = Mathf.Clamp01(_cooldown - (Time.deltaTime / cooldownTime));
	}

	private bool ShouldFire() {
		return CooldownExpired() && InputActivated();
	}

	private bool CooldownExpired() {
		return (_cooldown <= EPSILON);
	}

	private bool InputActivated() {
		return Input.GetMouseButtonDown(0);
	}
}

[System.Serializable]
public class FireWeaponEvent : UnityEvent<Vector3> { }
