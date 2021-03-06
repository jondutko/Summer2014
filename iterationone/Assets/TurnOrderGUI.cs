﻿using UnityEngine;
using System.Collections;

public class TurnOrderGUI : MonoBehaviour {
	
	public bool isDrawing;
	private Rect windowRect = new Rect(20, 20, 175, 250), secondWindowRect = new Rect(20, 20, 175, 500), charWindowRect = new Rect(5, 400, 200, 200);
	private int uniqueID = 523, uniqueID2 = 167, charID = 883;
	public CombatCharacter currentFighter;
	public TurnOrder turnOrder;
	private Vector2 scrollPosition = Vector2.zero;
	public int curSelection;
	private string lockedTooltip;
	
	public bool displayingChar = false;
	public CombatCharacter displayCombChar;
	public StatTextures iconPackage;
	
	void OnGUI(){
		GUI.skin.button.normal.textColor = Color.white;
		GUI.skin.button.hover.textColor = Color.white;
		if(isDrawing){
			if(turnOrder.attack == TurnOrder.PhaseState.InProcess || turnOrder.attack == TurnOrder.PhaseState.ChooseRange) {
				secondWindowRect = GUI.ModalWindow(uniqueID2, secondWindowRect, DoMyWindowAbilities, new GUIContent("Actions for " + currentFighter.combatName));
			}
			else {
				curSelection = -1;
				windowRect = GUI.Window(uniqueID, windowRect, DoMyWindow, currentFighter.combatName +"'s Turn");
			}
		}
		if(displayingChar){
			charWindowRect = GUI.Window(charID, charWindowRect, DoMyCharWindow, displayCombChar.combatName);
		}
	}
	
	void DoMyCharWindow(int windowID){
		int vOffset = 35;
		GUI.Label (new Rect (10, 20, 30, 30), iconPackage.hp);
		GUI.Label (new Rect (45, 23, 50, 30), displayCombChar.curHealth + "/" +displayCombChar.maxHealth);
		// mana
		GUI.Label (new Rect (110, 20, 30, 30), iconPackage.mana);
		GUI.Label (new Rect (145, 23, 50, 30), displayCombChar.curMana +"/"+displayCombChar.maxMana);
		
		// ad
		GUI.Label (new Rect (10, 20 + vOffset, 30, 30), iconPackage.ad);
		GUI.Label (new Rect (45, 23 + vOffset, 50, 30), displayCombChar.ad.ToString());
		
		// ap
		GUI.Label (new Rect (110, 20 + vOffset, 30, 30), iconPackage.ap);
		GUI.Label (new Rect (145, 23 + vOffset, 50, 30), displayCombChar.ap.ToString());
		
		// armor
		GUI.Label (new Rect (10, 20 + 2*vOffset, 30, 30), iconPackage.armor);
		GUI.Label (new Rect (45, 23 + 2*vOffset, 50, 30), displayCombChar.armor.ToString());
		
		// mr
		GUI.Label (new Rect (110, 20 + 2*vOffset, 30, 30), iconPackage.mr);
		GUI.Label (new Rect (145, 23 + 2*vOffset, 50, 30), displayCombChar.mr.ToString());
		
		// speed
		GUI.Label (new Rect (10, 20 + 3*vOffset, 30, 30), iconPackage.speed);
		GUI.Label (new Rect (45, 23 + 3*vOffset, 50, 30), displayCombChar.speed.ToString());
		
		if(GUI.Button (new Rect(10, 20 + 4*vOffset, 50, 30), "Close")){
			displayingChar = false;
			displayCombChar = null;
		}
	}
	
