using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnOrder : MonoBehaviour {

	private Queue<CombatCharacter> fighters = new Queue<CombatCharacter>();
	public CombatListener combatListener;
	public CombatBoard gameBoard;
	private PhaseState move, attack;
	public int numFighters;
	public CombatCharacter currentFighter;
	
	private enum PhaseState {
		Uncompleted,
		Completed,
		Passed
	};

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void StartTurn() {
		currentFighter = fighters.Dequeue();
		move = PhaseState.Uncompleted;
		attack = PhaseState.Uncompleted;
	}
	
	public void addFighter(CombatCharacter fighter, int row, int col) {
		CombatCharacter newFighter = Instantiate (fighter, gameBoard.board[row, col].transform.position, Quaternion.identity) as CombatCharacter;
		gameBoard.setFighterLocation(newFighter, row, col);
		fighters.Enqueue (newFighter);
		
	}
}
