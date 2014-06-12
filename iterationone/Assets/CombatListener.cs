using UnityEngine;
using System.Collections;

public class CombatListener : MonoBehaviour {

	public CombatAssets library;
	public SquareIcon[,] board;
	public CombatCharacter[] fighters;
	public Camera cam;
	private int currentFighter;
	public SquareIcon sq;
	public int width = 12;
	public int height = 8;

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
		
		fighters = new CombatCharacter[1];
		currentFighter = 0;
		CombatCharacter gob = (CombatCharacter) library.getAssetByName("goblin");
		int gobRow = 5;
		int gobCol = 7;
		currentFighter = 0;
		fighters[currentFighter] = Instantiate (gob, board[gobRow, gobCol].transform.position, Quaternion.identity) as CombatCharacter;
		setFighterLocation(currentFighter, gobRow, gobCol);

		

	}
	// Update is called once per frame
	void Update () {
		MouseHandler ();
	}
	
	void setFighterLocation(int index, int r, int c){
		fighters[index].row = r;
		fighters[index].col = c;
		board[r, c].curChar = fighters[index];
		fighters[index].transform.position = board[r, c].transform.position;
	}
	
	void MouseHandler() {
		if (Input.GetMouseButtonDown (0)) {
			Vector3 mouseLoc = cam.ScreenToWorldPoint (Input.mousePosition);
			mouseLoc.z = 0f;
			for (int col = 0; col < width; col++){
				for (int row = 0; row < height; row++) {
					if (board[row,col].collider2D.OverlapPoint(new Vector2(mouseLoc.x, mouseLoc.y))){
						board[row,col].onClick();
						if(board[row, col].curChar == null){
							setFighterLocation(currentFighter, row, col);
						}
						break;
					}
				}
			}
		}
	}
}
