using UnityEngine;
using System.Collections;

public class PortalToSkag : Portal {

	public override void OnClick() {
		Application.LoadLevel("skag");
	}
}
