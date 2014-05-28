using UnityEngine;
using System.Collections;

public class characterLerp : MonoBehaviour {

	private Vector2 oldPosition;
	private Vector2 newPosition;
	private float smooth = 10;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		changePosition ();
	}

	void changePosition() {
		transform.position = Vector2.Lerp (oldPosition, newPosition, Time.deltaTime*smooth);
	}

}
