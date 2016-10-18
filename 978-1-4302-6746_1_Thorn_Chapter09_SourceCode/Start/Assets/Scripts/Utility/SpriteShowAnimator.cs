//This class maintains a collection of sprite objects as frames of animation
//It shows and hides those frames according to a set of playback settings
//--------------------------------------------------------------
using UnityEngine;
using System.Collections;
//--------------------------------------------------------------
public class SpriteShowAnimator : MonoBehaviour
{
	//--------------------------------------------------------------
	//Playback types - run once or loop forever
	public enum ANIMATOR_PLAYBACK_TYPE {PLAYONCE = 0, PLAYLOOP = 1};

	//Playback type for this animation
	public ANIMATOR_PLAYBACK_TYPE PlaybackType = ANIMATOR_PLAYBACK_TYPE.PLAYONCE;

	//Frames per second to play for this animation
	public int FPS = 5;

	//Custom ID for animation - used with function PlayObjectAnimation
	public int AnimationID = 0;

	//Frames of animation
	public SpriteRenderer[] Sprites = null;

	//Should auto-play?
	public bool AutoPlay = false;

	//Should first hide all sprite renderers on playback? or leave at defaults
	public bool HideSpritesOnStart = true;

	//Boolean indicating whether animation is currently playing
	bool IsPlaying = false;
	//--------------------------------------------------------------
	void Start()
	{
		//Should we auto-play at start up?
		if(AutoPlay){StartCoroutine(PlaySpriteAnimation(AnimationID));}
	}
	//--------------------------------------------------------------
	//Function to run animation
	public IEnumerator PlaySpriteAnimation(int AnimID = 0)
	{
		//Check if this animation should be started
		if(AnimID!= AnimationID) yield break;

		//Should hide all sprite renderers?
		if(HideSpritesOnStart)
		{
			foreach(SpriteRenderer SR in Sprites)
				SR.enabled = false;
		}

		//Set is playing
		IsPlaying = true;

		//Calculate delay time
		float DelayTime = 1.0f/FPS;

		//Run animation at least once
		do
		{
			foreach(SpriteRenderer SR in Sprites)
			{
				SR.enabled = !SR.enabled;
				yield return new WaitForSeconds(DelayTime);
				SR.enabled = !SR.enabled;
			}
		}
		while(PlaybackType == ANIMATOR_PLAYBACK_TYPE.PLAYLOOP);

		//Stop animation
		StopSpriteAnimation(AnimationID);
	}
	//--------------------------------------------------------------
	//Function to stop animation
	public void StopSpriteAnimation(int AnimID = 0)
	{
		//Check if this animation can and should be stopped
		if((AnimID!= AnimationID) || (!IsPlaying)) return;

		//Stop all coroutines (animation will no longer play)
		StopAllCoroutines();

		//Is playing false
		IsPlaying = false;

		//Send Sprite Animation stopped event to gameobject
		gameObject.SendMessage("SpriteAnimationStopped", AnimID, SendMessageOptions.DontRequireReceiver);
	}
	//--------------------------------------------------------------
}
//--------------------------------------------------------------
