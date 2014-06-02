using UnityEngine;
using System.Collections;

public class ClanGUI : MonoBehaviour {

	public Texture parchTexture;
	public Camera guiCam;
	
	void OnGUI() {
		Vector3 boxStart = guiCam.ViewportToScreenPoint (new Vector3 (0, .5f, 0));
		Vector3 boxEnd = guiCam.ViewportToScreenPoint (new Vector3 (1, 1, 0));
		GUIStyle boxStyle = new GUIStyle();
		boxStyle.padding = new RectOffset(5, 5, 5, 5);
		GUI.Box(new Rect(boxStart.x, boxStart.y, (boxEnd.x - boxStart.x), (boxEnd.y - boxStart.y)), parchTexture, boxStyle);
	}

}
