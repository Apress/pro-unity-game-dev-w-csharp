//EVENTS MANAGER CLASS - for receiving notifications and notifying listeners
//------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//------------------------------------------------
public class NotificationsManager : MonoBehaviour
{
	//Private variables
	//------------------------------------------------
	//Internal reference to all listeners for notifications
	private Dictionary<string, List<Component>> Listeners = new Dictionary<string, List<Component>>();
	
	//Methods
	//------------------------------------------------
	//Function to add a listener for an notification to the listeners list
	public void AddListener(Component Sender, string NotificationName)
	{
		//Add listener to dictionary
		if(!Listeners.ContainsKey(NotificationName))
			Listeners.Add (NotificationName, new List<Component>());
		
		//Add object to listener list for this notification
		Listeners[NotificationName].Add(Sender);
	}
	//------------------------------------------------
	//Function to remove a listener for a notification
	public void RemoveListener(Component Sender, string NotificationName)
	{
		//If no key in dictionary exists, then exit
		if(!Listeners.ContainsKey(NotificationName))
			return;
		
		//Cycle through listeners and identify component, and then remove
		for(int i = Listeners[NotificationName].Count-1; i>=0; i--) 
		{
			//Check instance ID
			if(Listeners[NotificationName][i].GetInstanceID() == Sender.GetInstanceID())
				Listeners[NotificationName].RemoveAt(i); //Matched. Remove from list
		}
	}
	//------------------------------------------------
	//Function to post a notification to a listener
	public void PostNotification(Component Sender, string NotificationName)
	{
		//If no key in dictionary exists, then exit
		if(!Listeners.ContainsKey(NotificationName))
			return;
		
		//Else post notification to all matching listeners
		foreach(Component Listener in Listeners[NotificationName])
			Listener.SendMessage(NotificationName, Sender, SendMessageOptions.DontRequireReceiver);
	}
	//------------------------------------------------
	//Function to clear all listeners
	public void ClearListeners()
	{
		//Removes all listeners
		Listeners.Clear();
	}
	//------------------------------------------------
	//Function to remove redundant listeners - deleted and removed listeners
	public void RemoveRedundancies()
	{
		//Create new dictionary
		Dictionary<string, List<Component>> TmpListeners = new Dictionary<string, List<Component>>();
			
		//Cycle through all dictionary entries
		foreach(KeyValuePair<string, List<Component>> Item in Listeners)
		{
			//Cycle through all listener objects in list, remove null objects
			for(int i = Item.Value.Count-1; i>=0; i--)
			{
				//If null, then remove item
				if(Item.Value[i] == null)
					Item.Value.RemoveAt(i);
			}
			
			//If items remain in list for this notification, then add this to tmp dictionary
			if(Item.Value.Count > 0)
				TmpListeners.Add (Item.Key, Item.Value);
		}
		
		//Replace listeners object with new, optimized dictionary
		Listeners = TmpListeners;
	}
	//------------------------------------------------
	//Called when a new level is loaded; remove redundant entries from dictionary; in case left-over from previous scene
	void OnLevelWasLoaded()
	{
		//Clear redundancies
		RemoveRedundancies();
	}
	//------------------------------------------------
}