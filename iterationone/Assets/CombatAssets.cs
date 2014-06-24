using UnityEngine;
using System.Collections;

public class CombatAssets : MonoBehaviour {

	public CombatAsset[] assets;

	// Use this for initialization
	void Start () {
		assets = new CombatAsset[transform.childCount];
		int i = 0;
		foreach (Transform child in transform){
			assets[i] = child.GetComponent<CombatAsset>();
			i++;
		}
	}
	
	
	public CombatAsset getAssetByName(string name){
		for(int i = 0; i < assets.Length; i++){
			if(assets[i].name == name){
				return assets[i];
			}
		}

		Debug.Log("Asset "+name+" not found.");
		return null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
