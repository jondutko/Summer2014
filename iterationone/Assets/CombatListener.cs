using UnityEngine;
using System.Collections;

public class CombatListener : MonoBehaviour {

	public Camera cam;
	public CombatBoard gameBoard;
	public TurnOrder turnOrder;

	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame
	void Update () {
		MouseHandler ();
	}
	
	
	void MouseHandler() {
		if (Input.GetMouseButtonDown (0)) {
			Vector3 mouseLoc = cam.ScreenToWorldPoint (Input.mousePosition);
			mouseLoc.z = 0f;
			for (int col = 0; col < gameBoard.width; col++){
				for (int row = 0; row < gameBoard.height; row++) {
					if (gameBoard.board[row,col].collider2D.OverlapPoint(new Vector2(mouseLoc.x, mouseLoc.y))){
						gameBoard.board[row,col].onClick();
						break;
					}
				}
			}
		}
	}
}
