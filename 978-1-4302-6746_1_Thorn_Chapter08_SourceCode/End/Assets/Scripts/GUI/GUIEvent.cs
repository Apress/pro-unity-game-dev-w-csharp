//Posts notification when gui element is clicked
//------------------------------------------------
using UnityEngine;
using System.Collections;
//------------------------------------------------
public class GUIEvent : MonoBehaviour 
{
	//Notification to send when activated
	public string Notification = null;

	//Check for input
	void OnMouseDown()
	{
		GameManager.Notifications.PostNotification(this, Notification);
	}
}
//------------------------------------------------