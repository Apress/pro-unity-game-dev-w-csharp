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

	//Player health
	public int health = 100;

	//Default player weapon (Punch)
	public Weapon DefaultWeapon = null;

	//Respawn time in seconds after dying
	public float RespawnTime = 2.0f;
	
	//Sound to play on attack
	public AudioClip DamageAudio = null;
	
	//Audio Source for sound playback
	private AudioSource SFX = null;

	//Currently active weapon
	public Weapon ActiveWeapon = null;

	//Private damage texture
	private Texture2D DamageTexture = null;

	//Screen coordinates
	private Rect ScreenRect;

	//Show damage texture?
	private bool ShowDamage = false;

	//Damage texture interval (amount of time in seconds to show texture)
	private float DamageInterval = 0.2f;
	
	//Get Mecanim animator component in children
	private Animator AnimComp = null;

	//Reference to components for first person input that must be disabled
	public MonoBehaviour[] FPSInputComponents = null;

	//Reference to secondary weapon
	public Weapon CollectWeapon = null;

	//Reference to transform
	private Transform ThisTransform = null;
	
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
	//Called when object is created
	void Start()
	{
		//Activate default weapon
		DefaultWeapon.gameObject.SendMessage("Equip", DefaultWeapon.Type);

		//Set active weapon
		ActiveWeapon = DefaultWeapon;

		//Register controller for weapon expiration events
		GameManager.Notifications.AddListener(this, "AmmoExpired");

		//Register controller for input change events
		GameManager.Notifications.AddListener(this, "InputChanged");

		//Add listeners for saving games
		GameManager.Notifications.AddListener(this, "SaveGamePrepare");
		GameManager.Notifications.AddListener(this, "LoadGameComplete");

		//Find sound object in scene
		GameObject SoundsObject = GameObject.FindGameObjectWithTag("sounds");
		
		//If no sound object, then exit
		if(SoundsObject == null) return;
		
		//Get audio source component for sfx
		SFX = SoundsObject.GetComponent<AudioSource>();

		//Create damage texture
		DamageTexture = new Texture2D(1,1);
		DamageTexture.SetPixel(0,0,new Color(255,0,0,0.5f));
		DamageTexture.Apply();

		//Get Animator
		AnimComp = GetComponentInChildren<Animator>();

		//Get First person capsule and make non-visible
		MeshRenderer Capsule = GetComponentInChildren<MeshRenderer>();
		Capsule.enabled = false;

		//Get cached transform
		ThisTransform = transform;
	}
	//------------------------------------------------
	//ON GUI Function to show texture
	void OnGUI()
	{
		if(ShowDamage){GUI.DrawTexture(ScreenRect,DamageTexture);}
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

		//Play damage sound, if audio source is available
		if(SFX){SFX.PlayOneShot(DamageAudio, 1.0f);}

		//Wait for interval
		yield return new WaitForSeconds(DamageInterval);

		//Hide damage texture
		ShowDamage = false;
	}
	//------------------------------------------------
	//Equip next available weapon
	public void EquipNextWeapon()
	{
		//No weapon found yet
		bool bFoundWeapon = false;

		//Loop until weapon found
		while(!bFoundWeapon)
		{
			//Get next weapon
			ActiveWeapon = ActiveWeapon.NextWeapon;

			//Activate weapon, if possible
			ActiveWeapon.gameObject.SendMessage("Equip", ActiveWeapon.Type);

			//Is successfully equipped?
			bFoundWeapon = ActiveWeapon.IsEquipped;
		}
	}
	//------------------------------------------------
	//Event called when ammo expires
	public void AmmoExpired(Component Sender)
	{
		//Ammo expired for this weapon. Equip next
		EquipNextWeapon();
	}
	//------------------------------------------------
	void Update()
	{
		//Build screen rect on update (in case screen size changes)
		ScreenRect.x = ScreenRect.y = 0;
		ScreenRect.width = Screen.width;
		ScreenRect.height = Screen.height;

		if(Input.GetKeyDown(KeyCode.Period))
			EquipNextWeapon();
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
	//Function called when input status changed
	public void InputChanged(Component Sender)
	{
		//Get Input Status
		bool InputStatus = GameManager.Instance.InputAllowed;

		//Set controller input enabled status
		foreach(MonoBehaviour MB in FPSInputComponents)
			MB.enabled = InputStatus;
	}
	//------------------------------------------------
}
