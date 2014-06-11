using UnityEngine;
using System.Collections;
using UnityEditor;

public class CharacterIcon : Icon
{
	public Character assChar;
	public int uniqueID;
	//public Font guiFont;
	public StatTextures iconPackage;
	bool isDrawing = false;
	public Rect windowRect, secondWindowRect;
	int width = 210;
	int height = 300;
	int vOffset = 35;
	private WindowView windowView = WindowView.character;
	private bool displayingEquipDialog = false;
	private int equipDialogSlot = -1;
	private int chosenClanEquipment = -1;
	//float offset = 0.5f;
	//float rowOffset = 0.5f;
	//float iconWidth = 0.5f;
	//float columnRightOffset = 0.5f;
	
	enum WindowView {
		character,
		equipment,
		job
	};
	
	
	// Use this for initialization
	void OnGUI(){
		if(isDrawing){
			windowRect = GUI.Window(uniqueID, windowRect, DoMyWindow, new GUIContent(assChar.charName + ", the " + assChar.jobObject.title, assChar.charName+"'s Character Sheet"));
		}
		if(displayingEquipDialog) {
			secondWindowRect = new Rect(900, 50, 300, 8000);
			secondWindowRect = GUI.ModalWindow(99-uniqueID, secondWindowRect, DoMyWindowSecond, new GUIContent("Clan Inventory", assChar.charName+"'s item slot " + equipDialogSlot));
		}
	}
	
	void DoMyWindow(int windowID) {
	
		if (GUI.Button(new Rect(15, height-50, 80, 20), "Character"))
			windowView = WindowView.character;
		if (GUI.Button(new Rect(115, height-50, 80, 20), "Job"))
			windowView = WindowView.job;
		if (GUI.Button(new Rect(15, height-25, 80, 20), "Equip"))
			windowView = WindowView.equipment;
		if (GUI.Button(new Rect(115, height-25, 80, 20), "Close")) {
			isDrawing = false;
			windowView = WindowView.character;
		}

		if (windowView == WindowView.character) {
			// attributes (ie hp, mana, armor, etc
			// hp
			GUI.Label (new Rect (10, 60, 30, 30), iconPackage.hp);
			GUI.Label (new Rect (45, 63, 50, 30), new GUIContent(assChar.stats [StatTextures.Stat.Health].ToString (),
				"Base: " + assChar.baseStats[StatTextures.Stat.Health] + ", Modifier: " + assChar.getEquipModifier(StatTextures.Stat.Health)));
			
			// mana
			GUI.Label (new Rect (110, 60, 30, 30), iconPackage.mana);
			GUI.Label (new Rect (145, 63, 50, 30), new GUIContent(assChar.stats [StatTextures.Stat.Mana].ToString (), 
			     "Base: " + assChar.baseStats[StatTextures.Stat.Mana] + ", Modifier: " + assChar.getEquipModifier(StatTextures.Stat.Mana)));
			
			// ad
			GUI.Label (new Rect (10, 60 + vOffset, 30, 30), iconPackage.ad);
			GUI.Label (new Rect (45, 63 + vOffset, 50, 30), new GUIContent(assChar.stats [StatTextures.Stat.AD].ToString (),
				 "Base: " + assChar.baseStats[StatTextures.Stat.AD] + ", Modifier: " + assChar.getEquipModifier(StatTextures.Stat.AD)));
			
			// ap
			GUI.Label (new Rect (110, 60 + vOffset, 30, 30), iconPackage.ap);
			GUI.Label (new Rect (145, 63 + vOffset, 50, 30), new GUIContent(assChar.stats [StatTextures.Stat.AP].ToString (),
			     "Base: " + assChar.baseStats[StatTextures.Stat.AP] + ", Modifier: " + assChar.getEquipModifier(StatTextures.Stat.AP)));
			
			// armor
			GUI.Label (new Rect (10, 60 + 2*vOffset, 30, 30), iconPackage.armor);
			GUI.Label (new Rect (45, 63 + 2*vOffset, 50, 30), new GUIContent(assChar.stats [StatTextures.Stat.Armor].ToString (),
				"Base: " + assChar.baseStats[StatTextures.Stat.Armor] + ", Modifier: " + assChar.getEquipModifier(StatTextures.Stat.Armor)));
			
			// mr
			GUI.Label (new Rect (110, 60 + 2*vOffset, 30, 30), iconPackage.mr);
			GUI.Label (new Rect (145, 63 + 2*vOffset, 50, 30), new GUIContent(assChar.stats [StatTextures.Stat.MR].ToString (),
				"Base: " + assChar.baseStats[StatTextures.Stat.MR] + ", Modifier: " + assChar.getEquipModifier(StatTextures.Stat.MR)));
			
			// speed
			GUI.Label (new Rect (10, 60 + 3*vOffset, 30, 30), iconPackage.speed);
			GUI.Label (new Rect (45, 63 + 3*vOffset, 50, 30), new GUIContent(assChar.stats [StatTextures.Stat.Speed].ToString (),
				"Base: " + assChar.baseStats[StatTextures.Stat.Speed] + ", Modifier: " + assChar.getEquipModifier(StatTextures.Stat.Speed)));
			
			
			string xpToolTip = "Current XP: "+assChar.currentXP+"/"+assChar.nextLevelXP+".";
			// level
			GUI.Label (new Rect (10, 30, width-10, 30), new GUIContent("Level: " + assChar.currentLevel, xpToolTip));	
			EditorGUI.ProgressBar (new Rect (75, 33, width - 90, 15), assChar.currentXP / ((float)assChar.nextLevelXP), "XP");
			
	
			GUI.Label (new Rect(10, 220, 200, 40), GUI.tooltip);
		}
		else if(windowView == WindowView.equipment) {
			Equipment curEquipment;
			string msg;
			int charVOffset = 220 / assChar.numItemSlots;
			for(int i = 0; i < assChar.numItemSlots; i++) {
				curEquipment = assChar.equipList[i];
				msg = "";
				if (curEquipment != null) {
					msg = curEquipment.equipName + ": " + curEquipment.statsToString();
				}
				if(GUI.Button( new Rect(10, 25 + charVOffset*i, 190, 30), msg)) {
					displayingEquipDialog = true;
					equipDialogSlot = i;
				}
			}
		
		}
		else if(windowView == WindowView.job) {
			GUI.Label (new Rect (10, 30, width-30, 20), "Current Job: "+assChar.jobObject.title);
			for(int i = 0; i < assChar.jobObject.skills.Count; i++){
				GUI.Label(new Rect (30, 50+(20 * i), width-35, 20), new GUIContent(assChar.jobObject.skills[i].abilName, assChar.jobObject.skills[i].abilDescription));
				if(GUI.Button(new Rect (120, (50+20*i), 40, 20), assChar.jobObject.skills[i].abilCost.ToString()))
					Debug.Log ("Tried to buy "+assChar.jobObject.skills[i].abilName+" skill for "+assChar.jobObject.skills[i].abilCost+" "+assChar.jobObject.title+ " points.");
			
			}
			GUI.Button (new Rect(10, 170, 100, 20), "Change Jobs");
			GUI.Label (new Rect(10, 190, 180, 65), GUI.tooltip);
		}
		else
			Debug.Log ("BIG PROBLEMS IN CHARACTERICON");


		GUI.DragWindow(new Rect(0, 0, 10000, 10000));
	}
	
