using UnityEngine;
using System.Collections;

public class Listener : MonoBehaviour
{
	//Reference to gloabl Notifications Manager
	public NotificationsManager Notifications = null;

	// Use this for initialization
	void Start () 
	{
		//Register this object as a listener for keyboard notifications
		if(Notifications!=null)
			Notifications.AddListener(this, "OnKeyboardInput");
	}

	//This function will be called by the NotificationsManager when keyboard events occur
	public void OnKeyboardInput(Component Sender)
	{
		//Print to console
		Debug.Log("Keyboard Event Occurred");
	}
}
