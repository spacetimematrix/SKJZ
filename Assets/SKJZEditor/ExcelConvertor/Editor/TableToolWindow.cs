//********************************************************************
// 文件名: TableToolWindow.cs
// 描述: 将Execl表转为csv，并生成代码
// 作者: U-xia
// 创建时间: 2015-02-13
//
// 修改历史:
// 2015-02-13 U-xia创建,实现主要功能
//********************************************************************


using System;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using SKJZ.Core;
using SKJZ.Editor;


public class TableToolWindow : EditorWindow
{
    public static TableToolWindow window;
    
    [MenuItem("Window/SKJZ/TableWindow")]
    static void Init()
    {
        window = GetWindow<TableToolWindow>();
        window.Show();
        window.title = "TableTool";
        window.minSize = new Vector2(600, 600);

        Singleton<TableTool>.Instance.Init();
    }

    void OnGUI()
    {
        Singleton<TableTool>.Instance.DrewUI();
    }
}

public class TableTool :Singleton<TableTool>
{
    private const string TableSourceName = "表导入路径";
    private const string TableOutputName = "表导出路径";
    private const string CodeServerOutputName = "服务器代码导出路径";
    private const string CodeClientOutputName = "客户端代码导出路径";

    private const string OpenTablePanelTitle = "选择表";
    private const string SaveTablePanelTitle = "保存表";
    private const string SaveCodePanelTitle = "保存代码";

    private const string BtnNameFolder = "文件夹";
    private const string BtnNameFile = "文件";

    private const string DefaultExtension = "xlsx";

    private const string DefaultTableSourceFolderName = "Table";
    private const string DefaultTableOutputFolderName = "Cvs";
    private const string DefaultServerCodeSourceFolderName = "SeverCode";
    private const string DefaultClientCodeSourceFolderName = "ClientCode";


    private const string TableSourcePathKey = "TableTool_TableSourcePathKey";
    private const string TableOutputPathKey = "TableTool_TableOutputPathKey";
    private const string CodeServerOutputPathKey = "TableTool_CodeServerOutputPathKey";
    private const string CodeClientOutputPathKey = "TableTool_CodeClientOutputPathKey";

    private string _tableSourcePath = string.Empty;
    private string _tableOutputPath = string.Empty;
    private string _codeServerOutputPath = string.Empty;
    private string _codeClientOutputPath = string.Empty;

    private ItemData _tableSourceItemData;
    private ItemData _tableOutputItemData;
    private ItemData _tableServerCodeItemData;
    private ItemData _tableClientCodeItemData;


    public void Init()
    {
        //选择Excel表格
        _tableSourceItemData = new ItemData();
        _tableSourceItemData.LabelName = TableSourceName;
        _tableSourceItemData.Path = _tableSourcePath;
        _tableSourceItemData.BtnFolderName = BtnNameFolder;
        _tableSourceItemData.BtnFileName = BtnNameFile;
        _tableSourceItemData.BtnFolderCallback = DoTableSourceFolder;
        _tableSourceItemData.BtnFileCallback = DoTableSourceFile;
        _tableSourceItemData.IsBtnFileActive = true;
        _tableSourceItemData.DefaultFolderName = DefaultTableSourceFolderName;
        _tableSourceItemData.PlayerPrefsKey = TableSourcePathKey;

        _tableSourceItemData.Init();


        //导出csv表
        _tableOutputItemData = new ItemData();
        _tableOutputItemData.LabelName = TableOutputName;
        _tableOutputItemData.Path = _tableOutputPath;
        _tableOutputItemData.BtnFolderName = BtnNameFolder;
        _tableOutputItemData.BtnFileName = BtnNameFile;
        _tableOutputItemData.BtnFolderCallback = DoTableOutput;
        _tableOutputItemData.BtnFileCallback = null;
        _tableOutputItemData.IsBtnFileActive = false;
        _tableOutputItemData.DefaultFolderName = DefaultTableOutputFolderName;
        _tableOutputItemData.PlayerPrefsKey = TableOutputPathKey;

        _tableOutputItemData.Init();

        //为服务器导出代码
        _tableServerCodeItemData = new ItemData();
        _tableServerCodeItemData.LabelName = CodeServerOutputName;
        _tableServerCodeItemData.Path = _codeServerOutputPath;
        _tableServerCodeItemData.BtnFolderName = BtnNameFolder;
        _tableServerCodeItemData.BtnFileName = BtnNameFile;
        _tableServerCodeItemData.BtnFolderCallback = DoCodeServer;
        _tableServerCodeItemData.BtnFileCallback = null;
        _tableServerCodeItemData.IsBtnFileActive = false;
        _tableServerCodeItemData.DefaultFolderName = DefaultServerCodeSourceFolderName;
        _tableServerCodeItemData.PlayerPrefsKey = CodeServerOutputPathKey;

        _tableServerCodeItemData.Init();

        //为客户端导出代码
        _tableClientCodeItemData = new ItemData();
        _tableClientCodeItemData.LabelName = CodeClientOutputName;
        _tableClientCodeItemData.Path = _codeClientOutputPath;
        _tableClientCodeItemData.BtnFolderName = BtnNameFolder;
        _tableClientCodeItemData.BtnFileName = BtnNameFile;
        _tableClientCodeItemData.BtnFolderCallback = DoCodeClient;
        _tableClientCodeItemData.BtnFileCallback = null;
        _tableClientCodeItemData.IsBtnFileActive = false;
        _tableClientCodeItemData.DefaultFolderName = DefaultClientCodeSourceFolderName;
        _tableClientCodeItemData.PlayerPrefsKey = CodeClientOutputPathKey;

        _tableClientCodeItemData.Init();
    }


