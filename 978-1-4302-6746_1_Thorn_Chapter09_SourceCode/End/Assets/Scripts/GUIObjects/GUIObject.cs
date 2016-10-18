//------------------------------------------------
using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
//------------------------------------------------
public class GUIObject : MonoBehaviour
{
	[System.Serializable]
	public class PixelPadding
	{
		public float LeftPadding;
		public float RightPadding;
		public float TopPadding;
		public float BottomPadding;
	}

	//Pixel Padding
	public PixelPadding Padding;
	
	//HALIGN
	public enum HALIGN {left=0, right=1};

	//VALIGN
	public enum VALIGN {top=0, bottom=1};

	//Alignment
	public HALIGN HorzAlign = HALIGN.left;
	public VALIGN VertAlign = VALIGN.top;

	//Reference to GUICamera for this object
	public GUICam GUICamera = null;

	//Reference to cached transform
	private Transform ThisTransform = null;

	//------------------------------------------------
	// Use this for initialization
	void Start () 
	{
		//Get cached transform
		ThisTransform = transform;
	}
	//------------------------------------------------
	// Update is called once per frame
	void Update ()
	{
		//Calculate position on-screen
		Vector3 FinalPosition = new Vector3(HorzAlign == HALIGN.left ? 0.0f : Screen.width,
		                                    VertAlign == VALIGN.top ? 0.0f : -Screen.height,
		                                    ThisTransform.localPosition.z);

		//Offset with padding
		FinalPosition = new Vector3(FinalPosition.x + (Padding.LeftPadding * Screen.width) - (Padding.RightPadding * Screen.width), FinalPosition.y - (Padding.TopPadding * Screen.height) + (Padding.BottomPadding * Screen.height), FinalPosition.z);

		//Convert to pixel scale
		FinalPosition = new Vector3(FinalPosition.x / GUICamera.PixelToWorldScale, FinalPosition.y / GUICamera.PixelToWorldScale, FinalPosition.z);

		//Update position
		ThisTransform.localPosition = FinalPosition;
	}
	//------------------------------------------------
}
//------------------------------------------------