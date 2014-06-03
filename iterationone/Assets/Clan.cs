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
		foreach (Transform child in transform)
			clanMembers.Add(child.GetComponent<Character>());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
