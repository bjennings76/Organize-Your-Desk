using UnityEngine;

public class BounceOnClick : MonoBehaviour {
	public float BounceForce;

	private Rigidbody m_Rigidbody;

	private Rigidbody Rigidbody {
		get { return m_Rigidbody ? m_Rigidbody : (m_Rigidbody = GetComponent<Rigidbody>()); }
	}

	private void OnMouseDown() {
		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) {
			Rigidbody.AddForceAtPosition(Vector3.up * BounceForce, hit.point);
		}
	}
}