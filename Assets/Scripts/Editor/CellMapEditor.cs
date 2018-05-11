using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using System.IO;
using LitJson;
using System;

public class CellMapEditor : EditorWindow {
    [MenuItem("Editors/CellMapEditor")]
    static void ShowEditor() {
        if (s_instance == null) {
            s_instance = GetWindow<CellMapEditor>(false, "CellMapEditor");
        }
        s_instance.Show();
    }
    private Texture2D createTexture(uint len, Color color) {
        if (len > 2048) {
            return null;
        }
        var tex = new Texture2D((int)len, (int)len);
        for (int i = 0; i < tex.width; i++) {
            for (int j = 0; j < tex.height; j++) {
                tex.SetPixel(i, j, color);
            }
        }
        tex.Apply();
        return tex;
    }
    private void initCellBtnTex() {
        /*
            0x SIZx xxxx : SIZ代表大小
            
            0x SIZ1 0000 : normal 0     浅绿
            0x SIZ1 0001 : normal 1     黄绿至红
            0x SIZ0 ffff : normal -1    深绿
            0x SIZ0 fffe : normal -2    深深绿至蓝

            0x SIZ3 0000 : disable           深灰
        */
        m_cellBtnTex = new Dictionary<uint, Texture2D>();
        m_curSelectTex = new Dictionary<int, Texture2D>();
        //Texture2D tex
    }
    private Texture2D getCellBtnTexCache(uint id) {
        Texture2D cache;
        if (m_cellBtnTex.TryGetValue(id, out cache) == false || cache == null) {
            uint size = ((id & 0xfff00000) >> 20);
            uint colorIndex = (id & 0x000fffff);
            Color color;
            if (Cell.s_cellColor.TryGetValue(colorIndex, out color) == false) {
                color = new Color(0, 0, 0, 0);
            }
            m_cellBtnTex.Add(id, createTexture(size - c_cellButtonSpacing, color));
            cache = m_cellBtnTex[id];
        }
        return cache;
    }
    private Texture2D getSelectBorderTexCache(int len) {
        Texture2D tex;
        if (m_curSelectTex.TryGetValue(len, out tex) == false || tex == null) {
            tex = new Texture2D(len, len);
            for (int i = 0; i < tex.width; i++) {
                for (int j = 0; j < tex.height; j++) {
                    if (i < m_curSelectBorderWidth || j < m_curSelectBorderWidth || i >= len - m_curSelectBorderWidth || j >= len - m_curSelectBorderWidth) {
                        tex.SetPixel(i, j, m_curSelectColor);
                    } else {
                        tex.SetPixel(i, j, new Color(0, 0, 0, 0));
                    }
                }
            }
            tex.Apply();
        }
        return tex;
    }
    private void OnEnable() {
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

        m_preview = new CellMapPreview();
        initCellBtnTex();
    }
    private void saveCellMapSetting() {
        var writer = new LitJson.JsonWriter { PrettyPrint = true };
        LitJson.JsonMapper.ToJson(m_curMapConfig, writer);
        File.WriteAllText(mt_curFilePath, writer.ToString());
        Repaint();
    }
    private void resizeCellMap(int x, int y) {
        m_curMapConfig.resizeCellMap(x, y);
        saveCellMapSetting();
    }
    private Vector2 scrollPosition = new Vector2();
    private string keyCache = "";
    private void curSelectCellChanged() {
        keyCache = "";
    }
    private void OnGUI() {
        int dh = 0;
        var e = Event.current;
        bool isChanged = false;
        if (e != null && e.isKey && e.type == EventType.KeyUp) {
            if (m_curMapConfig != null && m_curCellX < m_curMapConfig.mapSizeX && m_curCellY < m_curMapConfig.mapSizeY) {
                if (e.keyCode == KeyCode.W) {
                    dh = 1;
                }
                if (e.keyCode == KeyCode.S) {
                    dh = -1;
                }
                if (e.keyCode == KeyCode.Minus) {
                    keyCache = "-";
                }
                if (e.keyCode >= KeyCode.Alpha0 && e.keyCode <= KeyCode.Alpha9) {
                    keyCache += (e.keyCode - KeyCode.Alpha0).ToString();
                    isChanged = true;
                }
            }
        }

        HandleHorizontalResize();
        HandleVerticalResize();

        #region TreeView_LT
        var unitTreeRect = new Rect(
            0,
            0,
            m_HorizontalSplitterRect.x,
            m_VerticalSplitterRectLeft.y
            );

        if (GUILayout.Button("刷新", ZESetting.LayoutSetting("Button")) || m_cmdTree == null) {
            m_cmdTree = new CellMapDataFileTreeView(new TreeViewState(), this);
        }
        var unitTreeRect2 = unitTreeRect;
        unitTreeRect2.y += 25;
        unitTreeRect2.height -= 25;
        //TreeView
        m_cmdTree.OnGUI(unitTreeRect2);
        #endregion

        #region Setting_LB
        var unitPreviewRect = new Rect(
            unitTreeRect.x,
            unitTreeRect.y + unitTreeRect.height + k_SplitterWidth,
            unitTreeRect.width,
            position.height - unitTreeRect.height - k_SplitterWidth * 2
            );
        GUILayout.BeginArea(unitPreviewRect, EditorStyles.helpBox);
        //m_preview.OnGUI(unitPreviewRect);
        GUILayout.BeginVertical();
        m_isChangedValue = EditorGUILayout.Toggle("改disable", m_isChangedValue);
        GUILayout.BeginHorizontal();
        const int btnLen = 32;
        if (GUILayout.Button("大", new GUILayoutOption[] { GUILayout.Width(btnLen), GUILayout.Height(btnLen) })) {
            m_cellButtonLen = c_cellButtonLenLarge;
        }
        if (GUILayout.Button("中", new GUILayoutOption[] { GUILayout.Width(btnLen), GUILayout.Height(btnLen) })) {
            m_cellButtonLen = c_cellButtonLenMiddle;
        }
        if (GUILayout.Button("小", new GUILayoutOption[] { GUILayout.Width(btnLen), GUILayout.Height(btnLen) })) {
            m_cellButtonLen = c_cellButtonLenSmall;
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        GUILayout.EndArea();
        #endregion

        float panelLeft = m_HorizontalSplitterRect.x + k_SplitterWidth;
        float panelWidth = m_VerticalSplitterRectRight.width - k_SplitterWidth * 2;
        float panelHeight = m_VerticalSplitterRectRight.y;

        #region Canvas_RT
        var unitListRect = new Rect(
            panelLeft,
            0,
            panelWidth,
            panelHeight
            );
        GUILayout.BeginArea(unitListRect, EditorStyles.helpBox);
        if (m_curMapConfig != null && m_isShowCanvas == true) {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            GUILayout.BeginHorizontal(GUILayout.Width(m_curMapConfig.mapSizeX * m_cellButtonLen));
            for (int i = 0; i < m_curMapConfig.mapSizeX; i++) {
                GUILayout.BeginVertical();
                if ((i & 1) == 1) {
                    GUILayout.Space(m_cellButtonLen * 0.5f);
                }
                for (int j = m_curMapConfig.mapSizeY - 1; j >= 0; j--) {
                    //curTip = i.ToString() + ", " + j.ToString() + "\n";
                    uint texId = 0;
                    Texture2D tmpTex = null;

                    if (m_curMapConfig != null) {
                        var curCell = m_curMapConfig.cellData[i * m_curMapConfig.mapSizeY + j];
                        //if (m_curCellX == i && m_curCellY == j)
                        //{
                        //    //curSelect
                        //}
                        if (curCell.disable) {
                            //disable
                            texId |= c_cbtnStateDisable;
                        } else {
                            //normal
                            texId |= c_cbtnStateNormal;
                            texId = (uint)(texId + curCell.height);
                        }
                    }
                    GUIStyle s = new GUIStyle();
                    //var tex = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Texture/Hexagon.png");
                    texId |= (((uint)m_cellButtonLen) << 20);
                    //s.imagePosition = ImagePosition.ImageOnly;
                    tmpTex = getCellBtnTexCache(texId);
                    if (tmpTex == null) {
                        tmpTex = new Texture2D(m_cellButtonLen, m_cellButtonLen);
                    }
                    GUIContent gc = new GUIContent(tmpTex);
                    //tex.width = m_cellButtonLen;
                    //tex.height = m_cellButtonLen;
                    //GUILayout.Button()
                    //if (GUILayout.Button(curTip, new GUILayoutOption[] { GUILayout.Width(m_cellButtonLen), GUILayout.Height(m_cellButtonLen) }))
                    if (GUILayout.Button(gc, s, new GUILayoutOption[] { GUILayout.Width(m_cellButtonLen), GUILayout.Height(m_cellButtonLen) })) {
                        m_curCellX = i;
                        m_curCellY = j;
                        if (m_isChangedValue == true) {
                            m_curMapConfig.cellData[i * m_curMapConfig.mapSizeY + j].disable = !m_curMapConfig.cellData[i * m_curMapConfig.mapSizeY + j].disable;
                            saveCellMapSetting();
                        }
                        GUI.SetNextControlName("mx_tmpFocus");
                        GUI.TextField(new Rect(), "", 0);
                        GUI.FocusControl("mx_tmpFocus");
                        curSelectCellChanged();
                        isChanged = false;
                    }
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
            //curSelectBorder
            const float dx = 0;
            const float dy = 0;
            float selbx = m_curCellX * m_cellButtonLen + dx;
            float selby = ((m_curCellX & 1) * 0.5f + m_curMapConfig.mapSizeY - m_curCellY - 1) * m_cellButtonLen + dy;
            GUILayout.BeginArea(new Rect(selbx, selby, m_cellButtonLen, m_cellButtonLen));
            var texSel = getSelectBorderTexCache(m_cellButtonLen - c_cellButtonSpacing);
            GUILayout.Button(new GUIContent(texSel), new GUIStyle(), new GUILayoutOption[] { GUILayout.Width(m_cellButtonLen), GUILayout.Height(m_cellButtonLen) });
            GUILayout.EndArea();
            GUILayout.EndScrollView();
        }
        GUILayout.EndArea();
        #endregion

        #region Attr_RB
        var unitEditorRect = new Rect(
            panelLeft,
            panelHeight + k_SplitterWidth,
            panelWidth,
            (position.height - panelHeight) - k_SplitterWidth * 2
            );
        GUILayout.BeginArea(unitEditorRect, EditorStyles.helpBox);
        //m_UnitList.OnGUI(unitListRect);
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
        if (!(m_curResPath == null || m_curResPath.Length == 0 || m_curResPath[m_curResPath.Length - 1] == '/')) {
            m_isShowCanvas = true;
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("地图尺寸：", ZESetting.LayoutSetting("LabelFieldShort"));
            EditorGUILayout.LabelField("x:", ZESetting.LayoutSetting("LabelFieldShort"));
            m_mapSizeX = EditorGUILayout.TextField(m_mapSizeX, ZESetting.LayoutSetting("TextField"));
            EditorGUILayout.LabelField("y:", ZESetting.LayoutSetting("LabelFieldShort"));
            m_mapSizeY = EditorGUILayout.TextField(m_mapSizeY, ZESetting.LayoutSetting("TextField"));
            var sizeX = Convert.ToInt32(m_mapSizeX);
            var sizeY = Convert.ToInt32(m_mapSizeY);
            if (GUILayout.Button("设置", ZESetting.LayoutSetting("Button"))) {
                resizeCellMap(sizeX, sizeY);
            }
            EditorGUILayout.EndHorizontal();
            if (m_curMapConfig != null && m_curCellX < m_curMapConfig.mapSizeX && m_curCellY < m_curMapConfig.mapSizeY) {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("当前Cell：" + m_curCellX.ToString() + ", " + m_curCellY.ToString(), ZESetting.LayoutSetting("LabelField"));
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("disable:", ZESetting.LayoutSetting("LabelFieldShort"));
                var oldValue = m_curMapConfig.cellData[m_curCellX * m_curMapConfig.mapSizeY + m_curCellY].disable;
                var newValue = EditorGUILayout.Toggle(oldValue);
                if (newValue != oldValue) {
                    m_curMapConfig.cellData[m_curCellX * m_curMapConfig.mapSizeY + m_curCellY].disable = newValue;
                    saveCellMapSetting();
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("height:", ZESetting.LayoutSetting("LabelFieldShort"));
                string newHeightStr;
                if (isChanged == true) {
                    newHeightStr = keyCache;
                    isChanged = false;
                } else {
                    newHeightStr = EditorGUILayout.TextField(m_curMapConfig.cellData[m_curCellX * m_curMapConfig.mapSizeY + m_curCellY].height.ToString(), ZESetting.LayoutSetting("TextField"));
                }
                var newHeight = Convert.ToInt32(newHeightStr) + dh;
                if (newHeight != m_curMapConfig.cellData[m_curCellX * m_curMapConfig.mapSizeY + m_curCellY].height) {
                    m_curMapConfig.cellData[m_curCellX * m_curMapConfig.mapSizeY + m_curCellY].height = newHeight;
                    saveCellMapSetting();
                }
                EditorGUILayout.EndHorizontal();
            }
            //EditorGUILayout.BeginHorizontal();
            //if (GUILayout.Button("Ok", ZESetting.LayoutSetting("Button")))
            //{
            //}
            //else if (GUILayout.Button("Cancel", ZESetting.LayoutSetting("Button")))
            //{
            //}
            //EditorGUILayout.EndHorizontal();
        } else {
            m_isShowCanvas = false;
        }
        EditorGUILayout.EndVertical();
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        GUILayout.EndArea();
        #endregion

        if (m_ResizingHorizontalSplitter || m_ResizingVerticalSplitterLeft || m_ResizingVerticalSplitterRight) {
            Repaint();
        }
    }

    #region splitter
    private void HandleHorizontalResize() {
        m_HorizontalSplitterRect.x = (int)(position.width * m_HorizontalSplitterPercent);
        m_HorizontalSplitterRect.height = position.height;

        EditorGUIUtility.AddCursorRect(m_HorizontalSplitterRect, MouseCursor.ResizeHorizontal);
        if (Event.current.type == EventType.MouseDown && m_HorizontalSplitterRect.Contains(Event.current.mousePosition)) {
            m_ResizingHorizontalSplitter = true;
        }

        if (m_ResizingHorizontalSplitter) {
            m_HorizontalSplitterPercent = Mathf.Clamp(Event.current.mousePosition.x / position.width, 0.1f, 0.9f);
            m_HorizontalSplitterRect.x = (int)(position.width * m_HorizontalSplitterPercent);
        }

        if (Event.current.type == EventType.MouseUp) {
            m_ResizingHorizontalSplitter = false;
        }
    }

    private void HandleVerticalResize() {
        m_VerticalSplitterRectRight.x = m_HorizontalSplitterRect.x;
        m_VerticalSplitterRectRight.y = (int)(m_HorizontalSplitterRect.height * m_VerticalSplitterPercentRight);
        m_VerticalSplitterRectRight.width = position.width - m_HorizontalSplitterRect.x;
        m_VerticalSplitterRectLeft.y = (int)(m_HorizontalSplitterRect.height * m_VerticalSplitterPercentLeft);
        m_VerticalSplitterRectLeft.width = m_VerticalSplitterRectRight.width;

        EditorGUIUtility.AddCursorRect(m_VerticalSplitterRectRight, MouseCursor.ResizeVertical);
        if (Event.current.type == EventType.MouseDown && m_VerticalSplitterRectRight.Contains(Event.current.mousePosition)) {
            m_ResizingVerticalSplitterRight = true;
        }

        EditorGUIUtility.AddCursorRect(m_VerticalSplitterRectLeft, MouseCursor.ResizeVertical);
        if (Event.current.type == EventType.MouseDown && m_VerticalSplitterRectLeft.Contains(Event.current.mousePosition)) {
            m_ResizingVerticalSplitterLeft = true;
        }

        if (m_ResizingVerticalSplitterRight) {
            m_VerticalSplitterPercentRight = Mathf.Clamp(Event.current.mousePosition.y / m_HorizontalSplitterRect.height, 0.2f, 0.98f);
            m_VerticalSplitterRectRight.y = (int)(m_HorizontalSplitterRect.height * m_VerticalSplitterPercentRight);
        } else if (m_ResizingVerticalSplitterLeft) {
            m_VerticalSplitterPercentLeft = Mathf.Clamp(Event.current.mousePosition.y / m_HorizontalSplitterRect.height, 0.25f, 0.98f);
            m_VerticalSplitterRectLeft.y = (int)(m_HorizontalSplitterRect.height * m_VerticalSplitterPercentLeft);
        }

        if (Event.current.type == EventType.MouseUp) {
            m_ResizingVerticalSplitterRight = false;
            m_ResizingVerticalSplitterLeft = false;
        }
    }
    #endregion

    private const string c_rootPath = "./Assets/Resources/Data/CellMap/";
    private const string c_resPath = "Data/CellMap/";

    private const int c_cellButtonLenSmall = 20;
    private const int c_cellButtonLenMiddle = 32;
    private const int c_cellButtonLenLarge = 50;
    private const int c_cellButtonSpacing = 2;
    private int m_cellButtonLen = c_cellButtonLenSmall;

    private Dictionary<uint, Texture2D> m_cellBtnTex;
    private Dictionary<int, Texture2D> m_curSelectTex;
    private Color m_curSelectColor = Color.white;
    private int m_curSelectBorderWidth = 3;

    public static CellMapEditor s_instance = null;

    public string m_showValue = "";
    public CellMapDataFileTreeView m_cmdTree;
    public CellMapPreview m_preview;

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
    #endregion

    private string mt_curFilePath;
    public string m_curFilePath {
        get {
            return mt_curFilePath;
        }
        set {
            mt_curFilePath = value;
            if (File.Exists(value)) {
                m_curMapConfig = JsonMapper.ToObject<CellMapData>(File.ReadAllText(value));
                if (m_curMapConfig == null) {
                    m_curMapConfig = new CellMapData(2, 2);
                    saveCellMapSetting();
                }
                m_mapSizeX = m_curMapConfig.mapSizeX.ToString();
                m_mapSizeY = m_curMapConfig.mapSizeY.ToString();
            }
        }
    }
    private const int c_cbtnStateNormal = 0x10000;
    private const int c_cbtnStateCurSelect = 0x20000;
    private const int c_cbtnStateDisable = 0x30000;

    private string m_mapSizeX;
    private string m_mapSizeY;
    private int m_curCellX = 0;
    private int m_curCellY = 0;
    private bool m_isShowCanvas = false;
    private CellMapData m_curMapConfig;
    private bool m_isChangedValue = false;
    [HideInInspector]
    public string m_curResPath;
}
