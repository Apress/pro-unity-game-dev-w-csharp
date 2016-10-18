using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour 
{
	private Transform ThisTransform = null;

	// Use this for initialization
	void Start () 
	{
		//Cache transform
		ThisTransform = transform;
	}
	
	void LateUpdate()
	{
		//Billboard sprite
		Vector3 LookAtDir = new Vector3 (Camera.main.transform.position.x - ThisTransform.position.x, 0, Camera.main.transform.position.z - ThisTransform.position.z);
		ThisTransform.rotation = Quaternion.LookRotation(LookAtDir.normalized, Vector3.up);
	}
}
