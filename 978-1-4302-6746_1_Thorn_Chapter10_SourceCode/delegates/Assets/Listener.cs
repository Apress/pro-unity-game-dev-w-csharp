using UnityEngine;
using System.Collections;

public class Listener : MonoBehaviour
{
	private NotificationsManager NM = null;
	
	void Start()
	{
		//Get notifications manager
		NM = GetComponent<NotificationsManager>();
		
		//Add as listener
		NM.AddListener(OnEventCall);
	}

	//Function prototype matches delegate
	public void OnEventCall(NotificationsManager.EVENT_TYPE EType, int Param)
	{
		Debug.Log("Event Called");
	}
}
