using UnityEngine;
using System.Collections;

public class Job : MonoBehaviour {

	public string title;
	public struct Prerequisite {
		public Job prereqJob;
		public int prereqLevel;
		public Quest prereqQuest;
	}

	public Prerequisite prereq;

	public Job (string newTitle){
			title = newTitle;
			prereq.prereqJob = null;
			prereq.prereqLevel = 0;
			prereq.prereqQuest = null;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
