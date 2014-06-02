using UnityEngine;
using System.Collections;

public class Place : MonoBehaviour {

	public string placeName;
	public Place[] adjacencies;

	public Place prevPlace = null;
	public bool visited = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Reset(){
		prevPlace = null;
		visited = false;
	}
}
