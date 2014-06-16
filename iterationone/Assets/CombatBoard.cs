using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CombatBoard : MonoBehaviour {
	
	public CombatAssets library;
	public SquareIcon[,] board;
	public SquareIcon sq;
	public int width = 12;
	public int height = 8;
	public TurnOrder turnOrder;
	
	// Use this for initialization
	void Start () {
		float scalar = Mathf.Max (height / 6f, width/12f);
		sq.transform.localScale = new Vector3(1/scalar, 1/scalar, 1);
		float sqWidth = sq.icon.textureRect.width;
		Vector3 sqExtents = sq.icon.bounds.extents;
		sqExtents.x = sqExtents.x * sq.transform.localScale.x;
		sqExtents.y = sqExtents.y * sq.transform.localScale.y;
		Debug.Log("Sq Width: "+sqWidth);
		Debug.Log("Sq Extents: "+sqExtents);
		Vector3 lowerLeft = new Vector3(0- (width-1) * sqExtents.x, 0-(height-1) * sqExtents.y, 0);
		board = new SquareIcon[height, width];
		for(int column = 0; column < width; column++){
			for (int row = 0; row < height; row++){
				board[row, column] = Instantiate(sq, new Vector3(lowerLeft.x + column*sqExtents.x*2, lowerLeft.y + row*sqExtents.y*2, 0), Quaternion.identity) as SquareIcon; 	
				board[row,column].row = row;
				board[row,column].col = column;
			}
		}
		
		
		
		CombatCharacter gob = (CombatCharacter) library.getAssetByName("goblin");
		int gobRow = 5;
		int gobCol = 7;
		Debug.Log ("TAYLOR");
		gob.combatName = "Hissafiss";
		turnOrder.addFighter(gob, gobRow, gobCol);
		
		CombatCharacter gob2 = (CombatCharacter) library.getAssetByName("goblin");
		int gob2Row = 4;
		int gob2Col = 10;
		gob2.combatName = "Faggotmancer";
		turnOrder.addFighter(gob2, gob2Row, gob2Col);
		
		CombatCharacter hero = (CombatCharacter) library.getAssetByName("hero");
		int heroRow = 3;
		int heroCol = 2;
		Debug.Log ("BRANDON");
		hero.combatName = "The Hero";
		turnOrder.addFighter(hero, heroRow, heroCol);
		
		
		turnOrder.StartTurn();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void highlightMoveSquares(CombatCharacter fighter) {
		int[,] squareDistances = new int[height, width];
		for(int i = 0; i < height; i++) {
			for(int j = 0; j < width; j++)
				squareDistances[i, j] = -1;
		}
		
		SquareIcon curSquare = board[fighter.row, fighter.col];
		Queue<SquareIcon> sqQueue = new Queue<SquareIcon>();
		sqQueue.Enqueue(curSquare);
		List<SquareIcon> neighborList;
		squareDistances[curSquare.row, curSquare.col] = 0;
		while(sqQueue.Count != 0) {
			curSquare = sqQueue.Dequeue();

			neighborList = getNeighbors(curSquare);
			foreach(SquareIcon sqIcon in neighborList) {
				if ((squareDistances[sqIcon.row, sqIcon.col] == -1) && (squareDistances[curSquare.row, curSquare.col] < fighter.speed) && (sqIcon.canPass ())) {
					squareDistances[sqIcon.row, sqIcon.col] = squareDistances[curSquare.row, curSquare.col] + 1;
					sqQueue.Enqueue (sqIcon);
				}
			}
		}
		
		for(int i = 0; i < height; i++) {
			for(int j = 0; j < width; j++)
				if ((squareDistances[i,j] != -1) && squareDistances[i,j] <= fighter.speed)
					board[i, j].highlightForMove();
		}
		board[fighter.row, fighter.col].highlightForActive();
	
	}
	
	public void clearHighlightedSquares() {
		for(int i = 0; i < height; i++) {
			for(int j = 0; j < width; j++)
				board[i, j].unhighlight();
		}
		
	}
	
	private List<SquareIcon> getNeighbors(SquareIcon curSquare) {
		List<SquareIcon> neighborList = new List<SquareIcon>();
		if((curSquare.row-1>=0) && (curSquare.row-1 < height) && (curSquare.col>0) && (curSquare.col < width))
			neighborList.Add (board[curSquare.row-1, curSquare.col]);
		if((curSquare.row+1>=0) && (curSquare.row+1 < height) && (curSquare.col>0) && (curSquare.col < width))
			neighborList.Add (board[curSquare.row+1, curSquare.col]);
		if((curSquare.row>=0) && (curSquare.row < height) && (curSquare.col-1>=0) && (curSquare.col-1 < width))
			neighborList.Add (board[curSquare.row, curSquare.col-1]);
		if((curSquare.row>=0) && (curSquare.row < height) && (curSquare.col+1>=0) && (curSquare.col+1 < width))
			neighborList.Add (board[curSquare.row, curSquare.col+1]);
		return neighborList;
	}
	
	
	public void setFighterLocation(CombatCharacter fighter, int r, int c){
		board[fighter.row, fighter.col].curChar = null;
		fighter.row = r;
		fighter.col = c;
		board[r, c].curChar = fighter;
		fighter.transform.position = board[r, c].transform.position;
	}
}
