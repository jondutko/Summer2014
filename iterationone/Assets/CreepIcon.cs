using UnityEngine;
using System.Collections;

public class CreepIcon : Icon
{

	public int uniqueID;
	public StatTextures iconPackage;
	bool isDrawing = false;
	public Rect windowRect;
	int width = 210;
	int height = 300;
	int vOffset = 35;

	public override void onClick() {
		GameObject gameObj = GameObject.Find ("MainCamera");
		TurnOrderGUI turnOrderGUI = gameObj.GetComponent<TurnOrderGUI>();
		
		turnOrderGUI.displayingChar = true;
		turnOrderGUI.displayCombChar = combatAssChar;
	
	}
	
}

