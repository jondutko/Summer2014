using UnityEngine;
using System.Collections;

public class PortalToCombat : Portal {
	
	public override void OnClick() {
		Application.LoadLevel("combat");
	}
}

