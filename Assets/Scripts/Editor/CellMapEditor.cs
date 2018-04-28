using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

public class CellMapEditor : EditorWindow
{
    [MenuItem("Editors/CellMapEditor")]
    static void ShowEditor()
    {
        //EditorUtility.DisplayDialog("MyTool", "Do It in C# !", "OK", "");
        if (s_instance == null)
        {
            //Type[] types = new Type[2] { typeof(BlackWood.NodeCanvas.Editor.SkinContainer), typeof(BlackWood.NodeCanvas.Editor.BBPContainer) };
            //s_instance = GetWindow<ZEditor>("Unit Browser", true, types);
            s_instance = GetWindow<CellMapEditor>(false, "CellMapEditor");
        }
        //s_instance.Show();

        s_instance.Show();
    }
    private void OnEnable()
    {
        UnityEngine.Input.imeCompositionMode = IMECompositionMode.On;
    }
    private void resizeCellMap(int x, int y)
    {

    }
    private void OnGUI()
    {
        if(m_cmdTree == null)
        {
            m_cmdTree = new CellMapDataFileTreeView(new TreeViewState());
        }
        m_cmdTree.OnGUI(new Rect(0, 100, 100, 100));

        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("地图尺寸：", ZESetting.LayoutSetting("LabelFieldShort"));
        EditorGUILayout.LabelField("x:", ZESetting.LayoutSetting("LabelFieldShort"));
        EditorGUILayout.TextField(m_mapSizeX, ZESetting.LayoutSetting("TextField"));
        EditorGUILayout.LabelField("y:", ZESetting.LayoutSetting("LabelFieldShort"));
        EditorGUILayout.TextField(m_mapSizeY, ZESetting.LayoutSetting("TextField"));
        if (GUILayout.Button("重置尺寸", ZESetting.LayoutSetting("Button")))
        {
            //int x = m_mapSizeX.tr
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Ok", ZESetting.LayoutSetting("Button")))
        {
        }
        else if (GUILayout.Button("Cancel", ZESetting.LayoutSetting("Button")))
        {
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        //EditorGUILayout.LabelField(m_showValue, ZESetting.LayoutSetting("LabelField"));
    }

    private const string c_rootPath = "./Assets/Resources/Data/CellMap/";
    private const string c_resPath = "Data/CellMap/";

    public static CellMapEditor s_instance = null;

    public string m_mapSizeX = "";
    public string m_mapSizeY = "";
    public string m_showValue = "";
    public CellMapDataFileTreeView m_cmdTree;
}
