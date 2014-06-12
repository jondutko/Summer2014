using UnityEngine;
using System.Collections;

public class CombatAssets : MonoBehaviour {

	private CombatAsset[] combatAssets;

	// Use this for initialization
	void Start () {
		combatAssets = new CombatAsset[transform.childCount];
		int i = 0;
		foreach (Transform child in transform) {
			combatAssets[i] = child.GetComponent<CombatAsset>();
			i++;
		}
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
