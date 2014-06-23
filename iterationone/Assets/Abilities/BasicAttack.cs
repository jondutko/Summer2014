using UnityEngine;
using System.Collections;

public class BasicAttack : Ability{
	
	void Start() {
		abilName = "Basic Attack (Melee)";
		abilCost = 0;
		manaCost = 0;
		range = 1;
		aoe = 0;
		abilScaling = new AbilityScaling(1, 0);
	}
	
	
	public override void executeAbility(CombatBoard board, CombatCharacter currentFighter, SquareIcon targetSquare) {
		
	}
	
	
	public override string abilitySummary(Character curFighter) {
		return "Deals "+(abilScaling.physicalScaling*curFighter.stats[StatTextures.Stat.AD])+ " physical damage to an adjacent unit.";
	}
	
	public override string abilitySummary(CombatCharacter curFighter) {
		return "Deals "+(abilScaling.physicalScaling*curFighter.ad)+ " physical damage to an adjacent unit.";
	}
	
	public override string ToString() {
		return abilName;
	}
	
}
