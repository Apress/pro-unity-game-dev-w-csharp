using UnityEngine;
using System.Collections;
//--------------------------------------------------------------
public class PingPongDistance : MonoBehaviour
{
	//Direction to move
	public Vector3 MoveDir = Vector3.zero;

	//Speed to move - units per second
	public float Speed = 0.0f;

	//Distance to travel in world units (before inverting direction and turning back)
	public float TravelDistance = 0.0f;
	
	//Cached Transform
	private Transform ThisTransform = null;

	//--------------------------------------------------------------
	// Use this for initialization
	IEnumerator Start () 
	{
		//Get cached transform
		ThisTransform = transform;

		//Loop forever
		while(true)
		{
			//Invert direction
			MoveDir = MoveDir * -1;

			//Start movement
			yield return StartCoroutine(Travel());
		}
	}
	//--------------------------------------------------------------
	//Travel full distance in direction, from current position
	IEnumerator Travel()
	{
		//Distance travelled so far
		float DistanceTravelled = 0;

		//Move
		while(DistanceTravelled < TravelDistance)
		{
			//Get new position based on speed and direction
			Vector3 DistToTravel = MoveDir * Speed * Time.deltaTime;

			//Update position
			ThisTransform.position += DistToTravel;

			//Update distance travelled so far
			DistanceTravelled += DistToTravel.magnitude;

			//Wait until next update
			yield return null;
		}
	}
	//--------------------------------------------------------------
}
