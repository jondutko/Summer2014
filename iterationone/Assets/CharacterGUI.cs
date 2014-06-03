using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterGUI : MonoBehaviour {

	public Texture parchTexture;
	public Camera guiCam;
	public Character charShown;
	public float offset = 0.025f;
	public ClanGUI sisterGUI;
	public Font guiFont;

	void Start() {

	}

	void OnGUI() {
		createCharGUI ();
	
	}

	void createCharGUI() {
		GUIStyle wordsStyle = new GUIStyle ();
		wordsStyle.font = guiFont;
		wordsStyle.fontSize = 25;
		wordsStyle.fontStyle = FontStyle.Italic;
		GUIStyle boxStyle = new GUIStyle();
		boxStyle.padding = new RectOffset(5, 5, 5, 5);
		GUI.Box(newBox(0, 0, 1, 0.5f), parchTexture, boxStyle);

		// portrait
		GUI.Label (newBox (offset, offset, offset + 0.2f, offset + 0.4f), charShown.portrait);

		// blank the faggotmancer
		GUI.Label (newBox (offset + 0.22f, offset+.015f, 1.0f - 0.2f, offset + 0.1f), charShown.charName + ", the " + charShown.jobName, wordsStyle);

	}

	//all between 0 and 1, except y can never be higher than .5f
	Rect newBox(float topx, float topy, float botx, float boty){
		Vector3 boxStart = guiCam.ViewportToScreenPoint (new Vector3 (topx, topy, 0));
		Vector3 boxEnd = guiCam.ViewportToScreenPoint (new Vector3 (botx, boty, 0));
		return new Rect(boxStart.x, boxStart.y, (boxEnd.x - boxStart.x), (boxEnd.y - boxStart.y));
	}

}
