using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MyPlayer))]


//对某自定义组件进行观察的Inspector窗口，可以从它派生
public class OneInspectorTool : Editor
{
    private SerializedProperty damageProperty;
    private SerializedProperty armorProperty;
    private SerializedProperty gunProperty;

    private void OnEnable()
    {
        damageProperty = serializedObject.FindProperty("damage");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.IntSlider(damageProperty, 0, 100, new GUIContent("Damage"));
        if (!damageProperty.hasMultipleDifferentValues)
        {
            ProgressBar(damageProperty.intValue / 100.0f, "Damage");
        }
    }

    void ProgressBar(float value, string label)
    {
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rect, value, label);
        EditorGUILayout.Space();
    }
	
}
