using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.IMGUI.Controls;
using UnityEditor;
using System.IO;

public class CellMapDataFileTreeView : TreeView
{
    //int curId;
    public CellMapDataFileTreeView(TreeViewState state, CellMapEditor parent) : base(state)
    {
        m_parent = parent;
        showBorder = true;
        Reload();
    }
    private void buildFolderView(CellMapFileItem parent, DirectoryInfo parentDi, int curDepth)
    {
        var arrDi = parentDi.GetDirectories();
        foreach (var di in arrDi)
        {
            var diView = new CellMapFileItem { id = ++m_curId, depth = 0, displayName = di.Name, m_fdi = di, m_resPath = parent.m_resPath + di.Name + "/" };
            parent.AddChild(diView);
            buildFolderView(diView, di, curDepth + 1);
        }
        var arrFi = parentDi.GetFiles("*.json");
        foreach (var fi in arrFi)
        {
            parent.AddChild(new CellMapFileItem { id = ++m_curId, depth = curDepth + 1, displayName = fi.Name, m_fdi = fi, m_resPath = parent.m_resPath + fi.Name });
        }
    }
    protected override TreeViewItem BuildRoot()
    {
        m_curId = 0;
        var di = new DirectoryInfo(c_rootFullPath);
        var root = new CellMapFileItem { id = 0, depth = -1, displayName = "root", m_fdi = di, m_resPath = c_resPath };
        if (di != null)
        {
            buildFolderView(root, di, -1);
        }
        return root;
    }
    public override void OnGUI(Rect rect)
    {
        base.OnGUI(rect);

        if (Event.current.type == EventType.MouseDown && Event.current.button == 0 && rect.Contains(Event.current.mousePosition))
        {
            SetSelection(new int[0], TreeViewSelectionOptions.FireSelectionChanged);
        }
    }
    protected override void SelectionChanged(IList<int> selectedIds)
    {
        if(selectedIds.Count == 0)
        {
            return;
        }
        var curMapItem = FindItem(selectedIds[0], rootItem) as CellMapFileItem;
        if (curMapItem == null)
        {
            return;
        }
        var fi = curMapItem.m_fdi as FileInfo;
        if(fi != null)
        {
            m_parent.m_curFilePath = fi.FullName;
            m_parent.m_curResPath = curMapItem.m_resPath;
        }
        else
        {
            var di = curMapItem.m_fdi as DirectoryInfo;
            if (di != null)
            {
                m_parent.m_curFilePath = di.FullName;
                m_parent.m_curResPath = curMapItem.m_resPath;
            }
        }
    }

    protected override void ContextClickedItem(int id)
    {
    }

    private const string c_rootFullPath = "./Assets/Resources/Data/CellMap/";
    private const string c_rootResPath = "./Data/CellMap/";
    private const string c_resPath = "Data/CellMap/";
    private int m_curId = 0;
    private CellMapEditor m_parent;
}