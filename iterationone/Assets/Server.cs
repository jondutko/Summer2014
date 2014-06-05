using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Server : MonoBehaviour {

	public Place[] placeList;
	private faceMover spriteToMove;
	public Camera ourCam;
	public bool moving = false, traversePath = false;
	public Place currentPlace;
	public Place targetPlace;
	public List<Place> placePath;

	public int countMAX = 0;

	// Use this for initialization
	void Start () {
		GameObject target = GameObject.Find("FirstSprite");
		spriteToMove = target.GetComponent<faceMover>();

		placeList = new Place[transform.childCount];
		int i = 0;
		foreach (Transform child in transform) {
			placeList[i] = child.GetComponent<Place>();
			Debug.Log(i +": "+ placeList[i].placeName);
			i++;
		}
		currentPlace = placeList [0];
		targetPlace = placeList [0];
		Debug.Log ("Current Place: " + currentPlace.placeName);
	}
	
	// Update is called once per frame
	void Update () {
		if (traversePath)
			moveSprite ();
		else 
			mouseHandler ();
	}

	void moveSprite() {
		Vector2 newPosition2D = new Vector2(targetPlace.transform.position.x, targetPlace.transform.position.y);
		moving = spriteToMove.move (newPosition2D);
		if (!moving) {
			currentPlace = targetPlace;
			if (placePath.Count != 0) {
				targetPlace = placePath[0];
				placePath.RemoveAt (0);
				moving = true;
			}
			else{
				traversePath = false;
				resetPlaces();
			}
		}
	}

	void mouseHandler(){
		if (Input.GetMouseButtonDown (0)) {
			targetPlace = currentPlace;
			Vector3 mouseLoc = ourCam.ScreenToWorldPoint (Input.mousePosition);
			mouseLoc.z = 0f;
			for (int i = 0; i < placeList.Length; i++){
				if (placeList[i].collider2D.OverlapPoint(new Vector2(mouseLoc.x, mouseLoc.y))){
					pathMove(placeList[i]);
					break;
				}
			}
		}
	}

	void pathMove(Place pathTarget) {
		moving = true;
		traversePath = true;
		placePath = doBFS (pathTarget);
		targetPlace = placePath[0];
		Debug.Log ("BFS terminated");
		Debug.Log ("Path size: " + placePath.Count);
		for(int i = 0; i < placePath.Count; i++)
			Debug.Log (placePath[i].placeName);
		placePath.RemoveAt (0);
	}
	
	List<Place> doBFS(Place pathTarget) {
		Queue<Place> BFSQueue = new Queue<Place> ();
		BFSQueue.Enqueue (currentPlace);
		currentPlace.visited = true;
		Debug.Log ("Start point: " + currentPlace.placeName);
		while (BFSQueue.Count != 0) {
			Place cur = BFSQueue.Dequeue();
			Debug.Log ("Current place: " + cur.placeName);
			if(cur.placeName.Equals (pathTarget.placeName)){
				Debug.Log ("Done!");
				return retracePath(cur);
			}
			for (int i = 0; i < cur.adjacencies.Length; i++){
				if (!cur.adjacencies[i].visited) {
					cur.adjacencies[i].visited =  true;
					cur.adjacencies[i].prevPlace = cur;
					BFSQueue.Enqueue (cur.adjacencies[i]);
					Debug.Log ("  Neighbor of current place (being enqueued now): "+cur.adjacencies[i].placeName);
				}
			}
		}

		Debug.Log ("NO PATH FOUND - ERROR");
		return null;
	}

	List<Place> retracePath(Place cur) {
		Debug.Log ("Entering retrace path.");
		List<Place> pathList = new List<Place> ();
		Debug.Log("Adding "+cur.placeName+" to the retrace path queue.");
		pathList.Add (cur);
		while (cur.prevPlace != null) {
			countMAX++;
			if (countMAX == 1000)
				return null;

			cur = cur.prevPlace;
			Debug.Log("Adding "+cur.placeName+" to the retrace path queue.");
			pathList.Add (cur);
		}

		pathList.Reverse ();
		return pathList;
	}

	void resetPlaces(){
		for(int i = 0; i < placeList.Length; i++){
			placeList[i].Reset();
		}
	}
}
