//--------------------------------------------------------------
//Class for menu functionality
using UnityEngine;
using System.Collections;
//--------------------------------------------------------------
public class GUIOptions : MonoBehaviour 
{
	//Sprite Renderer for menu
	private SpriteRenderer SR = null;

	//Collision objects for buttons
	private BoxCollider[] Colliders = null;

	//--------------------------------------------------------------
	// Use this for initialization
	void Start () 
	{
		//Get sprite renderer
		SR = GetComponent<SpriteRenderer>();

		//Get button colliders
		Colliders = GetComponentsInChildren<BoxCollider>();

		//Add listeners
		GameManager.Notifications.AddListener(this, "ShowOptions");
		GameManager.Notifications.AddListener(this, "HideOptions");

		//Hide menu on startup
		HideOptions(null);
	}
	//--------------------------------------------------------------
	//Hide options event
	public void HideOptions(Component Sender)
	{
		SetOptionsVisible(false);
	}
	//--------------------------------------------------------------
	//Show options event
	public void ShowOptions(Component Sender)
	{
		SetOptionsVisible();
	}
	//--------------------------------------------------------------
	//Function to show/hide options
	private void SetOptionsVisible(bool bShow = true)
	{
		//If enabling, then pause game - else resume
		Time.timeScale = (bShow) ? 0.0f : 1.0f;

		//Enable/Disable input
		GameManager.Instance.InputAllowed = !bShow;

		//Show/Hide menu graphics
		SR.enabled = bShow;

		//Enable/Disable button colliders
		foreach(BoxCollider B in Colliders)
			B.enabled = bShow;
	}
	//--------------------------------------------------------------
	//Watch escape key input
	void Update()
	{
		//If escape key pressed
		if(Input.GetKeyDown(KeyCode.Escape))
			SetOptionsVisible(!SR.enabled);
	}
	//--------------------------------------------------------------
}