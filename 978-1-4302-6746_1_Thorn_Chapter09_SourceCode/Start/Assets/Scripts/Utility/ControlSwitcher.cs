//------------------------------------------------
using UnityEngine;
using System.Collections;
//------------------------------------------------
public class ControlSwitcher : MonoBehaviour
{
	//------------------------------------------------
	//Reference to desktop first person controller (default)
	public GameObject DesktopFirstPerson = null;
	
	//Reference to mobile first person controller
	public GameObject MobileFirstPerson = null;
	
	//------------------------------------------------
	//Select appropriate first person control for platform
	void Awake()
	{
		//If mobile platform, then use mobile first person controller
		#if UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8
		DesktopFirstPerson.SetActive(false);
		MobileFirstPerson.SetActive(true);
		#endif
	}
	//------------------------------------------------
}
