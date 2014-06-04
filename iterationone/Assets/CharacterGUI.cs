using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class CharacterGUI : MonoBehaviour {

	public Texture parchTexture;
	public Camera guiCam;
	public Character charShown;
	private float offset = 0.025f, iconWidth = 0.05f, columnRightOffset = 0.2f, rowOffset = 0.075f;
	public ClanGUI sisterGUI;
	public Font guiFont;

	private enum typeDisplaying {character, equipment, job};
	private typeDisplaying currentDisplay = typeDisplaying.character;

	public Texture hp, mn, ad, armor, mag, mr, spd;

	void Start() {

	}

	void OnGUI() {
		createCharGUI ();
	
	}

	void createCharGUI() {
		GUIStyle wordsStyle = new GUIStyle ();
		GUIStyle boldStyle = new GUIStyle ();
		wordsStyle.font = guiFont;
		wordsStyle.fontSize = 25;
		wordsStyle.fontStyle = FontStyle.Italic;
		boldStyle.fontSize = 25;
		boldStyle.fontStyle = FontStyle.Bold;
		boldStyle.font = guiFont;
		GUIStyle boxStyle = new GUIStyle();
		boxStyle.padding = new RectOffset(5, 5, 5, 5);
		GUI.Box(newBox(0, 0, 1, 0.5f), parchTexture, boxStyle);

		if (currentDisplay == typeDisplaying.character) {

			// portrait
			GUI.Label (newBox (offset, offset, offset + 0.2f, offset + 0.4f), charShown.portrait);

			// blank the faggotmancer
			GUI.Label (newBox (offset + 0.22f, offset + .01f, 0.8f, offset + 0.05f), charShown.charName + ", the " + charShown.jobObject.title, wordsStyle);

			// attributes (ie hp, mana, armor, etc
			// hp
			GUI.Label (newBox (offset + 0.22f, offset + rowOffset, offset + 0.22f + iconWidth, offset + rowOffset + iconWidth), hp);
			GUI.Label (newBox (offset + 0.21f + iconWidth + offset, offset + rowOffset + 0.005f, 2 * offset + 0.21f + 2 * iconWidth, offset + rowOffset + 0.005f + iconWidth), charShown.stats ["Health"].ToString (), wordsStyle);

			// mana
			GUI.Label (newBox (offset + 0.22f + columnRightOffset, offset + rowOffset, offset + 0.22f + iconWidth + columnRightOffset, offset + rowOffset + iconWidth), mn);
			GUI.Label (newBox (offset + 0.21f + iconWidth + offset + columnRightOffset, offset + rowOffset + 0.005f, 2 * offset + 0.21f + 2 * iconWidth + columnRightOffset, offset + rowOffset + 0.005f + iconWidth), charShown.stats ["Mana"].ToString (), wordsStyle);

			// ad
			GUI.Label (newBox (offset + 0.22f, offset + 2 * rowOffset, offset + 0.22f + iconWidth, offset + 2 * rowOffset + iconWidth), ad);
			GUI.Label (newBox (offset + 0.21f + iconWidth + offset, offset + 2 * rowOffset + 0.005f, 2 * offset + 0.21f + 2 * iconWidth, offset + 2 * rowOffset + 0.005f + iconWidth), charShown.stats ["Strength"].ToString (), wordsStyle);

			// ap
			GUI.Label (newBox (offset + 0.22f + columnRightOffset, offset + 2 * rowOffset, offset + 0.22f + iconWidth + columnRightOffset, offset + 2 * rowOffset + iconWidth), mag);
			GUI.Label (newBox (offset + 0.21f + iconWidth + offset + columnRightOffset, offset + 2 * rowOffset + 0.005f, 2 * offset + 0.21f + 2 * iconWidth + columnRightOffset, offset + 2 * rowOffset + 0.005f + iconWidth), charShown.stats ["Wisdom"].ToString (), wordsStyle);

			// armor
			GUI.Label (newBox (offset + 0.22f, offset + 3 * rowOffset, offset + 0.22f + iconWidth, offset + 3 * rowOffset + iconWidth), armor);
			GUI.Label (newBox (offset + 0.21f + iconWidth + offset, offset + 3 * rowOffset + 0.005f, 2 * offset + 0.21f + 2 * iconWidth, offset + 3 * rowOffset + 0.005f + iconWidth), charShown.stats ["Physical Resistance"].ToString (), wordsStyle);

			// mr
			GUI.Label (newBox (offset + 0.22f + columnRightOffset, offset + 3 * rowOffset, offset + 0.22f + iconWidth + columnRightOffset, offset + 3 * rowOffset + iconWidth), mr);
			GUI.Label (newBox (offset + 0.21f + iconWidth + offset + columnRightOffset, offset + 3 * rowOffset + 0.005f, 2 * offset + 0.21f + 2 * iconWidth + columnRightOffset, offset + 3 * rowOffset + 0.005f + iconWidth), charShown.stats ["Magical Resistance"].ToString (), wordsStyle);

			// speed
			GUI.Label (newBox (offset + 0.22f, offset + 4 * rowOffset, offset + 0.22f + iconWidth, offset + 4 * rowOffset + iconWidth), spd);
			GUI.Label (newBox (offset + 0.21f + iconWidth + offset, offset + 4 * rowOffset + 0.005f, 2 * offset + 0.21f + 2 * iconWidth, offset + 4 * rowOffset + 0.005f + iconWidth), charShown.stats ["Speed"].ToString (), wordsStyle);

			// level
			GUI.Label (newBox (offset + 0.025f, offset + 5.5f * rowOffset, offset + 0.15f, offset + 5.5f * rowOffset + iconWidth), "Level: " + charShown.currentLevel, boldStyle);	
			EditorGUI.ProgressBar (newBox (offset + 0.2f, offset + 5.5f * rowOffset + 0.0015f, offset + 0.7f, offset + 5.5f * rowOffset + iconWidth - 0.02f), charShown.currentXP / ((float)charShown.nextLevelXP), "XP");
		} else if (currentDisplay == typeDisplaying.equipment) {

		} else if (currentDisplay == typeDisplaying.job) {

		} else
				Debug.Log ("WHAT THE FUCK");
		// Buttons
		// Active button - Character sheet
		boldStyle.wordWrap = true;
		boldStyle.alignment = TextAnchor.MiddleCenter;
		if(GUI.Button (newBox (0.75f, 2*offset, 1.0f - offset, 2*offset + 0.1f), "Character", boldStyle))
			currentDisplay = typeDisplaying.character;

		// equipment
		if(GUI.Button (newBox (0.75f, 2*offset+0.15f, 1.0f - offset, 2*offset + 0.25f), "Equipment", boldStyle)){
			currentDisplay = typeDisplaying.equipment;
			Debug.Log(currentDisplay.ToString());
		}

		// job
		if(GUI.Button (newBox (0.75f, 2*offset+0.3f, 1.0f - offset, 2*offset + 0.4f), "Job", boldStyle))
			currentDisplay = typeDisplaying.job;
	
	}

	//all between 0 and 1, except y can never be higher than .5f
	Rect newBox(float topx, float topy, float botx, float boty){
		Vector3 boxStart = guiCam.ViewportToScreenPoint (new Vector3 (topx, topy, 0));
		Vector3 boxEnd = guiCam.ViewportToScreenPoint (new Vector3 (botx, boty, 0));
		return new Rect(boxStart.x, boxStart.y, (boxEnd.x - boxStart.x), (boxEnd.y - boxStart.y));
	}

}
