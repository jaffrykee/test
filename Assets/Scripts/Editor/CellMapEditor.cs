using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using System.IO;
using LitJson;
using System;

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
            (int)(position.width * m_HorizontalSplitterPercent),
            0,
            k_SplitterWidth,
            position.height
            );

        m_VerticalSplitterRectRight = new Rect(
            m_HorizontalSplitterRect.x,
            (int)(m_HorizontalSplitterRect.height * m_VerticalSplitterPercentRight),
            (position.width - m_HorizontalSplitterRect.width) - k_SplitterWidth,
            k_SplitterWidth
            );

        m_VerticalSplitterRectLeft = new Rect(
            0,
            (int)(m_HorizontalSplitterRect.height * m_VerticalSplitterPercentLeft),
            (m_HorizontalSplitterRect.width) - k_SplitterWidth,
            k_SplitterWidth
            );
    }
    private void saveCellMapSetting()
    {

        var writer = new LitJson.JsonWriter { PrettyPrint = true };

        JsonMapper.RegisterExporter<float>((obj, writerFloat) => writerFloat.Write(Convert.ToDouble(obj)));
        JsonMapper.RegisterImporter<double, float>(inputFlot => Convert.ToSingle(inputFlot));
        JsonMapper.RegisterExporter<Vector3>((obj, writerVector3) => GameConvert.ToJsonWritter(writerVector3, obj));
        JsonMapper.RegisterImporter<double[], Vector3>(inputVector3 => new Vector3(Convert.ToSingle(inputVector3[0]), Convert.ToSingle(inputVector3[1]), Convert.ToSingle(inputVector3[2])));
        JsonMapper.RegisterExporter<Vector2Int>((obj, writerVector2Int) => GameConvert.ToJsonWritter(writerVector2Int, obj));
        JsonMapper.RegisterImporter<int[], Vector2Int>(inputVector2Int => new Vector2Int(inputVector2Int[0], inputVector2Int[1]));
        
        LitJson.JsonMapper.ToJson(m_curMapSetting, writer);
        File.WriteAllText(mt_curFilePath, writer.ToString());
    }
    private void resizeCellMap(int x, int y)
    {
        m_curMapSetting.resizeCellMap(x, y);
        saveCellMapSetting();
    }
    private void OnGUI()
    {
        HandleHorizontalResize();
        HandleVerticalResize();

        var unitTreeRect = new Rect(
            0,
            0,
            m_HorizontalSplitterRect.x,
            m_VerticalSplitterRectLeft.y
            );

        if (GUILayout.Button("刷新", ZESetting.LayoutSetting("Button")) || m_cmdTree == null)
        {
            m_cmdTree = new CellMapDataFileTreeView(new TreeViewState(), this);
        }
        var unitTreeRect2 = unitTreeRect;
        unitTreeRect2.y += 25;
        unitTreeRect2.height -= 25;
        //TreeView
        m_cmdTree.OnGUI(unitTreeRect2);

        var unitPreviewRect = new Rect(
            unitTreeRect.x,
            unitTreeRect.y + unitTreeRect.height + k_SplitterWidth,
            unitTreeRect.width,
            position.height - unitTreeRect.height - k_SplitterWidth * 2
            );

        GUILayout.BeginArea(unitPreviewRect, EditorStyles.helpBox);

        GUILayout.EndArea();
        
        float panelLeft = m_HorizontalSplitterRect.x + k_SplitterWidth;
        float panelWidth = m_VerticalSplitterRectRight.width - k_SplitterWidth * 2;
        float panelHeight = m_VerticalSplitterRectRight.y;

        var unitListRect = new Rect(
            panelLeft,
            0,
            panelWidth,
            panelHeight
            );
        GUILayout.BeginArea(unitListRect, EditorStyles.helpBox);
        //m_UnitList.OnGUI(unitListRect);
        #region attr
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("本地路径", ZESetting.LayoutSetting("LabelFieldShort"));
        EditorGUILayout.TextField(m_curFilePath, ZESetting.LayoutSetting("TextFieldLong"));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("资源路径", ZESetting.LayoutSetting("LabelFieldShort"));
        EditorGUILayout.TextField(m_curResPath, ZESetting.LayoutSetting("TextFieldLong"));
        EditorGUILayout.EndHorizontal();
        if(!(m_curResPath == null || m_curResPath.Length == 0 || m_curResPath[m_curResPath.Length - 1] == '/'))
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("地图尺寸：", ZESetting.LayoutSetting("LabelFieldShort"));
            EditorGUILayout.LabelField("x:", ZESetting.LayoutSetting("LabelFieldShort"));
            m_mapSizeX = EditorGUILayout.TextField(m_mapSizeX, ZESetting.LayoutSetting("TextField"));
            EditorGUILayout.LabelField("y:", ZESetting.LayoutSetting("LabelFieldShort"));
            m_mapSizeY = EditorGUILayout.TextField(m_mapSizeY, ZESetting.LayoutSetting("TextField"));
            if (GUILayout.Button("设置", ZESetting.LayoutSetting("Button")))
            {
                var sizeX = Convert.ToInt32(m_mapSizeX);
                var sizeY = Convert.ToInt32(m_mapSizeY);
                resizeCellMap(sizeX, sizeY);
            }
            EditorGUILayout.EndHorizontal();
            //EditorGUILayout.BeginHorizontal();
            //if (GUILayout.Button("Ok", ZESetting.LayoutSetting("Button")))
            //{
            //}
            //else if (GUILayout.Button("Cancel", ZESetting.LayoutSetting("Button")))
            //{
            //}
            //EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        #endregion
        GUILayout.EndArea();

        var unitEditorRect = new Rect(
            panelLeft,
            panelHeight + k_SplitterWidth,
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

    const float k_SplitterWidth = 3.0f;
    private string mt_curFilePath;
    public string m_curFilePath
    {
        get
        {
            return mt_curFilePath;
        }
        set
        {
            mt_curFilePath = value;
            if(File.Exists(value))
            {
                m_curMapSetting = JsonMapper.ToObject<CellMapSetting>(File.ReadAllText(value));
                if(m_curMapSetting == null)
                {
                    m_curMapSetting = new CellMapSetting(2, 2);
                    saveCellMapSetting();
                }
                m_mapSizeX = m_curMapSetting.mapSizeX.ToString();
                m_mapSizeY = m_curMapSetting.mapSizeY.ToString();
            }
        }
    }
    private string m_mapSizeX;
    private string m_mapSizeY;
    private CellMapSetting m_curMapSetting;
    [HideInInspector]
    public string m_curResPath;
    #endregion
}
