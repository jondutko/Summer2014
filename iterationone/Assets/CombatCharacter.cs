using UnityEngine;
using System.Collections;

public class CombatCharacter : CombatAsset {

	public int startingrow, startingcol;
	public Sprite display;

	// Use this for initialization
	void Start () {
		row = startingrow;
		col = startingcol;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Click() {
		Debug.Log (display.ToString ());
	}
}
