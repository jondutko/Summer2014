using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {

	public Dictionary<string, int> stats = new Dictionary<string, int>();
	public string charName;
	public Job jobObject;
	public Texture portrait;
	public Texture icon;
	public int currentLevel, currentXP;
	public int nextLevelXP = 100;
	public int xpLvlDiff = 30;

	public void initializeCharacter(int lvl, int health, int mana, int strength, int wisdom, int physRes, int magRes, int speed){
		stats.Add ("Health", health);
		stats.Add ("Mana", mana);
		stats.Add ("Strength", strength);
		stats.Add ("Wisdom", wisdom);
		stats.Add ("Physical Resistance", physRes);
		stats.Add ("Magical Resistance", magRes);
		stats.Add ("Speed", speed);
		initializeLevel (lvl);
	}

	public void addXP(int xp){
		currentXP += xp;
		while (currentXP >= nextLevelXP) {
			currentXP -= nextLevelXP;
			LevelUp ();
		}
	}
	
	void LevelUp(){
		currentLevel++;
		nextLevelXP += xpLvlDiff;
		//this is where shit goes down
		//fuckboi industreez
	}

	public void initializeLevel(int lvl) {
		currentLevel = 1;
		for(int i = 1; i < lvl; i++)
			LevelUp();
		currentXP = 0;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
