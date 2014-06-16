using UnityEngine;
using System.Collections;

public class SquareIcon : Icon {

	public TurnOrder turnOrder;
	public CombatCharacter curChar;
	public int row;
	public int col;
	public bool highlightedForMove;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(curChar != null){
			
		}
	}
	
	public override void onClick() {
		turnOrder.squareClicked(row, col);
	}
	
	public void highlightForMove() {
		Color moveColor;
		moveColor.r = 0.98f;
		moveColor.g = 0.87f;
		moveColor.b = 0.87f;
		moveColor.a = 1;
		this.GetComponent<SpriteRenderer>().color = moveColor;
		highlightedForMove = true;	
	}
	
	public void highlightForActive() {
		Color moveColor = Color.yellow;
		this.GetComponent<SpriteRenderer>().color = moveColor;
	
	}
	
	
	public void unhighlight() {
		Color moveColor;
		moveColor.r = 1f;
		moveColor.g = 1f;
		moveColor.b = 1f;
		moveColor.a = 1;
		this.GetComponent<SpriteRenderer>().color = moveColor;
		highlightedForMove = false;	
	}
	
	
	
	public bool canPass() {
		return ((curChar == null) || (curChar.isPassable));
	}
	
}
