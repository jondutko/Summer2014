using UnityEngine;
using System.Collections;

public class Cosmos : MonoBehaviour
{
	private Hashtable serverTable = new Hashtable();

	public Cosmos() {
		serverTable.Add(0, "skag_text_file.txt");
	}

	public string getServerTextFile(string serverName) {
		return (string)serverTable[serverName];
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

