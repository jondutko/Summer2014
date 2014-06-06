using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {

	public Dictionary<string, int> stats;
	public string charName;
	public Job jobObject;
	public Sprite portrait;
	public Sprite icon;
	public int currentLevel, currentXP;
	public int nextLevelXP;
	public int xpLvlDiff;
	public Equipment[] equipList;

	public void initializeCharacter(int lvl, int health, int mana, int strength, int wisdom, int physRes, int magRes, int speed){
		stats = new Dictionary<string, int>();
		stats.Add ("Health", health);
		stats.Add ("Mana", mana);
		stats.Add ("Strength", strength);
		stats.Add ("Wisdom", wisdom);
		stats.Add ("Physical Resistance", physRes);
		stats.Add ("Magical Resistance", magRes);
		stats.Add ("Speed", speed);
		nextLevelXP = 100;
		xpLvlDiff = 30;
		InitializeLevel (lvl);
		equipList = new Equipment[6];
	}

	public void AddXP(int xp){
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

	public void InitializeLevel(int lvl) {
		currentLevel = 1;
		for(int i = 1; i < lvl; i++)
			LevelUp();
		currentXP = 0;
	}
	
	public void Equip(Equipment toEquip, int slot) {
		if(equipList[slot]==null)
			equipList[slot] = toEquip;
		else
			Debug.Log ("Character class dire straits");
	}
	
	public Equipment unEquip(int slot) {
		Equipment equipToReturn = equipList[slot];
		equipList[slot] = null;
		return equipToReturn;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
