using UnityEngine;
using System.Collections;

public class TurnOrderGUI : MonoBehaviour {
	
	public bool isDrawing;
	private Rect windowRect = new Rect(20, 20, 175, 250), secondWindowRect = new Rect(20, 20, 175, 500);
	private int uniqueID = 523, uniqueID2 = 167;
	public CombatCharacter currentFighter;
	public TurnOrder turnOrder;
	private Vector2 scrollPosition = Vector2.zero;
	public int curSelection;
	private string lockedTooltip;
	
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
