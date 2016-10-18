using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class GUILabel : MonoBehaviour 
{
	//Content for label
	public GUIContent LabelData;

	//Style for label
	public GUIStyle LabelStyle;

	//Rect for label
	public Rect LabelRegion;

	//Draw label
	void OnGUI()
	{
		Rect FinalRect = new Rect(LabelRegion.x * Screen.width, LabelRegion.y * Screen.height, LabelRegion.width * Screen.width, LabelRegion.height * Screen.height);

		GUI.Label(FinalRect, LabelData, LabelStyle);
	}
}