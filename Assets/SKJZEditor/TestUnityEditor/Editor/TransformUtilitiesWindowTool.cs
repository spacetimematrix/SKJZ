using UnityEngine;
using System.Collections;
using UnityEditor;


//较复杂的功能，需要多个灵活的控件，实现自由浮动和加入其他窗口的tab
//可以从这个类派生，这种窗口的窗体功能和Scene，Hierarchy等窗口完全一致
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
