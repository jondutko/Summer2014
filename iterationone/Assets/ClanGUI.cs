using UnityEngine;
using System.Collections;

public class ClanGUI : MonoBehaviour {

	public Texture parchTexture;
	public Camera guiCam;
	public Clan theClan;
	public Character activeChar;
	public CharacterGUI sisterGui;
	private float offset = 0.025f;
	private float numPerLine = 10.0f;
	private int activeIcon = 0;
	public Texture activeTexture;

	void Start(){
		theClan.initializeClan ();
		setInitialCharacter ();
	}

	void OnGUI() {
		createClanGUI ();
	}

	public void setInitialCharacter() {
		if (theClan.clanMembers.Count != 0)
			setCurrentCharacter (theClan.clanMembers [0]);
		else
			Debug.Log ("Empty Clan!");
	}

	void setCurrentCharacter(Character clicked){
		activeChar = clicked;
		sisterGui.charShown = activeChar;
		Debug.Log ("Character set as active: " + clicked.charName);
	}

	void createClanGUI(){
		GUIStyle boxStyle = new GUIStyle();
		boxStyle.padding = new RectOffset(5, 5, 5, 5);
		GUI.Box(newBox(0, .5f, 1, 1), parchTexture, boxStyle);
		GUIStyle iconStyle = GUI.skin.label;
		for (int i = 0; i < theClan.clanMembers.Count; i++) {
			if(GUI.Button(newBox ((offset * (i+1)) + i/(numPerLine), .50f + offset, (offset * (i+1)) + (i+1)/(numPerLine), .50f+offset+(1/numPerLine)), theClan.clanMembers[i].icon, iconStyle)){
				setCurrentCharacter(theClan.clanMembers[i]);
				activeIcon = i;
			}
		}
	}

	//all between 0 and 1, except y can never be lower than .5f
	Rect newBox(float topx, float topy, float botx, float boty){
		Vector3 boxStart = guiCam.ViewportToScreenPoint (new Vector3 (topx, topy, 0));
		Vector3 boxEnd = guiCam.ViewportToScreenPoint (new Vector3 (botx, boty, 0));
		return new Rect(boxStart.x, boxStart.y, (boxEnd.x - boxStart.x), (boxEnd.y - boxStart.y));
	}
	
}
