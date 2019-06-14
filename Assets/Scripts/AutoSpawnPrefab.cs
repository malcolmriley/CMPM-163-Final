using UnityEngine;

/// <summary>
/// MonoBehavior for spawning a prefab.
/// 
/// Written by Malcolm Riley
/// CMPM 163, Spring 2019
/// </summary>
public class AutoSpawnPrefab : MonoBehaviour {

	// Public Fields
	public GameObject prefab;

	public void SpawnPrefab(Vector3 position) {
		Instantiate(prefab, position, Quaternion.identity);
	}
}