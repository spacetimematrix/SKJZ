using UnityEngine;
using System.Collections;
using UnityEditor;


//最常见的小功能扩展，一般不用窗口的编辑扩展，可以从这个类中继承，如以下代码所示：
public class AddChildTool : ScriptableObject
{
    /*
    [MenuItem("GameObject/Add Child")]
    static void AddChild()
    {
        var transforms = Selection.GetTransforms(SelectionMode.TopLevel | SelectionMode.OnlyUserModifiable);

        foreach (var transform in transforms)
        {
            GameObject child = new GameObject("_child");
            child.transform.parent = transform;
        }
    }
     * */
	
}
