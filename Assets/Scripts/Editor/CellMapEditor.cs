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

        m_HorizontalSplitterRect = new Rect(
            (int)(position.x + position.width * m_HorizontalSplitterPercent),
            position.y,
            k_SplitterWidth,
            position.height
            );

        m_VerticalSplitterRectRight = new Rect(
            m_HorizontalSplitterRect.x,
            (int)(position.y + m_HorizontalSplitterRect.height * m_VerticalSplitterPercentRight),
            (position.width - m_HorizontalSplitterRect.width) - k_SplitterWidth,
            k_SplitterWidth
            );

        m_VerticalSplitterRectLeft = new Rect(
            position.x,
            (int)(position.y + m_HorizontalSplitterRect.height * m_VerticalSplitterPercentLeft),
            (m_HorizontalSplitterRect.width) - k_SplitterWidth,
            k_SplitterWidth
            );
    }
    private void resizeCellMap(int x, int y)
    {

    }
    private void OnGUI()
    {
        HandleHorizontalResize();
        HandleVerticalResize();

        var unitTreeRect = new Rect(
            position.x,
            position.y,
            m_HorizontalSplitterRect.x,
            m_VerticalSplitterRectLeft.y - position.y
            );

        if (GUILayout.Button("刷新", ZESetting.LayoutSetting("Button")) || m_cmdTree == null)
        {
            m_cmdTree = new CellMapDataFileTreeView(new TreeViewState());
        }
        //TreeView
        m_cmdTree.OnGUI(unitTreeRect);

        var unitPreviewRect = new Rect(
            unitTreeRect.x,
            unitTreeRect.y + unitTreeRect.height + k_SplitterWidth,
            unitTreeRect.width,
            position.height - unitTreeRect.height - k_SplitterWidth * 2
            );

        GUILayout.BeginArea(unitPreviewRect, EditorStyles.helpBox);
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("地图尺寸：", ZESetting.LayoutSetting("LabelFieldShort"));
        EditorGUILayout.LabelField("x:", ZESetting.LayoutSetting("LabelFieldShort"));
        EditorGUILayout.TextField(m_mapSizeX, ZESetting.LayoutSetting("TextField"));
        EditorGUILayout.LabelField("y:", ZESetting.LayoutSetting("LabelFieldShort"));
        EditorGUILayout.TextField(m_mapSizeY, ZESetting.LayoutSetting("TextField"));
        if (GUILayout.Button("设置", ZESetting.LayoutSetting("Button")))
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
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        GUILayout.EndArea();



        float panelLeft = m_HorizontalSplitterRect.x + k_SplitterWidth;
        float panelWidth = m_VerticalSplitterRectRight.width - k_SplitterWidth * 2;
        float panelHeight = m_VerticalSplitterRectRight.y - position.y;

        var unitListRect = new Rect(
            panelLeft,
            position.y,
            panelWidth,
            panelHeight
            );
        //m_UnitList.OnGUI(unitListRect);
        var unitEditorRect = new Rect(
            panelLeft,
            position.y + panelHeight + k_SplitterWidth,
            panelWidth,
            (position.height - panelHeight) - k_SplitterWidth * 2
            );

        GUILayout.BeginArea(unitEditorRect, EditorStyles.helpBox);
        //m_UnitEditor.OnGUI(unitListRect);
        GUILayout.EndArea();

        if (m_ResizingHorizontalSplitter || m_ResizingVerticalSplitterLeft || m_ResizingVerticalSplitterRight)
        {
            Repaint();
        }
    }

    #region splitter
    private void HandleHorizontalResize()
    {
        m_HorizontalSplitterRect.x = (int)(position.width * m_HorizontalSplitterPercent);
        m_HorizontalSplitterRect.height = position.height;

        EditorGUIUtility.AddCursorRect(m_HorizontalSplitterRect, MouseCursor.ResizeHorizontal);
        if (Event.current.type == EventType.MouseDown && m_HorizontalSplitterRect.Contains(Event.current.mousePosition))
        {
            m_ResizingHorizontalSplitter = true;
        }

        if (m_ResizingHorizontalSplitter)
        {
            m_HorizontalSplitterPercent = Mathf.Clamp(Event.current.mousePosition.x / position.width, 0.1f, 0.9f);
            m_HorizontalSplitterRect.x = (int)(position.width * m_HorizontalSplitterPercent);
        }

        if (Event.current.type == EventType.MouseUp)
        {
            m_ResizingHorizontalSplitter = false;
        }
    }

    private void HandleVerticalResize()
    {
        m_VerticalSplitterRectRight.x = m_HorizontalSplitterRect.x;
        m_VerticalSplitterRectRight.y = (int)(m_HorizontalSplitterRect.height * m_VerticalSplitterPercentRight);
        m_VerticalSplitterRectRight.width = position.width - m_HorizontalSplitterRect.x;
        m_VerticalSplitterRectLeft.y = (int)(m_HorizontalSplitterRect.height * m_VerticalSplitterPercentLeft);
        m_VerticalSplitterRectLeft.width = m_VerticalSplitterRectRight.width;

        EditorGUIUtility.AddCursorRect(m_VerticalSplitterRectRight, MouseCursor.ResizeVertical);
        if (Event.current.type == EventType.MouseDown && m_VerticalSplitterRectRight.Contains(Event.current.mousePosition))
        {
            m_ResizingVerticalSplitterRight = true;
        }

        EditorGUIUtility.AddCursorRect(m_VerticalSplitterRectLeft, MouseCursor.ResizeVertical);
        if (Event.current.type == EventType.MouseDown && m_VerticalSplitterRectLeft.Contains(Event.current.mousePosition))
        {
            m_ResizingVerticalSplitterLeft = true;
        }

        if (m_ResizingVerticalSplitterRight)
        {
            m_VerticalSplitterPercentRight = Mathf.Clamp(Event.current.mousePosition.y / m_HorizontalSplitterRect.height, 0.2f, 0.98f);
            m_VerticalSplitterRectRight.y = (int)(m_HorizontalSplitterRect.height * m_VerticalSplitterPercentRight);
        }
        else if (m_ResizingVerticalSplitterLeft)
        {
            m_VerticalSplitterPercentLeft = Mathf.Clamp(Event.current.mousePosition.y / m_HorizontalSplitterRect.height, 0.25f, 0.98f);
            m_VerticalSplitterRectLeft.y = (int)(m_HorizontalSplitterRect.height * m_VerticalSplitterPercentLeft);
        }

        if (Event.current.type == EventType.MouseUp)
        {
            m_ResizingVerticalSplitterRight = false;
            m_ResizingVerticalSplitterLeft = false;
        }
    }
    #endregion

    private const string c_rootPath = "./Assets/Resources/Data/CellMap/";
    private const string c_resPath = "Data/CellMap/";

    public static CellMapEditor s_instance = null;

    public string m_mapSizeX = "";
    public string m_mapSizeY = "";
    public string m_showValue = "";
    public CellMapDataFileTreeView m_cmdTree;

    #region splitter
    bool m_ResizingHorizontalSplitter = false;
    bool m_ResizingVerticalSplitterRight = false;
    bool m_ResizingVerticalSplitterLeft = false;
    Rect m_HorizontalSplitterRect, m_VerticalSplitterRectRight, m_VerticalSplitterRectLeft;

    [SerializeField]
    float m_HorizontalSplitterPercent = 0.4f;
    [SerializeField]
    float m_VerticalSplitterPercentRight = 0.7f;
    [SerializeField]
    float m_VerticalSplitterPercentLeft = 0.85f;

    const float k_SplitterWidth = 3f;
    private static float m_UpdateDelay = 0f;
    #endregion
}
