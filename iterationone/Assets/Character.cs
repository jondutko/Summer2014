using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {

	public Dictionary<StatTextures.Stat, int> baseStats;
	public Dictionary<StatTextures.Stat, int> equipStats;
	public Dictionary<StatTextures.Stat, int> stats;
	public Clan clan;
	public string charName;
	public Job jobObject;
	public Sprite portrait;
	public Sprite icon;
	public int currentLevel, currentXP;
	public int nextLevelXP;
	public int xpLvlDiff;
	public Equipment[] equipList;
	public int numItemSlots = 6;

	public void initializeCharacter(int lvl, int health, int mana, int strength, int wisdom, int physRes, int magRes, int speed){
		baseStats = new Dictionary<StatTextures.Stat, int>();
		stats = new Dictionary<StatTextures.Stat, int>();
		equipStats = new Dictionary<StatTextures.Stat, int>();
		baseStats.Add (StatTextures.Stat.Health, health);
		baseStats.Add (StatTextures.Stat.Mana, mana);
		baseStats.Add (StatTextures.Stat.AD, strength);
		baseStats.Add (StatTextures.Stat.AP, wisdom);
		baseStats.Add (StatTextures.Stat.Armor, physRes);
		baseStats.Add (StatTextures.Stat.MR, magRes);
		baseStats.Add (StatTextures.Stat.Speed, speed);
		stats.Add (StatTextures.Stat.Health, 0);
		stats.Add (StatTextures.Stat.Mana, 0);
		stats.Add (StatTextures.Stat.AD, 0);
		stats.Add (StatTextures.Stat.AP, 0);
		stats.Add (StatTextures.Stat.Armor, 0);
		stats.Add (StatTextures.Stat.MR, 0);
		stats.Add (StatTextures.Stat.Speed, 0);
		equipStats.Add (StatTextures.Stat.Health, 0);
		equipStats.Add (StatTextures.Stat.Mana, 0);
		equipStats.Add (StatTextures.Stat.AD, 0);
		equipStats.Add (StatTextures.Stat.AP, 0);
		equipStats.Add (StatTextures.Stat.Armor, 0);
		equipStats.Add (StatTextures.Stat.MR, 0);
		equipStats.Add (StatTextures.Stat.Speed, 0);
		nextLevelXP = 100;
		xpLvlDiff = 30;
		InitializeLevel (lvl);
		updateStats ();
		if (equipList == null)
			equipList = new Equipment[numItemSlots];
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
	
	public void updateStats() {
		equipStats[StatTextures.Stat.Health] = 0;
		equipStats[StatTextures.Stat.Mana] = 0;
		equipStats[StatTextures.Stat.AD] = 0;
		equipStats[StatTextures.Stat.AP] = 0;
		equipStats[StatTextures.Stat.Armor] = 0;
		equipStats[StatTextures.Stat.MR] = 0;
		equipStats[StatTextures.Stat.Speed] = 0;
	
		for(int i = 0; i < numItemSlots; i++) {
			if (equipList[i] != null) {
				equipStats[StatTextures.Stat.Health] = equipStats[StatTextures.Stat.Health] + equipList[i].healthModifier;
				equipStats[StatTextures.Stat.Mana] = equipStats[StatTextures.Stat.Mana] + equipList[i].manaModifier;
				equipStats[StatTextures.Stat.AD] = equipStats[StatTextures.Stat.AD] + equipList[i].ADModifier;
				equipStats[StatTextures.Stat.AP] = equipStats[StatTextures.Stat.AP] + equipList[i].APModifier;
				equipStats[StatTextures.Stat.Armor] = equipStats[StatTextures.Stat.Armor] + equipList[i].armorModifier;
				equipStats[StatTextures.Stat.MR] = equipStats[StatTextures.Stat.MR] + equipList[i].MRModifier;
				equipStats[StatTextures.Stat.Speed] = equipStats[StatTextures.Stat.Speed] + equipList[i].speedModifier;
			}
		}
		
		foreach (KeyValuePair<StatTextures.Stat, int> pair in baseStats)
			stats[pair.Key] = baseStats[pair.Key] + equipStats[pair.Key];
	
	}
	
	public string getEquipModifier(StatTextures.Stat stat) {
		string str;
		if (equipStats[stat] >= 0)
			str = "+"+equipStats[stat];
		else
			str = "-"+equipStats[stat];
		return str;
	
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
