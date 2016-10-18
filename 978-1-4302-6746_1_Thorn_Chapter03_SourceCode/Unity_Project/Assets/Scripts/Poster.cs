using UnityEngine;
using System.Collections;

public class Poster : MonoBehaviour 
{
	//Reference to gloabl Notifications Manager
	public NotificationsManager Notifications = null;
	
	// Update is called once per frame
	void Update () 
	{
		//Check for keyboard input
		if(Input.anyKeyDown && Notifications != null)
			Notifications.PostNotification(this, "OnKeyboardInput");
	}
}
