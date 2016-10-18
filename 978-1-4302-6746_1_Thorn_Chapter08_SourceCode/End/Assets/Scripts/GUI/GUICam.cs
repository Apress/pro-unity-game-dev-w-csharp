//-------------------------------------------------------------
using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
//-------------------------------------------------------------
public class GUICam : MonoBehaviour
{
	//Camera Component
	private Camera Cam = null;
	
	//Pixel to World Scale
	public float PixelToWorldScale = 200.0f;

	//Cached transform for camera
	private Transform ThisTransform = null;
	//-------------------------------------------------------------
	// Use this for initialization
	void Start () 
	{
		//Get camera component for GUI
		Cam = GetComponent<Camera>();
		
		//Get camera transform
		ThisTransform = transform;
	}
	//-------------------------------------------------------------
	// Update is called once per frame
	void Update () 
	{
		//Update camera size
		Cam.orthographicSize = Screen.height/2/PixelToWorldScale;
		
		//Offset camera so top-left of screen is position (0,0) for game objects
		ThisTransform.localPosition = new Vector3(Screen.width/2/PixelToWorldScale, -(Screen.height/2/PixelToWorldScale), ThisTransform.localPosition.z);
	}
	//-------------------------------------------------------------
}
//-------------------------------------------------------------