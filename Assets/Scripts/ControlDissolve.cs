using UnityEngine;

/// <summary>
/// MonoBehavior for controlling a dissolve shader attached to a MeshRenderer.
/// 
/// Written by Malcolm Riley
/// CMPM 163, Spring 2019
/// </summary>
public class ControlDissolve : MonoBehaviour {

	// Public Fields
	[Header("References")]
	public MeshRenderer controlledRenderer;
	public Material dissolveMaterial;
	public string dissolveProperty;

	[Header("Configuration")]
	[Range(0.0F, 30.0F)]
	public float effectSpeed;
	public bool incremental;

	// Internal Fields
	private bool isDissolving = false;
	private float _progress;
	private Material _material;

	public void Start() {
		_progress = 0.0F;
		_material = Instantiate(dissolveMaterial);
		controlledRenderer.material = _material;
	}

	public void BeginDissolve() {
		if (!isDissolving) {
			_progress = 0.0F;
		}
		isDissolving = true;
	}

	public void IncrementDissolve(float value) {
		_progress += value;
	}

	public void Update() {
		if (isDissolving || incremental) {
			if (!incremental) {
				_progress = Mathf.Clamp01(_progress + (Time.deltaTime / effectSpeed));
			}
			_material.SetFloat(dissolveProperty, _progress);
		}
	}
}