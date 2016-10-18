//Sets color for all child sprite renderers in a gameobject
//------------------------------------------------
using UnityEngine;
using System.Collections;
//------------------------------------------------
public class PingPongSpriteColor : MonoBehaviour
{
	//Source (from) color
	public Color Source = Color.white;

	//Destination (to) color
	public Color Dest = Color.white;

	//Custom ID for this animation
	public int AnimationID = 0;

	//Total time in seconds to transition from source to dest
	public float TransitionTime = 1.0f;
	
	//List of sprite renders whose color must be set
	private SpriteRenderer[] SpriteRenderers = null;
	
	//------------------------------------------------
	// Use this for initialization
	void Start () 
	{
		//Get all child sprite renderers
		SpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
	}
	//------------------------------------------------
	public void PlayColorAnimation(int AnimID = 0)
	{
		//If Anim ID numbers do not match, then exit - should not play this animation
		if(AnimationID != AnimID) return;

		//Stop all running coroutines
		StopAllCoroutines();

		//Start new sequence
		StartCoroutine(PlayLerpColors());
	}
	//------------------------------------------------
	//Start animation
	private IEnumerator PlayLerpColors()
	{
		//Lerp colors
		yield return StartCoroutine(LerpColor(Source, Dest));
		yield return StartCoroutine(LerpColor(Dest, Source));
	}
	//------------------------------------------------
	//Function to lerp over time, from Color X to Color Y
	private IEnumerator LerpColor(Color X, Color Y)
	{
		//Maintain elapsed time
		float ElapsedTime = 0.0f;

		//Loop for transition time
		while(ElapsedTime <= TransitionTime)
		{
			//Update Elapsed time
			ElapsedTime += Time.deltaTime;

			//Set sprite renderer colors
			foreach(SpriteRenderer SR in SpriteRenderers)
				SR.color = Color.Lerp(X, Y, Mathf.Clamp(ElapsedTime/TransitionTime, 0.0f, 1.0f));

			//Wait until next frame
			yield return null;
		}

		//Set dest color
		foreach(SpriteRenderer SR in SpriteRenderers)
			SR.color = Y;
	}
	//------------------------------------------------
}
