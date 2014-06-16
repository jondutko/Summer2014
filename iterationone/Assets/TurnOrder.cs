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
	
	public enum PhaseState {
		Uncompleted,
		InProcess,
		Completed
	};
	
	public void movePhase() {
		gameBoard.highlightMoveSquares(currentFighter);
		move = PhaseState.InProcess;
	}
	
	public void attackPhase() {
		attack = PhaseState.InProcess;
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
	
	public void addFighter(CombatCharacter fighter, int row, int col) {
		CombatCharacter newFighter = Instantiate (fighter, gameBoard.board[row, col].transform.position, Quaternion.identity) as CombatCharacter;
		gameBoard.setFighterLocation(newFighter, row, col);
		fighters.Enqueue (newFighter);
	}
}
