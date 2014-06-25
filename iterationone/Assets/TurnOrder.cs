using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnOrder : MonoBehaviour {

	public Queue<CombatCharacter> fighters = new Queue<CombatCharacter>();
	public CombatListener combatListener;
	public CombatBoard gameBoard;
	public MacroState gameState;
	public PhaseState move, attack;
	public int numFighters;
	public CombatCharacter currentFighter;
	private Color moveColor, attackColor;
	public TurnOrderGUI turnOrderGui;
	public Ability chosenAbility;
	
	public enum MacroState{
		Placement,
		Combat,
		Victory,
		Defeat
	};
	
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
	
	
	public void StartTurn() {
		currentFighter = fighters.Dequeue();
		gameBoard.board[currentFighter.row, currentFighter.col].highlightForActive();
		turnOrderGui.currentFighter = currentFighter;
		turnOrderGui.isDrawing = true;
		move = PhaseState.Uncompleted;
		attack = PhaseState.Uncompleted;
	}
	
	
	public void squareClicked(int row, int col){
		if(gameState == MacroState.Placement){
			gameBoard.setFighterLocation(gameBoard.toBePlaced.Dequeue(), row, col);
			if(gameBoard.toBePlaced.Count == 0){
				gameState = MacroState.Combat;
				StartTurn();
			}
		}
		else if(gameState == MacroState.Combat){
			if(move == PhaseState.InProcess) {
				if(gameBoard.board[row, col].highlightedForMove){
					gameBoard.setFighterLocation(currentFighter, row, col);
					move = PhaseState.Completed;
					gameBoard.clearHighlightedSquares();
				}
			}
			else if (attack == PhaseState.ChooseRange) {
				if(gameBoard.board[row, col].highlightedForAttack || gameBoard.board[row, col].highlightedForTarget){
					Debug.Log ("Attacked Row: " + row + ", and col: " + col);
					currentFighter.abilityList[turnOrderGui.curSelection].executeAbility(gameBoard, currentFighter, gameBoard.board[row,col]);
					attack = PhaseState.Completed;
					gameBoard.clearHighlightedSquares();
				}
			}
			gameBoard.board[currentFighter.row, currentFighter.col].highlightForActive();
		}
	}
	
	public void endTurn(){
		gameBoard.clearHighlightedSquares();
		fighters.Enqueue(currentFighter);
	}
	
	public CombatCharacter addFighter(CombatCharacter fighter, int row, int col) {
		gameBoard.setFighterLocation(fighter, row, col);
		fighters.Enqueue (fighter);
		return fighter;
	}
}
