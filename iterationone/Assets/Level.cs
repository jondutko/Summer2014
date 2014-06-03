using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	public int currentLevel, currentXP;
	public int nextLevelXP = 100;
	public int xpLvlDiff = 30;

	public Level(){
		currentLevel = 1;
		currentXP = 0;
	}

	public Level(int lvl){
		currentLevel = 1;
		for(int i = 1; i < lvl; i++)
			LevelUp();
		currentXP = 0;
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
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
