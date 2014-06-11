using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Equipment : MonoBehaviour {

	public string equipName;
	public Texture image;
	public int healthModifier;
	public int manaModifier;
	public int ADModifier;
	public int APModifier;
	public int armorModifier;
	public int MRModifier;
	public int speedModifier;
	
	public string statsToString() {
		string msg = "";
		if (healthModifier > 0)
			msg += "+" + healthModifier.ToString () + " health, ";
		else if (healthModifier < 0)
			msg += "-" + healthModifier.ToString () + " health, ";
		
		if (manaModifier > 0)
			msg += "+" + manaModifier.ToString () + " mana, ";
		else if (manaModifier < 0)
			msg += "-" + manaModifier.ToString () + " mana, ";
		
		if (ADModifier > 0)
			msg += "+" + ADModifier.ToString () + " strength, ";
		else if (ADModifier < 0)
			msg += "-" + ADModifier.ToString () + " strength, ";
		
		if (APModifier > 0)
			msg += "+" + APModifier.ToString () + " wisdom, ";
		else if (APModifier < 0)
			msg += "-" + APModifier.ToString () + " wisdom, ";
		
		if (armorModifier > 0)
			msg += "+" + armorModifier.ToString () + " armor, ";
		else if (armorModifier < 0)
			msg += "-" + armorModifier.ToString () + " armor, ";
		
		if (MRModifier > 0)
			msg += "+" + MRModifier.ToString () + " resistance, ";
		else if (MRModifier < 0)
			msg += "-" + MRModifier.ToString () + " resistance, ";
			
		if (speedModifier > 0)
			msg += "+" + speedModifier.ToString () + " speed, ";
		else if (speedModifier < 0)
			msg += "-" + speedModifier.ToString () + " speed, ";
			
		msg = msg.Substring (0, msg.Length-2);
		return msg;
		
		
	
	}
}
