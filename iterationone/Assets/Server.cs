using UnityEngine;
using System.Collections;

public class Server : MonoBehaviour {

	public Place[] placeList;
	private float smooth = 10;
	private faceMover spriteToMove;
	public Camera ourCam;
	public bool moving = false;
	public Place currentPlace;
	public Place targetPlace;

	// Use this for initialization
	void Start () {
		GameObject target = GameObject.Find("FirstSprite");
		spriteToMove = target.GetComponent<faceMover>();

		placeList = new Place[transform.childCount];
		int i = 0;
		foreach (Transform child in transform) {
			placeList[i] = child.GetComponent<Place>();
			Debug.Log(i +": "+ placeList[i].name);
			i++;
		}
		currentPlace = placeList [0];
		targetPlace = placeList [0];
		Debug.Log ("Current Place: " + currentPlace.name);
	}
	
	// Update is called once per frame
	void Update () {
		if (moving)
			moveSprite ();
		else 
			mouseHandler ();
	}

	void moveSprite() {
		Vector2 newPosition2D = new Vector2(targetPlace.transform.position.x, targetPlace.transform.position.y);
		moving = spriteToMove.move (newPosition2D);
		if (!moving)
			currentPlace = targetPlace;
	}

	void mouseHandler(){
		if (Input.GetMouseButtonDown (0)) {
			targetPlace = currentPlace;
			Vector3 mouseLoc = ourCam.ScreenToWorldPoint (Input.mousePosition);
			mouseLoc.z = 0f;
			Debug.Log (mouseLoc);
			for (int i = 0; i < placeList.Length; i++){
				if (placeList[i].collider2D.OverlapPoint(new Vector2(mouseLoc.x, mouseLoc.y))){
					targetPlace = placeList[i];
					moving = true;
					break;
				}
			}
		}
	}
}
