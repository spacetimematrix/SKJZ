using UnityEngine;
using System.Collections;
using UnityEditor;

public class TransformUtilitiesWindowTool : EditorWindow
{
    public int ToolBarOption = 0;
    public string[] ToolBarTexts = {"Align", "Copy", "Randomize", "Add noise"};

    [MenuItem("Window/TransformUtilitiesWindowTool %y")]
    static void Init()
    {
        var window = GetWindow<TransformUtilitiesWindowTool>();
        window.Show();
    }

    void OnGUI()
    {
        ToolBarOption = GUILayout.Toolbar(ToolBarOption, ToolBarTexts);
    }
}
