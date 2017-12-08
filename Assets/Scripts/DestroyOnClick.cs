using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Deletes the object this component is on when it's clicked.
/// </summary>
public class DestroyOnClick : MonoBehaviour {

	// When the mouse down even occurs...
	private void OnMouseDown() {

		// Make sure we aren't over the UI. If we are, then bail early.
		if (IsOverUI()) {
			return;
		}

		// Delete this clicked object.
		Destroy(gameObject);
	}

	private static bool IsOverUI() {
		PointerEventData pointerData = new PointerEventData(EventSystem.current) {position = Input.mousePosition };
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(pointerData, results);
		return results.Count > 0;
	}
}