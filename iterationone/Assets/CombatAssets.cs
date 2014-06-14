using UnityEngine;
using System.Collections;

public class CombatAssets : MonoBehaviour {

	public CombatAsset[] combatAssets;

	// Use this for initialization
	void Start () {
	}
	
	
	public CombatAsset getAssetByName(string name){
		for(int i = 0; i < combatAssets.Length; i++){
			if(combatAssets[i].name == name){
				return combatAssets[i];
			}
		}
		Debug.Log("Asset "+name+" not found.");
		return null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
