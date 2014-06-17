using UnityEngine;
using System.Collections;

public class Fireball : Ability{

	void Start() {
		abilName = "Fireball";
		abilCost = 100;
		manaCost = 25;
		range = 3;
		aoe = 1;
		abilScaling = new AbilityScaling(1.5f, 0);
	}
	
	
	public override void executeAbility(CombatBoard board, CombatCharacter currentFighter, SquareIcon targetSquare) {
		
	}
	
	
	public override string abilitySummary(Character curFighter) {
		return "Deals "+(abilScaling.magicScaling*curFighter.stats[StatTextures.Stat.AP])+ " fire damage, with a small chance to inflict a BURN.";
	}
	
	public override string abilitySummary(CombatCharacter curFighter) {
		return "Deals "+(abilScaling.magicScaling*curFighter.ap)+ " fire damage, with a small chance to inflict a BURN.";
	}
	
	public override string ToString() {
		return abilName;
	}

}