	void DoMyWindow(int windowID) {	
		if((turnOrder.move == TurnOrder.PhaseState.Completed) && (turnOrder.attack == TurnOrder.PhaseState.Completed)) {
			turnOrder.endTurn();
			turnOrder.StartTurn ();
		}
		else if(turnOrder.move == TurnOrder.PhaseState.InProcess || turnOrder.attack == TurnOrder.PhaseState.InProcess){
			if(GUI.Button(new Rect(5, 20, 165, 30), "Cancel")){
				turnOrder.gameBoard.clearHighlightedSquares();
				turnOrder.gameBoard.board[currentFighter.row, currentFighter.col].highlightForActive();
				if (turnOrder.move == TurnOrder.PhaseState.InProcess){
					turnOrder.move = TurnOrder.PhaseState.Uncompleted;
				}
				if (turnOrder.attack == TurnOrder.PhaseState.InProcess) {
					turnOrder.attack = TurnOrder.PhaseState.ChooseRange;
				}
			}
		}
		else {
			if(turnOrder.move == TurnOrder.PhaseState.Uncompleted){
				if(GUI.Button(new Rect(5, 20, 165, 30), "Move"))
					turnOrder.movePhase();
			}
			else if(turnOrder.move == TurnOrder.PhaseState.InProcess){
				GUI.Label(new Rect(5, 20, 165, 30), "You are moving");
			}
			else
				GUI.Label(new Rect(5, 20, 165, 30), "Already moved!");
			if(turnOrder.attack == TurnOrder.PhaseState.Uncompleted) {
				if(GUI.Button(new Rect(5, 55, 165, 30), "Action"))
					turnOrder.attack = TurnOrder.PhaseState.InProcess;
			}
			else if(turnOrder.attack == TurnOrder.PhaseState.InProcess){
				GUI.enabled = false;
				GUI.Button(new Rect(5, 55, 165, 30), "Action");
				GUI.enabled = true;
			}
			else{
				GUI.enabled = false;
				GUI.Button(new Rect(5, 55, 165, 30), "Action");
				GUI.enabled = true;
			}
			if(GUI.Button(new Rect(5, 90, 165, 30), "Wait")){
				turnOrder.move = TurnOrder.PhaseState.Completed;
				turnOrder.attack = TurnOrder.PhaseState.Completed;
			}
		}

		GUI.DragWindow(new Rect(0, 0, 10000, 10000));
	}
	
	void DoMyWindowAbilities(int windowID) {
		bool showing = true;
		int numAbilities = currentFighter.abilityList.Count;
		scrollPosition = GUI.BeginScrollView(new Rect(5, 25, 165, 250), scrollPosition, new Rect(0, 0, 140, 30*numAbilities));
		for(int i = 0; i < numAbilities; i++){
			GUIStyle guiStyle = new GUIStyle();
			
			if(i == curSelection) {
				GUI.skin.button.normal.textColor = Color.yellow;
				GUI.skin.button.hover.textColor = Color.yellow;
			}
			else {
				GUI.skin.button.normal.textColor = Color.white;
				GUI.skin.button.hover.textColor = Color.white;
			}
			
			GUIContent guiContent = new GUIContent(currentFighter.abilityList[i].ToString(), currentFighter.abilityList[i].abilitySummary(currentFighter));
			if(GUI.Button(new Rect(0, 30*i, 150, 25), guiContent)) {
				lockedTooltip = currentFighter.abilityList[i].abilitySummary(currentFighter);
				turnOrder.attack = TurnOrder.PhaseState.ChooseRange;
				turnOrder.gameBoard.clearHighlightedSquares();
				turnOrder.gameBoard.highlightAttackSquares(currentFighter, currentFighter.abilityList[i].range);
				curSelection = i;
			}
			GUI.skin.button.normal.textColor = Color.white;
			GUI.skin.button.hover.textColor = Color.white;
		}
		
		GUI.EndScrollView();
		if(curSelection == -1)
			GUI.TextArea(new Rect(5, 260, 165, 190), GUI.tooltip);
		else
			GUI.TextArea(new Rect(5, 260, 165, 190), lockedTooltip);
		if(GUI.Button (new Rect(5, 460, 75, 30), "Back")) {
			curSelection = -1;
			turnOrder.attack = TurnOrder.PhaseState.Uncompleted;
			turnOrder.gameBoard.clearHighlightedSquares();
		}
		
		
	}
	
	void nothing() {}
}
