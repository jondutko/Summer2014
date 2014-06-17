using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ability : MonoBehaviour {
	public string abilName;
	public int abilCost;
	public int manaCost;
	public int range;
	public int aoe;
	public AbilityScaling abilScaling;
	
	public virtual string abilitySummary(Character curFighter) {
		return null;
	}
	
	public virtual string abilitySummary(CombatCharacter curFighter) {
		return null;
	}
	public virtual void executeAbility(CombatBoard board, CombatCharacter currentFighter, SquareIcon targetSquare) {}
}
