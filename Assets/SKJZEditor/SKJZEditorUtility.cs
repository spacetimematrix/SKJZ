//********************************************************************
// 文件名: SKJZEditorUtility.cs
// 描述: Unity编辑器扩展工具类
// 作者: U-xia
// 创建时间: 2015-02-13
//
// 修改历史:
// 2015-00-00 U-xia 创建 添加OpenFolderPanel(); OpenFilePanel();
//********************************************************************


using System.IO;
using System.Text;
using UnityEditor;


public class SKJZEditorUtility 
{
    /// <summary>
    /// 打开文件夹
    /// </summary>
    /// <param name="windowTitle">面板标题</param>
    /// <param name="path">文件夹路径</param>
    /// <param name="defaultFolder">选择的默认文件夹</param>
    /// <returns>选择的文件路径</returns>
    public static string OpenFolderPanel(string windowTitle, string path, string defaultFolder)
    {
        var pathTemp = string.IsNullOrEmpty(path) ? string.Empty : Path.GetDirectoryName(path);

        if (!string.IsNullOrEmpty(pathTemp) && Path.IsPathRooted(pathTemp))
        {
            string[] arr = pathTemp.Split(Path.AltDirectorySeparatorChar);

            var stringBuilder = new StringBuilder();

            for (int i = 0; i < arr.Length - 1; i++)
            {
                stringBuilder.Append(arr[i]);
                stringBuilder.Append(Path.AltDirectorySeparatorChar);
            }

            pathTemp = stringBuilder.ToString();
        }

        pathTemp = EditorUtility.OpenFolderPanel(windowTitle, pathTemp, defaultFolder);

        return string.IsNullOrEmpty(pathTemp) ? path : pathTemp;
    }


    /// <summary>
    /// 打开文件
    /// </summary>
    /// <param name="windowTitle">面板标题</param>
    /// <param name="path">文件夹路径</param>
    /// <param name="extension">默认后缀</param>
    /// <returns>选择的文件路径</returns>
    public static string OpenFilePanel(string windowTitle, string path, string extension)
    {
        var pathTemp = string.IsNullOrEmpty(path) ? string.Empty : Path.GetDirectoryName(path);

        pathTemp = EditorUtility.OpenFilePanel(windowTitle, pathTemp, extension);

        return string.IsNullOrEmpty(pathTemp) ? path : pathTemp;
    }
}
