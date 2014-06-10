using UnityEngine;
using System.Collections;
using UnityEditor;

public class CharacterIcon : Icon
{
	public Character assChar;
	public int uniqueID;
	//public Font guiFont;
	public StatTextures iconPackage;
	bool isDrawing = false;
	public Rect windowRect;
	int width = 200;
	int height = 300;
	int vOffset = 35;
	int hOffset = 100;
	//float offset = 0.5f;
	//float rowOffset = 0.5f;
	//float iconWidth = 0.5f;
	//float columnRightOffset = 0.5f;
	
	
	
	// Use this for initialization
	void OnGUI(){
		if(isDrawing){
			windowRect = GUI.Window(uniqueID, windowRect, DoMyWindow, new GUIContent(assChar.charName + ", the " + assChar.jobObject.title, assChar.charName+"'s Character Sheet"));
		}
	}
	
	void DoMyWindow(int windowID) {
		GUIStyle wordsStyle = new GUIStyle ();
		GUIStyle boldStyle = new GUIStyle ();
	
		if (GUI.Button(new Rect((2*width/3), height-25, 60, 20), "Close"))
			isDrawing = false;
		if (GUI.Button(new Rect((width/3)+3, height-25, 60, 20), "Job"))
			Debug.Log ("Job pushed");
		if (GUI.Button(new Rect(5, height-25, 60, 20), "Equip"))
			Debug.Log ("Equip pushed");

		
		// attributes (ie hp, mana, armor, etc
		// hp
		GUI.Label (new Rect (10, 60, 30, 30), iconPackage.hp);
		GUI.Label (new Rect (45, 63, 50, 30), new GUIContent(assChar.stats ["Health"].ToString (), "This is your health!"));
		
		// mana
		GUI.Label (new Rect (110, 60, 30, 30), iconPackage.mana);
		GUI.Label (new Rect (145, 63, 50, 30), new GUIContent(assChar.stats ["Mana"].ToString (), "This is your mana!"));
		
		
		// ad
		GUI.Label (new Rect (10, 60 + vOffset, 30, 30), iconPackage.ad);
		GUI.Label (new Rect (45, 63 + vOffset, 50, 30), new GUIContent(assChar.stats ["Strength"].ToString (), "This is your strength!"));
		
		// ap
		GUI.Label (new Rect (110, 60 + vOffset, 30, 30), iconPackage.ap);
		GUI.Label (new Rect (145, 63 + vOffset, 50, 30), new GUIContent(assChar.stats ["Wisdom"].ToString (), "This is your wisdom!"));
		
		// armor
		GUI.Label (new Rect (10, 60 + 2*vOffset, 30, 30), iconPackage.armor);
		GUI.Label (new Rect (45, 63 + 2*vOffset, 50, 30), new GUIContent(assChar.stats ["Physical Resistance"].ToString (), "This is your physical resistance!"));
		
		// mr
		GUI.Label (new Rect (110, 60 + 2*vOffset, 30, 30), iconPackage.mr);
		GUI.Label (new Rect (145, 63 + 2*vOffset, 50, 30), new GUIContent(assChar.stats ["Magical Resistance"].ToString (), "This is your magical resistance!"));
		
		// speed
		GUI.Label (new Rect (10, 60 + 3*vOffset, 30, 30), iconPackage.speed);
		GUI.Label (new Rect (45, 63 + 3*vOffset, 50, 30), new GUIContent(assChar.stats ["Speed"].ToString (), "This is your speed!"));
		
		
		string xpToolTip = "Current XP: "+assChar.currentXP+"/"+assChar.nextLevelXP+".";
		// level
		GUI.Label (new Rect (10, 30, width-10, 30), new GUIContent("Level: " + assChar.currentLevel, xpToolTip));	
		EditorGUI.ProgressBar (new Rect (75, 33, width - 90, 15), assChar.currentXP / ((float)assChar.nextLevelXP), "XP");
		

		GUI.Label (new Rect(10, 220, 200, 40), GUI.tooltip);


		GUI.DragWindow(new Rect(0, 0, 10000, 10000));
	}
	
	public override void onClick() {
		windowRect = new Rect(20 + (20*uniqueID), 20, width, height);
		isDrawing = true;
	}

}

