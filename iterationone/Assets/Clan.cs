using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Clan : MonoBehaviour
{

		public List<Character> clanMembers;
		public string clanName;
		public List<Quest> currentQuests;
		public List<Quest> completedQuests;
		public List<Equipment> unusedEquipment;

		// Use this for initialization
		void Start ()
		{
			DontDestroyOnLoad(this);
		}

		public void initializeClan ()
		{
			if (clanMembers == null)
				clanMembers = new List<Character> ();
			if (currentQuests == null)
				currentQuests = new List<Quest> ();
			if (completedQuests == null)
				completedQuests = new List<Quest> ();
			if (unusedEquipment == null)
				unusedEquipment = new List<Equipment> ();
			foreach (Transform child in transform) {
				Character curChar = child.GetComponent<Character> ();
				curChar.initializeCharacter (1, 100, 80, 30, 25, 30, 28, 6);
			}

		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void addEquipment (Equipment equip)
		{
			unusedEquipment.Add (equip);
		}

		public Equipment delEquipment (Equipment equip)
		{
			if (unusedEquipment.Contains(equip))
				unusedEquipment.Remove (equip);
			else
				return null;
			return equip;
		}
}
