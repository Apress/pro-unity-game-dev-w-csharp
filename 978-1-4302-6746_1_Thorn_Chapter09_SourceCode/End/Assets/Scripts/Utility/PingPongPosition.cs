using UnityEngine;
using System.Collections;
//--------------------------------------------------------------
public class PingPongPosition : MonoBehaviour 
{
	//Start and End positions
	public Vector3 StartPosition = Vector3.zero;
	public Vector3 EndPosition = Vector3.zero;
	
	//Total time to complete 1 move
	public float MoveTime = 1.0f;
	
	//Cached Transform
	private Transform ThisTransform = null;
	//--------------------------------------------------------------
	// Use this for initialization
	IEnumerator Start () 
	{
		//Get Transform
		ThisTransform = transform;
		
		//Set start position
		ThisTransform.position = StartPosition;
		
		//Loop forever, back and forth
		while(true)
		{
			yield return StartCoroutine(MoveToTarget(StartPosition, EndPosition, MoveTime));
			yield return StartCoroutine(MoveToTarget(EndPosition, StartPosition, MoveTime));
		}
	}
	//--------------------------------------------------------------
	IEnumerator MoveToTarget(Vector3 StartVec, Vector3 EndVec, float TotalTime = 1.0f)
	{
		//Set elapsed time
		float ElapsedTime = 0.0f;
		
		//Repeat until time over
		while(ElapsedTime <= TotalTime)
		{
			//Update time
			ElapsedTime += Time.deltaTime;
			
			//Update transform
			ThisTransform.position = Vector3.Lerp(StartVec, EndVec, ElapsedTime/TotalTime);
			
			//Wait until next frame
			yield return null;
		}
	}
	//--------------------------------------------------------------
}
