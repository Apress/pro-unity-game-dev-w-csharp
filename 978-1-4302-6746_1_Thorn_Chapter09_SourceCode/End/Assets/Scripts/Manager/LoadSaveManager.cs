//Loads and Saves game state data to and from xml file
//-----------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml; 
using System.Xml.Serialization; 
using System.IO;
//-----------------------------------------------
public class LoadSaveManager : MonoBehaviour
{
	//Save game data
	[XmlRoot("GameData")]
	public class GameStateData
	{
		//-----------------------------------------------
		public struct DataTransform
		{
			public float X;
			public float Y;
			public float Z;
			public float RotX;
			public float RotY;
			public float RotZ;
			public float ScaleX;
			public float ScaleY;
			public float ScaleZ;
		}
		//-----------------------------------------------
		//Data for enemy
		public class DataEnemy
		{
			//Enemy Transform Data
			public DataTransform PosRotScale;
			//Enemy ID
			public int EnemyID;
			//Health
			public int Health;
		}
		//-----------------------------------------------
		//Data for player
		public class DataPlayer
		{
			//Transform Data
			public DataTransform PosRotScale;

			//Collected cash
			public float CollectedCash;

			//Has Collected Gun 01?
			public bool CollectedGun;

			//Health
			public int Health;
		}
		//-----------------------------------------------

		//Instance variables
		public List<DataEnemy> Enemies = new List<DataEnemy>();

		public DataPlayer Player = new DataPlayer();
	}

	//Game data to save/load
	public GameStateData GameState = new GameStateData();

	//-----------------------------------------------
	//Saves game data to XML file
	public void Save(string FileName = "GameData.xml")
	{
		//Clear existing enemy data
		GameState.Enemies.Clear();

		//Call save start notification
		GameManager.Notifications.PostNotification(this, "SaveGamePrepare");

		//Now save game data
		XmlSerializer Serializer = new XmlSerializer(typeof(GameStateData));
		FileStream Stream = new FileStream(FileName, FileMode.Create);
		Serializer.Serialize(Stream, GameState);
		Stream.Close();

		//Call save end notification
		GameManager.Notifications.PostNotification(this, "SaveGameComplete");
	}
	//-----------------------------------------------
	//Load game data from XML file
	public void Load(string FileName = "GameData.xml")
	{
		//Call load start notification
		GameManager.Notifications.PostNotification(this, "LoadGamePrepare");
		
		XmlSerializer Serializer = new XmlSerializer(typeof(GameStateData));
		FileStream Stream = new FileStream(FileName, FileMode.Open);
		GameState = Serializer.Deserialize(Stream) as GameStateData;
		Stream.Close();
		
		//Call load end notification
		GameManager.Notifications.PostNotification(this, "LoadGameComplete");
	}
	//-----------------------------------------------
}
//-----------------------------------------------