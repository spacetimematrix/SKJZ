using UnityEngine;
using System.Collections;
using UnityEditor;


//需要对扩展的参数进行设置，然后再进行功能触发的，可以从这个类进行派生。
//它已经定制好了四个消息响应函数，开发者对其进行填充即可。

//(1) OnWizardUpdate  
//当扩展窗口打开时或用户对窗口的内容进行改动时，会调用此函数。
//一般会在这里面显示帮助文字和进行内容有效性验证；

//(2)OnWizardCreate 
//这是用户点击窗口的Create按钮时进行的操作，从ScriptableWizard的名字可以看出，
//这是一种类似向导的窗口 ，而这种窗口我们在Visual Studio中经常会使用到
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
        // 定制窗口标题和按钮，其中第二个参数是Create按钮，第三个则属于other按钮  
        // 如果不想使用other按钮，则可调用DisplayWizard的两参数版本  
        DisplayWizard<AddRemoveComponentsRecursivelyTool>("Add or remove components recursively", "Add", "Remove");
    }

    //在窗口可见时，每一帧都会调用这个函数。在其中进行Gizmos的绘制，也就是辅助编辑的线框体。
    //Unity的Gizmos类提供了DrawRayDrawLine ,DrawWireSphere ,DrawSphere ,DrawWireCube ,DrawCubeDrawIcon ,DrawGUITexture 功能
    // Called every frame when the wizard is visible.
    void OnDrawGizmos()
    {
        Debug.Log("OnDrawGizmos");
    }

    //Remove call This is called when the user clicks on the Create button.
    //这是用户点击窗口的Create按钮时进行的操作，从ScriptableWizard的名字可以看出，
    //这是一种类似向导的窗口 ，而这种窗口我们在Visual Studio中经常会使用到
    //只不过Unity3D中的ScriptableWizard窗口只能进行小于或等于两个按钮的定制，
    //一个就是所谓的Create按钮，另外一个则笼统称之为Other按钮。
    //ScriptableWizard.DisplayWizard这个静态函数用于对ScriptableWizard窗口标题和按钮名字的定制。
    void OnWizardCreate()
    {
        Debug.Log("OnWizardCreate");   
    }

    //ScriptableWizard窗口最多可以定制两个按钮，
    //一个是Create，另外一个称之为Other，这个函数会在other按钮被点击时调用。
    //下面是一个使用ScriptableWizard进行编辑扩展的例子：
    //Add call This is called when the user clicks on the Create button.
    void OnWizardOtherButton()
    {
        Debug.Log("OnWizardOtherButton");  
    }

    //This is called when the wizard is opened or whenever the user changes something in the wizard.
    //当扩展窗口打开时或用户对窗口的内容进行改动时，会调用此函数。一般会在这里面显示帮助文字和进行内容有效性验证
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