	void DoMyWindowSecond(int windowID) {
		int selection = -1;
		bool showing = true;
		
		GUIContent guiContent = new GUIContent();
		GUIStyle listStyle = new GUIStyle();
		listStyle.normal.textColor = Color.white;
		Texture2D tex = new Texture2D(2, 2);
		Color[] colors = new Color[4];
		for(int i = 0; i < colors.Length; i++) colors[i] = Color.white;
		tex.SetPixels(colors);
		tex.Apply();
		listStyle.hover.background = tex;
		listStyle.onHover.background = tex;
		listStyle.padding.left = listStyle.padding.right = listStyle.padding.top = listStyle.padding.bottom = 4;
		
		if(Popup.List(new Rect(20, 50, 200, 200), ref showing, ref selection, guiContent, assChar.clan.unusedEquipment.ToArray(), listStyle, nothing)) {
			if(selection!=-1) {
				Debug.Log (selection);
				chosenClanEquipment = selection;
				displayingEquipDialog = false;
				if(assChar.equipList[equipDialogSlot] != null) {
					Equipment equip = assChar.unEquip (equipDialogSlot);
					assChar.clan.addEquipment(equip);
				}
				assChar.Equip (assChar.clan.unusedEquipment[selection], equipDialogSlot);
			assChar.clan.delEquipment(assChar.clan.unusedEquipment[selection]);
			assChar.updateStats ();
			}
		}
			
			
		
	}
	
	void nothing() {}
	
	public override void onClick() {
		assChar.updateStats();
		windowRect = new Rect(20 + (20*uniqueID), 20, width, height);
		isDrawing = true;
	}

}

