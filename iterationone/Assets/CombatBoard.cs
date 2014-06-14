using UnityEngine;
using System.Collections;

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
		turnOrder.addFighter(gob, gobRow, gobCol);
		turnOrder.StartTurn();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	public void setFighterLocation(CombatCharacter fighter, int r, int c){
		fighter.row = r;
		fighter.col = c;
		board[r, c].curChar = fighter;
		fighter.transform.position = board[r, c].transform.position;
	}
}
