using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fireball : Ability{

	void Start() {
		abilName = "Fireball";
		abilCost = 100;
		manaCost = 25;
		range = 3;
		aoe = 1;
		baseMagDmg = 20;
		basePhysDmg = 0;
		abilScaling = new AbilityScaling(1.5f, 0);
	}
	
	
	public override void executeAbility(CombatBoard board, CombatCharacter currentFighter, SquareIcon targetSquare) {
		List<SquareIcon> attackSquares = board.getNeighbors(targetSquare);
		foreach(SquareIcon sq in attackSquares) {
			if (sq.curChar != null) {
				Debug.Log ("Raw magic dmg: " + baseMagDmg + ", and scaling raw: " + abilScaling.magicScaling * currentFighter.ap);
				Debug.Log ("Raw physical dmg: " + basePhysDmg + ", and scaling raw: " + abilScaling.physicalScaling * currentFighter.ad);
				sq.curChar.takeMagicalDamage ((float) baseMagDmg + abilScaling.magicScaling * currentFighter.ap);
				sq.curChar.takePhysicalDamage ((float) basePhysDmg + abilScaling.physicalScaling * currentFighter.ad);
			}
		}
	
	
		Debug.Log ("Fireballed");
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
