using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Clan : MonoBehaviour {

	public List<Character> clanMembers = new List<Character>();
	public string clanName;
	public List<Quest> currentQuests = new List<Quest>();
	public List<Quest> completedQuests = new List<Quest>();
	//public List<Equipment> inventory = new List<Equipment>();

	// Use this for initialization
	void Start () {


	}

	public void initializeClan() {
		foreach (Transform child in transform) {
			Character curChar = child.GetComponent<Character> ();
			curChar.initializeCharacter(1, 100, 80, 30, 25, 30, 28, 6);
			clanMembers.Add (curChar);
		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
