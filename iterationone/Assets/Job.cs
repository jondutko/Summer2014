using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Job : MonoBehaviour {

	public string title;
	public Job prereqJob;
	public int prereqLevel;
	
	public List<Ability> skills;
	
	public void initializeJob (string newTitle){
			title = newTitle;
			prereqJob = null;
			prereqLevel = 0;
	}

}
