//GameManager
//Singleton and persistent object to manage game state
//For high level control over game
//--------------------------------------------------------------
using UnityEngine;
using System.Collections;
//Game Manager requires other manager components
[RequireComponent (typeof (NotificationsManager))] //Component for sending and receiving notifications
//--------------------------------------------------------------
public class GameManager : MonoBehaviour
{
	//--------------------------------------------------------------
	//public properties
	//C# property to retrieve currently active instance of object, if any
	public static GameManager Instance
	{
		get
		{
			if (instance == null) instance = new GameObject ("GameManager").AddComponent<GameManager>(); //create game manager object if required
			return instance;
		}
	}
	//--------------------------------------------------------------
	//C# property to retrieve notifications manager
	public static NotificationsManager Notifications
	{
		get
		{
			if(notifications == null) notifications =  instance.GetComponent<NotificationsManager>();
			return notifications;
		}
	}
	//--------------------------------------------------------------
	//Private variables
	//--------------------------------------------------------------
	//Internal reference to single active instance of object - for singleton behaviour
	private static GameManager instance = null;

	//Internal reference to notifications object
	private static NotificationsManager notifications = null;
	
	//public variables
	//--------------------------------------------------------------
	// Called before Start on object creation
	void Awake ()
	{	
		//Check if there is an existing instance of this object
		if((instance) && (instance.GetInstanceID() != GetInstanceID()))
			DestroyImmediate(gameObject); //Delete duplicate
		else
		{
			instance = this; //Make this object the only instance
			DontDestroyOnLoad (gameObject); //Set as do not destroy
		}
	}
	//--------------------------------------------------------------
	// Use this for initialization
	void Start () 
	{
		//Add cash collected listener to listen for win condition
		Notifications.AddListener(this, "CashCollected");
	}
	//--------------------------------------------------------------
	//Function called when all cash is collected in level
	public void CashCollected(Component Sender)
	{
	}
	//--------------------------------------------------------------
	//Restart Game
	public void RestartGame()
	{
		//Load first level
		Application.LoadLevel(0);
	}
	//--------------------------------------------------------------
	//Exit Game
	public void ExitGame()
	{
		Application.Quit();
	}
	//--------------------------------------------------------------
}
