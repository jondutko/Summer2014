using UnityEngine;
using System.Collections;

public class faceMover : MonoBehaviour {

	private int smooth = 3;

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public bool move(Vector2 newPosition) {
		Debug.Log ("Registered move");
		transform.position = Vector2.Lerp (transform.position, newPosition, Time.deltaTime*smooth);
		Vector2 curPosition = new Vector2 (transform.position.x, transform.position.y);
		if (Vector2.Distance (curPosition, newPosition) < 0.05) {
			transform.position = newPosition;
			return false;
		}

		return true;
	}
}
