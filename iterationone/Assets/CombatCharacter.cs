using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatCharacter : CombatAsset {

	public int speed, attackRange;
	public string combatName;
	public List<Ability> abilityList;
	public int ap, ad, armor, mr, curHealth, maxHealth, curMana, maxMana;
	
}
