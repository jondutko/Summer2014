using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {

	public Dictionary<string, int> stats = new Dictionary<string, int>();
	public string charName;
	public string jobName;
	public Job jobObject;
	public int curLevel = 1;
	public Level lvlObject;
	public Texture portrait;
	public Texture icon;

	public Character(string newName){
		charName = newName;
		stats.Add ("Health", 100);
		stats.Add ("Mana", 80);
		stats.Add ("Strength", 30);
		stats.Add ("Wisdom", 25);
		stats.Add ("Physical Resistance", 20);
		stats.Add ("Magical Resistance", 20);
		stats.Add ("Speed", 6);
		lvlObject = new Level (curLevel);
		jobObject = new Job (jobName);
	}

	public Character(string newName, int health, int mana, int strength, int wisdom, int physRes, int magRes, int speed){
		charName = newName;
		stats.Add ("Health", health);
		stats.Add ("Mana", mana);
		stats.Add ("Strength", strength);
		stats.Add ("Wisdom", wisdom);
		stats.Add ("Physical Resistance", physRes);
		stats.Add ("Magical Resistance", magRes);
		stats.Add ("Speed", speed);
		lvlObject = new Level (curLevel);
		jobObject = new Job (jobName);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
