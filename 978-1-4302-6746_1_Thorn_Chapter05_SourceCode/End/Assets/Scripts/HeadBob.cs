//------------------------------------------------
//Class to make first person camera bob gently up and down while walking
using UnityEngine;
using System.Collections;
//------------------------------------------------
public class HeadBob : MonoBehaviour
{
	//Strength of head bob - amplitude of sine wave
	public float Strength = 1.0f;
	
	//Frequency of wave
	public float BobAmount = 2.0f;
	
	//Neutral head height position
	public float HeadY = 1.0f;
	
	//Cached transform
	private Transform ThisTransform = null;
	
	//Elapsed Time since movement
	private float ElapsedTime = 0.0f;
	
	//------------------------------------------------
	void Start()
	{
		//Get transform
		ThisTransform = transform;
	}
	//------------------------------------------------
	// Update is called once per frame
	void Update ()
	{
		//If input is not allowed, then exit
		if(!GameManager.Instance.InputAllowed) return;
		
		//Get player movement if input allowed
		float horizontal = Mathf.Abs(Input.GetAxis("Horizontal"));
		float vertical = Mathf.Abs(Input.GetAxis("Vertical"));
		
		//Total movement
		float TotalMovement = Mathf.Clamp(horizontal + vertical,0.0f,1.0f);
		
		//Update elapsed time
		ElapsedTime = (TotalMovement > 0.0f) ? ElapsedTime += Time.deltaTime : 0.0f;
		
		//Y Offset for headbo
		float YOffset = Mathf.Sin (ElapsedTime * BobAmount) * Strength;
		
		//Create position
		Vector3 PlayerPos = new Vector3(ThisTransform.position.x, HeadY + YOffset * TotalMovement, ThisTransform.position.z);
		
		//Update position
		ThisTransform.position = PlayerPos;
	}
	//------------------------------------------------
}