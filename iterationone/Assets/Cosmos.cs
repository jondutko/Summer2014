using UnityEngine;
using System.Collections;

public class Cosmos : MonoBehaviour
{
	public Camera ourCam;
	public Clan theClan;
	
	void Start() {
		theClan.initializeClan();
	}
	void Update() {
		mouseHandler();
	}
	
	
	void mouseHandler(){
		if (Input.GetMouseButtonDown (0)) {
			Vector3 mouseLoc = ourCam.ScreenToWorldPoint (Input.mousePosition);
			mouseLoc.z = 0f;
			foreach(Transform child in transform) {
				Cosmo cosmo = child.GetComponent<Cosmo>();
				if(cosmo.collider2D.OverlapPoint(new Vector2(mouseLoc.x, mouseLoc.y))) {
					cosmo.loadServer();
				}
			}
		}
	}

}