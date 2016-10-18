//-------------------------------------------------------------
using UnityEngine;
using System.Collections;
//-------------------------------------------------------------
public class Powerup_Burger : MonoBehaviour 
{
	//Amount of health to give player
	public int HealthAmount = 10;
	
	//Audio Clip for this object
	public AudioClip Clip = null;
	
	//Audio Source for sound playback
	private AudioSource SFX = null;

	//-------------------------------------------------------------
	// Use this for initialization
	void Start () 
	{
		//Find sound object in scene
		GameObject SoundsObject = GameObject.FindGameObjectWithTag("sounds");
		
		//If no sound object, then exit
		if(SoundsObject == null) return;
		
		//Get audio source component for sfx
		SFX = SoundsObject.GetComponent<AudioSource>();
	}
	//-------------------------------------------------------------
	//Event triggered when colliding with player
	void OnTriggerEnter(Collider Other)
	{
		//Is colliding object a player? Cannot collide with enemies
		if(!Other.CompareTag("player")) return;
		
		//Play collection sound, if audio source is available
		if(SFX){SFX.PlayOneShot(Clip, 1.0f);}
		
		//Hide object from level so it cannot be collected more than once
		gameObject.SetActive(false);
		
		//Get PlayerController object and update cash
		PlayerController PC = Other.gameObject.GetComponent<PlayerController>();
		
		//If there is a PC attached to colliding object, then update cash
		if(PC) PC.Health += HealthAmount;
		
		//Post power up collected notification, so other objects can handle this event if required
		GameManager.Notifications.PostNotification(this, "PowerupCollected");
	}
	//--------------------------------------------------------------
}
