//********************************************************************
// 文件名: ApplicationEditor.cs
// 描述: 菜单命令打开场景
// 作者: U-xia
// 创建时间: 2015-03-26
//
// 修改历史:
// 2015-03-26 U-xia创建
//********************************************************************

using UnityEngine;
using System.Collections;
using UnityEditor;

public class ApplicationEditor : Editor
{
	
	#region OepnScene
	enum SceneType
	{
		Login = 0,
		Main = 1,
		Test = 2,

		Max
	}

	[SerializeField]
	private static SceneType currentSceneType = SceneType.Max;


	[MenuItem("OepnScene/Login/Open")]
	public static void OpenLoginScene()
	{
		if(currentSceneType != SceneType.Login)
		{
			Open(SceneType.Login);
		}
	}


	[MenuItem("OepnScene/Login/Open Run")]
	public static void OpenLoginRunScene()
	{
		OpenLoginScene ();

		if(!EditorApplication.isPlaying)
		{
			EditorApplication.isPlaying = true;
		}
	}


	[MenuItem("OepnScene/Main/Open")]
	public static void OpenMainScene()
	{
		if(currentSceneType != SceneType.Main)
		{
			Open(SceneType.Main);
		}
	}


	[MenuItem("OepnScene/Main/Open Run")]
	public static void OpenMainRunScene()
	{
		if(currentSceneType != SceneType.Main)
		{
			Open(SceneType.Main);
		}
		
		if(!EditorApplication.isPlaying)
		{
			EditorApplication.isPlaying = true;
		}
	}
	
	
	[MenuItem("OepnScene/Test")]
	public static void OpenTestScene()
	{
		if(currentSceneType != SceneType.Test)
		{
			Open(SceneType.Test);
		}
	}


	private static void Open(SceneType sceneType)
	{
		if(EditorApplication.SaveCurrentSceneIfUserWantsTo())
		{
			currentSceneType = sceneType;
		
			int sceneId = (int)sceneType;

			if(sceneId >= 0 && sceneId < EditorBuildSettings.scenes.Length)
			{
				EditorApplication.OpenScene (EditorBuildSettings.scenes [sceneId].path);
			}
		}
	}
	#endregion

    #region AddChild
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
    #endregion
}
