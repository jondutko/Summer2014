using UnityEngine;
using System.Collections;

public class Job : MonoBehaviour {

	public string title;
	public Job prereqJob;
	public int prereqLevel;
	public Quest prereqQuest;

	public void initializeJob (string newTitle){
			title = newTitle;
			prereqJob = null;
			prereqLevel = 0;
			prereqQuest = null;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
