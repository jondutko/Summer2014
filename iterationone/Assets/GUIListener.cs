﻿using UnityEngine;
using System.Collections;

public class GUIListener : MonoBehaviour {

	public Camera guiCam;
	public CharacterIcon[] iconList;
	
	// Use this for initialization
	void Start () {
		iconList = new CharacterIcon[transform.childCount];
		int i = 0;
		foreach (Transform child in transform) {
			iconList[i] = child.GetComponent<CharacterIcon>();
			i++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		MouseHandler ();
	}
	
	void MouseHandler() {
		if(!Application.loadedLevelName.Equals ("combat")) {
			if (Input.GetMouseButtonDown (0)) {
				Vector3 mouseLoc = guiCam.ScreenToWorldPoint (Input.mousePosition);
				mouseLoc.z = 0f;
				for (int i = 0; i < iconList.Length; i++){
					if (iconList[i].collider2D.OverlapPoint(new Vector2(mouseLoc.x, mouseLoc.y))){
						iconList[i].onClick();
						break;
					}
				}
			}
		}
	}
}
