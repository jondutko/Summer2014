using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Clan : MonoBehaviour
{

		public List<Character> clanMembers;
		public string clanName;
		public List<Quest> currentQuests;
		public List<Quest> completedQuests;
		public Dictionary<Equipment, int> unusedEquipment;

		// Use this for initialization
		void Start ()
		{


		}

		public void initializeClan ()
		{
			clanMembers = new List<Character> ();
			currentQuests = new List<Quest> ();
			completedQuests = new List<Quest> ();
			unusedEquipment = new Dictionary<Equipment, int> ();
			foreach (Transform child in transform) {
				Character curChar = child.GetComponent<Character> ();
				curChar.initializeCharacter (1, 100, 80, 30, 25, 30, 28, 6);
				clanMembers.Add (curChar);
			}

		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void addEquipment (Equipment equip)
		{
			if (unusedEquipment.ContainsKey (equip))
				unusedEquipment [equip]++;
			else
				unusedEquipment [equip] = 1;
		}

		public Equipment delEquipment (Equipment equip)
		{
			if (unusedEquipment.ContainsKey (equip)) {
				if (unusedEquipment [equip] > 1)
						unusedEquipment [equip]--;
				else if (unusedEquipment [equip] == 1)
						unusedEquipment.Remove (equip);
				else {
						Debug.Log ("Dire Straits");
						return null;
				}
				return equip;
			} 
			else {
				Debug.Log ("More Dire Straits");
				return null;
			}
		}
}
