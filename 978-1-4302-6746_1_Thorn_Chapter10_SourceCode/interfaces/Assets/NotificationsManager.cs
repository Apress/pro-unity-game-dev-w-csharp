//------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//------------------------------------
public interface IListener
{
	//Event called through listener interface
	void OnEventOccured(NotificationsManager.EVENT_TYPE EType = NotificationsManager.EVENT_TYPE.ON_ENEMYDESTROYED, int Param = 0);
}
//------------------------------------
public class NotificationsManager : MonoBehaviour
{
	//Define even types here...
	public enum EVENT_TYPE {ON_ENEMYDESTROYED = 0, ON_LEVELRESTARTED = 1, ON_POWERUPCOLLECTED = 2, ON_KEYPRESS=3};

	//Collection of listeners
	private List<IListener> Listeners = new List<IListener>();
	//------------------------------------
	//Function to add listener
	public void AddListener(IListener lObject)
	{
		//Add listener to list
		Listeners.Add(lObject);
	}
	//------------------------------------
	void PostEvent(EVENT_TYPE EType = EVENT_TYPE.ON_ENEMYDESTROYED, int Param = 0)
	{
		//Notify all listeners
		for(int i=0; i<Listeners.Count; i++)
			Listeners[i].OnEventOccured(EType, Param);
	}
	//------------------------------------
	void Update()
	{
		if(Input.anyKeyDown)
		{
			//Post key press event to all listeners
			PostEvent(EVENT_TYPE.ON_KEYPRESS,0);
		}
	}
	//------------------------------------
}
//------------------------------------
