using UnityEngine;
using System.Collections;

public class TurnOrderGUI : MonoBehaviour {
	
	public bool isDrawing;
	private Rect windowRect = new Rect(20, 20, 175, 250);
	private int uniqueID = 523;
	public CombatCharacter currentFighter;
	public TurnOrder turnOrder;
	
	void OnGUI(){
		if(isDrawing){
			windowRect = GUI.Window(uniqueID, windowRect, DoMyWindow, currentFighter.combatName +"'s Turn");
		}
	}
	
	void DoMyWindow(int windowID) {	
		if((turnOrder.move == TurnOrder.PhaseState.Completed) && (turnOrder.attack == TurnOrder.PhaseState.Completed)) {
			turnOrder.endTurn();
			turnOrder.StartTurn ();
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
				if(GUI.Button(new Rect(5, 55, 165, 30), "Attack"))
					turnOrder.attackPhase();
			}
			else if(turnOrder.attack == TurnOrder.PhaseState.InProcess){
				GUI.Label(new Rect(5, 55, 165, 30), "You are attacking!");
			}
			else
				GUI.Label(new Rect(5, 55, 165, 30), "Already attacked!");
			if(GUI.Button(new Rect(5, 90, 165, 30), "Wait")){
				turnOrder.move = TurnOrder.PhaseState.Completed;
				turnOrder.attack = TurnOrder.PhaseState.Completed;
			}
		}

		GUI.DragWindow(new Rect(0, 0, 10000, 10000));
	}
}
