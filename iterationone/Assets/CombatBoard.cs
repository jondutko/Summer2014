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
	public int[] test;
	public Vector3 sqExtents, lowerLeft;
	public Clan theClan;
	public GUIListener clanGui;
	public Camera myCam;
	public List<Icon> iconList;
	public Queue<CombatCharacter> toBePlaced;
	public CombatCharacter beingPlaced;
	
	// Use this for initialization
	void Start () {
		turnOrder.gameState = TurnOrder.MacroState.Placement;
		GameObject stackage = GameObject.Find("Stats");
		turnOrder.turnOrderGui.iconPackage = stackage.GetComponent<StatTextures>();
		float scalar = Mathf.Max (height / 6f, width/12f);
		sq.transform.localScale = new Vector3(1/scalar, 1/scalar, 1);
		float sqWidth = sq.icon.textureRect.width;
		sqExtents = sq.icon.bounds.extents;
		sqExtents.x = sqExtents.x * sq.transform.localScale.x;
		sqExtents.y = sqExtents.y * sq.transform.localScale.y;
		Debug.Log("Sq Width: "+sqWidth);
		Debug.Log("Sq Extents: "+sqExtents);
		lowerLeft = new Vector3(0- (width-1) * sqExtents.x, 0-(height-1) * sqExtents.y, 0);
		board = new SquareIcon[height, width];
		for(int column = 0; column < width; column++){
			for (int row = 0; row < height; row++){
				board[row, column] = Instantiate(sq, new Vector3(lowerLeft.x + column*sqExtents.x*2, lowerLeft.y + row*sqExtents.y*2, 0), Quaternion.identity) as SquareIcon; 	
				board[row,column].row = row;
				board[row,column].col = column;
			}
		}
		
		
		GameObject clanObj = GameObject.Find ("Clan");
		theClan = clanObj.GetComponent<Clan>();
		Debug.Log ("Found clan with members: " + theClan.clanMembers.Count);
		
		GameObject clanGuiObj = GameObject.Find ("ClanGUI");
		clanGui = clanGuiObj.GetComponent<GUIListener>();
		
		clanGui.guiCam = myCam;
		
		CombatCharacter blankGob = (CombatCharacter) library.getAssetByName("goblin");

		List<CombatCharacter> creeps = new List<CombatCharacter>();
		int numGobs = 3;
		for(int i = 0; i < numGobs; i++)
			creeps.Add (blankGob);
		
		CombatCharacter blankHero = (CombatCharacter) library.getAssetByName("blankcharacter");
		
		List<CombatCharacter> heroes = new List<CombatCharacter>();
		foreach (Character hero in theClan.clanMembers){
			heroes.Add (blankHero);
		}

		PopulateFighters(creeps, heroes);
		toBePlaced = new Queue<CombatCharacter>(turnOrder.fighters);

	}

	public void PopulateFighters(List<CombatCharacter> creeps, List<CombatCharacter> heroes) {
		GameObject blankIconObject = GameObject.Find("blankcreepicon");
		CreepIcon blankIcon = blankIconObject.GetComponent<CreepIcon>();
		
		GameObject blankIconObject2 = GameObject.Find("blankcharactericon");
		CharacterIcon blankCharIcon = blankIconObject2.GetComponent<CharacterIcon>();
		
		iconList = new List<Icon>();
		int i = 0;
		foreach (CombatCharacter creep in creeps){
			CreepIcon newCreepIcon = Instantiate(blankIcon, new Vector3(0, 0, 0), Quaternion.identity) as CreepIcon;
			CombatCharacter newCreep = Instantiate (creep, new Vector3(20, 20, 0), Quaternion.identity) as CombatCharacter;
			newCreepIcon.GetComponent<SpriteRenderer>().sprite = newCreep.GetComponent<SpriteRenderer>().sprite;
			newCreep.combatName = "Goblin " + i;
			newCreepIcon.combatAssChar = newCreep;
			iconList.Add (newCreepIcon);
			i++;
		}
		
		i = 0;
		foreach (CombatCharacter hero in heroes){
			CharacterIcon newCharIcon = Instantiate(blankCharIcon, new Vector3(0, i, 0), Quaternion.identity) as CharacterIcon;
			CombatCharacter newHero = Instantiate(hero, new Vector3(20, 20, 0), Quaternion.identity) as CombatCharacter;
			newCharIcon.GetComponent<SpriteRenderer>().sprite = clanGui.iconList[i].GetComponent<SpriteRenderer>().sprite;
			newHero.GetComponent<SpriteRenderer>().sprite = clanGui.iconList[i].GetComponent<SpriteRenderer>().sprite;
			newCharIcon.combatAssChar = newHero;
			newCharIcon.assChar = theClan.clanMembers[i];
			newCharIcon.setAttributes();
			iconList.Add (newCharIcon);
			i++;
		}
		
		iconList.Sort((c1, c2) => (int) (c2.combatAssChar.speed - c1.combatAssChar.speed));
		
		int slot = 0;
		foreach(Icon icon in iconList) {
			BoxCollider2D temp = icon.GetComponent<BoxCollider2D>();
			temp.size = icon.GetComponent<SpriteRenderer>().bounds.size;
			icon.combatAssChar.transform.position = new Vector3(7, iconList.Count -slot*1, 0);
			icon.transform.localScale = new Vector3(.5f, .5f, .5f);
			icon.transform.position = new Vector3(17, iconList.Count - slot*3, 0);
			turnOrder.fighters.Enqueue(icon.combatAssChar);
			slot++;
		}
		
		
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
	
	public void highlightAttackSquares(CombatCharacter fighter, int range) {
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
				if ((squareDistances[sqIcon.row, sqIcon.col] == -1) && (squareDistances[curSquare.row, curSquare.col] < range)) {
					squareDistances[sqIcon.row, sqIcon.col] = squareDistances[curSquare.row, curSquare.col] + 1;
					sqQueue.Enqueue (sqIcon);
				}
			}
		}
		
		for(int i = 0; i < height; i++) {
			for(int j = 0; j < width; j++)
				if ((squareDistances[i,j] != -1) && squareDistances[i,j] <= range){
					if(board[i, j].curChar != null){
						board[i, j].highlightForTarget();
					}
					else{
						board[i, j].highlightForAttack();
					}
				}
		}
		board[fighter.row, fighter.col].highlightForActive();
		
	}
	
	public void clearHighlightedSquares() {
		for(int i = 0; i < height; i++) {
			for(int j = 0; j < width; j++)
				board[i, j].unhighlight();
		}
		
	}
	
	public List<SquareIcon> getNeighbors(SquareIcon curSquare) {
		List<SquareIcon> neighborList = new List<SquareIcon>();
		if((curSquare.row-1>=0) && (curSquare.row-1 < height) && (curSquare.col>=0) && (curSquare.col < width))
			neighborList.Add (board[curSquare.row-1, curSquare.col]);
		if((curSquare.row+1>=0) && (curSquare.row+1 < height) && (curSquare.col>=0) && (curSquare.col < width))
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
