//------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//------------------------------------------------
public class PlayerController : MonoBehaviour
{
	//------------------------------------------------
	//Amount of cash player should collect to complete level
	public float CashTotal = 1400.0f;
	
	//Amount of cash for this player
	private float cash = 0.0f;

	//Reference to transform
	private Transform ThisTransform = null;

	//Respawn time in seconds after dying
	public float RespawnTime = 2.0f;

	//Player health
	public int health = 100;

	//Get Mecanim animator component in children
	private Animator AnimComp = null;

	//Private damage texture
	private Texture2D DamageTexture = null;

	//Screen coordinates
	private Rect ScreenRect;
	
	//Show damage texture?
	private bool ShowDamage = false;

	//Damage texture interval (amount of time in seconds to show texture)
	private float DamageInterval = 0.2f;
	//------------------------------------------------
	//Called when object is created
	void Start()
	{
		//Get First person capsule and make non-visible
		MeshRenderer Capsule = GetComponentInChildren<MeshRenderer>();
		Capsule.enabled = false;

		//Get Animator
		AnimComp = GetComponentInChildren<Animator>();

		//Create damage texture
		DamageTexture = new Texture2D(1,1);
		DamageTexture.SetPixel(0,0,new Color(255,0,0,0.5f));
		DamageTexture.Apply();

		//Get cached transform
		ThisTransform = transform;
	}
	//------------------------------------------------
	//Accessors to set and get cash
	public float Cash
	{
		//Return cash value
		get{return cash;}
		
		//Set cash and validate, if required
		set
		{
			//Set cash
			cash = value;
			
			//Check collection limit - post notification if limit reached
			if(cash >= CashTotal)
				GameManager.Notifications.PostNotification(this, "CashCollected");
		}
	}
	//------------------------------------------------
	//Accessors to set and get health
	public int Health
	{
		//Return health value
		get{return health;}
		
		//Set health and validate, if required
		set
		{
			health = value;
			
			//Playe Die functionality
			if(health <= 0) gameObject.SendMessage("Die",SendMessageOptions.DontRequireReceiver);
		}
	}
	//------------------------------------------------
	//Function to apply damage to the player
	public IEnumerator ApplyDamage(int Amount = 0)	
	{
		//Reduce health
		Health -= Amount;
		
		//Post damage notification
		GameManager.Notifications.PostNotification(this, "PlayerDamaged");
		
		//Show damage texture
		ShowDamage = true;

		//Wait for interval
		yield return new WaitForSeconds(DamageInterval);
		
		//Hide damage texture
		ShowDamage = false;
	}
	//------------------------------------------------
	//ON GUI Function to show texture
	void OnGUI()
	{
		if(ShowDamage){GUI.DrawTexture(ScreenRect,DamageTexture);}
	}
	//------------------------------------------------
	//Function called when player dies
	public IEnumerator Die()
	{
		//Disable input
		GameManager.Instance.InputAllowed = false;
		
		//Trigger death animation if available
		if(AnimComp) AnimComp.SetTrigger("ShowDeath");
		
		//Wait for respawn time
		yield return new WaitForSeconds(RespawnTime);
		
		//Restart level
		Application.LoadLevel(Application.loadedLevel);
	}
	//------------------------------------------------
	void Update()
	{
		//Build screen rect on update (in case screen size changes)
		ScreenRect.x = ScreenRect.y = 0;
		ScreenRect.width = Screen.width;
		ScreenRect.height = Screen.height;
	}
	//------------------------------------------------
}