using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnOrder : MonoBehaviour {

	private Queue<CombatCharacter> fighters = new Queue<CombatCharacter>();
	public CombatListener combatListener;
	public CombatBoard gameBoard;
	public PhaseState move, attack;
	public int numFighters;
	public CombatCharacter currentFighter;
	private Color moveColor, attackColor;
	public TurnOrderGUI turnOrderGui;
	public Ability chosenAbility;
	
	public enum PhaseState {
		Uncompleted,
		ChooseRange,
		InProcess,
		Completed
	};
	
	public void movePhase() {
		Debug.Log ("Moving");
		Debug.Log (currentFighter.speed);
		gameBoard.highlightMoveSquares(currentFighter);
		move = PhaseState.InProcess;
	}
	
	public void attackPhase() {
		attack = PhaseState.ChooseRange;
	}
	
	public void StartTurn() {
		currentFighter = fighters.Dequeue();
		gameBoard.board[currentFighter.row, currentFighter.col].highlightForActive();
		turnOrderGui.currentFighter = currentFighter;
		turnOrderGui.isDrawing = true;
		move = PhaseState.Uncompleted;
		attack = PhaseState.Uncompleted;
	}
	
	public void squareClicked(int row, int col){
		if(move == PhaseState.InProcess) {
			gameBoard.setFighterLocation(currentFighter, row, col);
			move = PhaseState.Completed;
			gameBoard.clearHighlightedSquares();
			gameBoard.board[currentFighter.row, currentFighter.col].highlightForActive();
		}
		else if (attack == PhaseState.InProcess) {
			Debug.Log ("Attacked Row: " + row + ", and col: " + col);
			attack = PhaseState.Completed;
			gameBoard.clearHighlightedSquares();
		}
	}
	
	public void endTurn(){
		gameBoard.clearHighlightedSquares();
		fighters.Enqueue(currentFighter);
	}
	
	public CombatCharacter addFighter(CombatCharacter fighter, int row, int col) {
		CombatCharacter newFighter = Instantiate (fighter, gameBoard.board[row, col].transform.position, Quaternion.identity) as CombatCharacter;
		gameBoard.setFighterLocation(newFighter, row, col);
		fighters.Enqueue (newFighter);
		return newFighter;
	}
}