    //绘制UI
    public void DrewUI()
    {
        GUILayout.Space(2);

        GUILayout.BeginVertical();
        DrewItem(_tableSourceItemData);
        DrewItem(_tableOutputItemData);
        DrewItem(_tableServerCodeItemData);
        DrewItem(_tableClientCodeItemData);

        GUILayout.Space(100);
        DrewBtn();

        GUILayout.EndVertical();
    }


    /// <summary>
    /// 绘制一个单元
    /// </summary>
    /// <param name="labelName"></param>
    /// <param name="pathStr"></param>
    /// <param name="btnFolderCallback"></param>
    /// <param name="btnFileCallback"></param>
    private void DrewItem(ItemData itemData)
    {
        if (itemData == null) return;

        GUILayout.Space(1);

        GUILayout.BeginHorizontal();

        GUILayout.Label(itemData.LabelName, GUILayout.Width(70), GUILayout.Height(20));

        GUILayout.FlexibleSpace();
        itemData.Path = GUILayout.TextField(itemData.Path, GUILayout.Width(400), GUILayout.Height(20));

        GUILayout.FlexibleSpace();
        GUI.color = Color.green;
        if (GUILayout.Button(BtnNameFolder, GUILayout.Height(20)) && itemData.BtnFolderCallback != null)
        {
            itemData.BtnFolderCallback();
        }

        GUILayout.FlexibleSpace();
        if (!itemData.IsBtnFileActive)
        {
            GUI.color = Color.red;
        }

        if (GUILayout.Button(BtnNameFile, GUILayout.Height(20)) && itemData.BtnFileCallback != null && itemData.IsBtnFileActive)
        {
            itemData.BtnFileCallback();
        }

        GUI.color = Color.white;
        GUILayout.EndHorizontal();

        GUILayout.Space(1);
    }


    void DrewBtn()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("导出", GUILayout.Height(20)))
        {
            DoOkBtn();
        }
        if (GUILayout.Button("取消", GUILayout.Height(20)))
        {
            DoCancelBtn();
        }
        GUILayout.EndHorizontal();
    }

    void DoOkBtn()
    {
        Singleton<ExcelConvertor>.Instance.Excute(_tableSourceItemData.Path, _tableOutputItemData.Path, DefaultExtension);
    }

    void DoCancelBtn()
    {
        EditorWindow.GetWindow<TableToolWindow>().Close();
    }


    void DoTableSourceFolder()
    {
        _tableSourceItemData.Path = SKJZEditorUtility.OpenFolderPanel(OpenTablePanelTitle, _tableSourceItemData.Path, DefaultTableSourceFolderName);
    }

    void DoTableSourceFile()
    {
        _tableSourceItemData.Path = SKJZEditorUtility.OpenFilePanel(OpenTablePanelTitle, _tableSourceItemData.Path, DefaultExtension);
    }

    void DoTableOutput()
    {
        _tableOutputItemData.Path = SKJZEditorUtility.OpenFolderPanel(SaveTablePanelTitle, _tableOutputItemData.Path, DefaultTableOutputFolderName);
    }


    void DoCodeServer()
    {
        _tableServerCodeItemData.Path = SKJZEditorUtility.OpenFolderPanel(CodeServerOutputName, _tableServerCodeItemData.Path, DefaultServerCodeSourceFolderName);
    }

    private void DoCodeClient()
    {
        _tableClientCodeItemData.Path = SKJZEditorUtility.OpenFolderPanel(CodeClientOutputName, _tableClientCodeItemData.Path, DefaultClientCodeSourceFolderName);
    }

    public class ItemData
    {
        public string LabelName;

        private string _path;

        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;

                WritePlayerPrefs();
            }
        }
        public string BtnFolderName;
        public string BtnFileName;
        public Action BtnFolderCallback;
        public Action BtnFileCallback;
        public bool IsBtnFileActive;
        public string DefaultFolderName;
        public string PlayerPrefsKey;

        public void Init()
        {
            ReadPlayerPrefs();
        }


        //读取上次使用的路径
        private void ReadPlayerPrefs()
        {
            _path = PlayerPrefs.GetString(PlayerPrefsKey, _path);
        }


        /// <summary>
        /// 记录上次使用的路径
        /// </summary>
        public void WritePlayerPrefs()
        {
            PlayerPrefs.SetString(PlayerPrefsKey, _path);
        }
    }

}