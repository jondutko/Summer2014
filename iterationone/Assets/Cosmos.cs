using UnityEngine;
using System.Collections;

public class Cosmos : MonoBehaviour
{
	private Hashtable serverTable = new Hashtable();

	public Cosmos() {
		serverTable.Add("Skagerrak", "skag_text_file");
	}

	public getServerTextFile(string serverName) {
		return serverTable[serverName];
	}

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}
}

