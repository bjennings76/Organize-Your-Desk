using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns a prefab as a child of this component's GameObject. Keeps a tally and will clean up spawned prefabs when there are too many.
/// </summary>
public class PrefabSpawner : MonoBehaviour {
	// Public inspecter settings for prefab to spawn and a limit of spawned instances;
	public GameObject Prefab;
	public int Limit = 50;

	// A queue to keep track of the instance count and remember which instances are the oldest.
	private readonly Queue<GameObject> m_Instances = new Queue<GameObject>();

	// At the start of the game, add any pre-existing items inside the spawner to the instance queue.
	private void Start() {
		for (int i = 0; i < transform.childCount; i++) {
			m_Instances.Enqueue(transform.GetChild(i).gameObject);
		}
	}

	/// <summary>
	/// Spawns the prefab with random offset and rotation.
	/// </summary>
	public void Spawn() {
		// Randomize rotation;
		float randomAngle = Random.value * 360;
		Vector3 randomAxis = new Vector3(Random.value, Random.value, Random.value) * 360;
		Quaternion randomRotation = Quaternion.AngleAxis(randomAngle, randomAxis);

		// Randomize starting position a bit.
		Vector3 offset = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
		Vector3 randomPosition = transform.position + offset;

		// Create an instance of the prefab with a random rotation and offset with the spawner as the parent.
		GameObject instance = Instantiate(Prefab, randomPosition, randomRotation, transform);

		// Add the instance to the instance list so we know how many are about.
		m_Instances.Enqueue(instance);

		// Remove old ones when there are too many around.
		if (m_Instances.Count > Limit) {
			Destroy(m_Instances.Dequeue());
		}
	}
}