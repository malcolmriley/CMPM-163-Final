using UnityEngine;

/// <summary>
/// MonoBehavior for automatically destroying the parent GameObject.
/// 
/// Written by Malcolm Riley
/// CMPM 163, Spring 2019
/// </summary>
public class AutoDestroy : MonoBehaviour {

	// Public Fields
	public float destroyAfter = 5.0F;

	// Internal Fields
	private float _progress;

	public void Update() {
		_progress = Mathf.Clamp01(_progress + (Time.deltaTime / destroyAfter));
		if (_progress >= 1.0F) {
			Destroy(this);
		}
	}
}