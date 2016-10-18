using UnityEngine;
using System.Collections;

public class Listener : MonoBehaviour, IListener
{
	private NotificationsManager NM = null;

	void Start()
	{
		//Get notifications manager
		NM = GetComponent<NotificationsManager>();

		//Add as listener
		NM.AddListener(this);
	}

	//Implement interface - called on event
	public void OnEventOccured(NotificationsManager.EVENT_TYPE EType = NotificationsManager.EVENT_TYPE.ON_ENEMYDESTROYED, int Param = 0)
	{
		Debug.Log ("My Event Called");
	}
}
