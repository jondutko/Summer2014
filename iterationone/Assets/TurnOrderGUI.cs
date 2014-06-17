using UnityEngine;
using System.Collections;

public class TurnOrderGUI : MonoBehaviour {
	
	public bool isDrawing;
	private Rect windowRect = new Rect(20, 20, 175, 250), secondWindowRect = new Rect(800, 100, 150, 300);
	private int uniqueID = 523, uniqueID2 = 167;
	public CombatCharacter currentFighter;
	public TurnOrder turnOrder;
	
	void OnGUI(){
		if(isDrawing){
			if(turnOrder.attack == TurnOrder.PhaseState.ChooseRange) {
				secondWindowRect = GUI.ModalWindow(uniqueID2, secondWindowRect, DoMyWindowAbilities, new GUIContent("Actions for " + currentFighter.combatName));
			}
			else
				windowRect = GUI.Window(uniqueID, windowRect, DoMyWindow, currentFighter.combatName +"'s Turn");
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
					turnOrder.attackPhase();
			}
			else if(turnOrder.attack == TurnOrder.PhaseState.InProcess){
				GUI.Label(new Rect(5, 55, 165, 30), "Performing action!");
			}
			else
				GUI.Label(new Rect(5, 55, 165, 30), "Action performed");
			if(GUI.Button(new Rect(5, 90, 165, 30), "Wait")){
				turnOrder.move = TurnOrder.PhaseState.Completed;
				turnOrder.attack = TurnOrder.PhaseState.Completed;
			}
		}

		GUI.DragWindow(new Rect(0, 0, 10000, 10000));
	}
	
	void DoMyWindowAbilities(int windowID) {
		int selection = -1;
		bool showing = true;
		
		GUIContent guiContent = new GUIContent();
		GUIStyle listStyle = new GUIStyle();
		listStyle.normal.textColor = Color.white;
		Texture2D tex = new Texture2D(2, 2);
		Color[] colors = new Color[4];
		for(int i = 0; i < colors.Length; i++) colors[i] = Color.white;
		tex.SetPixels(colors);
		tex.Apply();
		listStyle.hover.background = tex;
		listStyle.onHover.background = tex;
		listStyle.padding.left = listStyle.padding.right = listStyle.padding.top = listStyle.padding.bottom = 4;
		
		if(Popup.List(new Rect(10, 30, 200, 200), ref showing, ref selection, guiContent, currentFighter.abilityList.ToArray (), listStyle, nothing)) {
			if(selection!=-1) {
				turnOrder.chosenAbility = currentFighter.abilityList[selection];
				turnOrder.gameBoard.highlightAttackSquares(currentFighter, turnOrder.chosenAbility.range);
				turnOrder.attack = TurnOrder.PhaseState.InProcess;
				Debug.Log (selection);
			}
		}
		
		
		
	}
	
	void nothing() {}
}
