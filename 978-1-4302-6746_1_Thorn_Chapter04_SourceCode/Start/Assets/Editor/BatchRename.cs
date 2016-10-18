using UnityEngine;
using UnityEditor;
using System.Collections;

public class BatchRename : ScriptableWizard 
{
	//Base name
	public string BaseName = "MyObject_";
	
	//Start Count
	public int StartNumber = 0;
	
	//Increment
	public int Increment = 1;
	
	[MenuItem("Edit/Batch Rename...")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard("Batch Rename",typeof(BatchRename),"Rename");
    }
	
	//Called when the window first appears
	void OnEnable()
	{
		UpdateSelectionHelper();
	}
	
	//Function called when selection changes in scene
	void OnSelectionChange()
	{
		UpdateSelectionHelper();
	}
	
	//Update selection counter
	void UpdateSelectionHelper()
	{
		helpString = "";
		
		if (Selection.objects != null)
			helpString = "Number of objects selected: " + Selection.objects.Length;
	}
	
	//Rename
	void OnWizardCreate()
	{
		//If selection empty, then exit
		if (Selection.objects == null)
			return;
		
		//Current Increment
		int PostFix = StartNumber;
		
		//Cycle and rename
		foreach(Object O in Selection.objects)
		{
			O.name = BaseName + PostFix;
			PostFix += Increment;
		}
	}
}
