using UnityEngine;
using System.Collections;

public class Cosmo : MonoBehaviour {

	public string levelToLoad;
	
	public void loadServer() {
		Application.LoadLevel (levelToLoad);
	}
}
