using UnityEngine;
using System.Collections;
using UnityEditor;

public class AddRemoveComponentsRecursivelyTool : ScriptableWizard
{
    public string componentType = null;
    public int i;


    [MenuItem("GameObject/Add or remove components recursively", true)]
    static bool CreateWindowDisabled()
    {
        return Selection.activeTransform;
    }

    [MenuItem("GameObject/Add or remove components recursively")]
    static void CreateWindow()
    {
        DisplayWizard<AddRemoveComponentsRecursivelyTool>("Add or remove components recursively", "Add", "Remove");
    }

    // Called every frame when the wizard is visible.
    void OnDrawGizmos()
    {
        Debug.Log("OnDrawGizmos");
    }

    //Remove call This is called when the user clicks on the Create button.
    void OnWizardCreate()
    {
        Debug.Log("OnWizardCreate");   
    }


    //Add call This is called when the user clicks on the Create button.
    void OnWizardOtherButton()
    {
        Debug.Log("OnWizardOtherButton");  
    }

    //This is called when the wizard is opened or whenever the user changes something in the wizard.
    void OnWizardUpdate()
    {
        Debug.Log("OnWizardUpdate");
        helpString = "This string describes what the Scriptable Wizard does.";
        isValid = false;
        if (!isValid)
        {
            errorString = "errorString bl bl bl";
        }
    }
}
