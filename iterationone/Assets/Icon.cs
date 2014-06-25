using UnityEngine;
using System.Collections;

public class Icon : MonoBehaviour{

	public Sprite icon;
	public CombatCharacter combatAssChar;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public virtual void onClick() {
		Debug.Log ("Error - Icon's onClick() method should always be overloaded");
	}
}
