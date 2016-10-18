using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NotificationsManager : MonoBehaviour
{
	//Define even types here...
	public enum EVENT_TYPE {ON_ENEMYDESTROYED = 0, ON_LEVELRESTARTED = 1, ON_POWERUPCOLLECTED = 2, ON_KEYPRESS=3};

	//Declare listener delegate
	public delegate void ListenerDelegate(NotificationsManager.EVENT_TYPE EType, int Param);

	//Array of listener delegates
	private List<ListenerDelegate> Listeners = new List<ListenerDelegate>();

	//Add listener
	public void AddListener(ListenerDelegate Listener)
	{
		Listeners.Add(Listener);
	}

	void PostNotification(NotificationsManager.EVENT_TYPE EType, int Param)
	{
		//Notify all listeners
		for(int i=0; i<Listeners.Count; i++)
		{
			//Call delegate like function
			Listeners[i](EType, Param);
		}
	}

	void Update()
	{
		//Notify event system on keypress
		if(Input.anyKeyDown)
			PostNotification(EVENT_TYPE.ON_KEYPRESS,0);
	}
}
