//--------------------------------------------------------------
using UnityEngine;
using System.Collections;
//--------------------------------------------------------------
public class GUIUpdateStats : MonoBehaviour 
{
	//Player reference
	private PlayerController PC = null;

	//Health Label Component
	public GUILabel HealthLabel = null;

	//Ammo Label Component
	public GUILabel AmmoLabel = null;

	//--------------------------------------------------------------
	void Start()
	{
		//Get Player Controller Component
		GameObject PlayerObject = GameObject.Find("Player");
		PC = PlayerObject.GetComponentInChildren<PlayerController>();
	}
	//--------------------------------------------------------------
	// Update is called once per frame
	void Update ()
	{
		//Update health and ammo strings
		AmmoLabel.LabelData.text = "Ammo: " + ((PC.ActiveWeapon.Ammo < 0) ? "None" : PC.ActiveWeapon.Ammo.ToString());
		HealthLabel.LabelData.text = "Health: " + Mathf.Clamp(PC.Health,0,100);
	}
	//--------------------------------------------------------------
}