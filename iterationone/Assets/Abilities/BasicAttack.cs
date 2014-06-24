using UnityEngine;
using System.Collections;

public class BasicAttack : Ability{
	
	void Start() {
		abilName = "Basic Attack (Melee)";
		abilCost = 0;
		manaCost = 0;
		range = 1;
		aoe = 0;
		abilScaling = new AbilityScaling(0, 1);
	}
	
	
	public override void executeAbility(CombatBoard board, CombatCharacter currentFighter, SquareIcon targetSquare) {
		if (targetSquare.curChar != null) {
			Debug.Log ("Raw magic dmg: " + baseMagDmg + ", and scaling raw: " + abilScaling.magicScaling * currentFighter.ap);
			Debug.Log ("Raw physical dmg: " + basePhysDmg + ", and scaling raw: " + abilScaling.physicalScaling * currentFighter.ad);
			targetSquare.curChar.takeMagicalDamage ((float) baseMagDmg + abilScaling.magicScaling * currentFighter.ap);
			targetSquare.curChar.takePhysicalDamage ((float) basePhysDmg + abilScaling.physicalScaling * currentFighter.ad);
		}
		
		
		Debug.Log ("Basic Melee Attack");
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
