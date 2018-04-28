using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.IMGUI.Controls;
using UnityEditor;
using System.IO;

public class CellMapDataFileTreeView : TreeView
{
    //int curId;
    public CellMapDataFileTreeView(TreeViewState state) : base(state)
    {
        showBorder = true;
        Reload();
    }
    private void buildFolderView(TreeViewItem parent, DirectoryInfo parentDi, int curDepth)
    {
        var arrDi = parentDi.GetDirectories();
        foreach (var di in arrDi)
        {
            var diView = new TreeViewItem { id = ++m_curId, depth = 0, displayName = di.Name };
            parent.AddChild(diView);
            buildFolderView(diView, di, curDepth + 1);
        }
        var arrFi = parentDi.GetFiles("*.cmp");
        foreach (var fi in arrFi)
        {
            parent.AddChild(new TreeViewItem { id = ++m_curId, depth = curDepth + 1, displayName = fi.Name });
        }
    }
    protected override TreeViewItem BuildRoot()
    {
        m_curId = 0;
        var root = new TreeViewItem { id = 0, depth = -1, displayName = "root" };
        var di = new DirectoryInfo(c_rootPath);
        if (di != null)
        {
            buildFolderView(root, di, -1);
            //TreeView
            //TreeEditor.
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
    }

    protected override void ContextClickedItem(int id)
    {
    }

    private const string c_rootPath = "./Assets/Resources/Data/CellMap/";
    private const string c_resPath = "Data/CellMap/";
    private int m_curId = 0;
}