using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatCharacter : CombatAsset {

	public int speed, attackRange;
	public string combatName;
	public List<Ability> abilityList;
	public int ap, ad, armor, mr, curHealth, maxHealth, curMana, maxMana;
	private float A = 2139.0f;
	private float B = 46.25f;
	public int startRow, startCol;
	
	
	public void takePhysicalDamage(float dmg) {
		float damageProportion = A / Mathf.Pow(armor + B, 2.0f);
		Debug.Log ("damage proportion: " + damageProportion);
		int dmgToTake = Mathf.CeilToInt(damageProportion * dmg);
		curHealth =- dmgToTake;
		Debug.Log (combatName + " took " + dmgToTake + " physical damage");
	
	}
	
	public void takeMagicalDamage(float dmg) {
		float damageProportion = A / Mathf.Pow(mr + B, 2.0f);
		Debug.Log ("damage proportion: " + damageProportion);
		int dmgToTake = Mathf.CeilToInt(damageProportion * dmg);
		curHealth =- dmgToTake;
		Debug.Log (combatName + " took " + dmgToTake + "magical damage");
		
	}
}
