using UnityEngine;
using System.Collections;
using SKJZ.Core;
using System;

public class TableMgr : Singleton<TableMgr>
{
    public Action InitFinishCallback;
    public int I = 1;

    public TableMgr() { }
}
