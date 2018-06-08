using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StyleSetting {
    public static StyleSetting s_default {
        get {
            if (st_default == null) {
                st_default = new StyleSetting();
                st_default.m_layoutSetting = new Dictionary<string, GUILayoutOption[]>();
                st_default.m_layoutSetting["TextField"] = new GUILayoutOption[] { GUILayout.Width(100), GUILayout.ExpandWidth(false) };
                st_default.m_layoutSetting["TextFieldLong"] = new GUILayoutOption[] { GUILayout.Width(500), GUILayout.ExpandWidth(false) };
                st_default.m_layoutSetting["Button"] = new GUILayoutOption[] { GUILayout.Width(50) };
                st_default.m_layoutSetting["ButtonL"] = new GUILayoutOption[] { GUILayout.Width(100), GUILayout.Height(25) };
                st_default.m_layoutSetting["Cell"] = new GUILayoutOption[] { GUILayout.Width(50), GUILayout.Height(50) };
                st_default.m_layoutSetting["LabelFieldShort"] = new GUILayoutOption[] { GUILayout.Width(50) };
                st_default.m_layoutSetting["LabelField"] = new GUILayoutOption[] { GUILayout.Width(200) };
                st_default.m_layoutSetting["LabelFieldLong"] = new GUILayoutOption[] { GUILayout.Width(500) };
                st_default.m_layoutSetting["w100ne"] = new GUILayoutOption[] { GUILayout.Width(100), GUILayout.ExpandWidth(false) };
                st_default.m_layoutSetting["w150ne"] = new GUILayoutOption[] { GUILayout.Width(150), GUILayout.ExpandWidth(false) };
                st_default.m_layoutSetting["w200ne"] = new GUILayoutOption[] { GUILayout.Width(200), GUILayout.ExpandWidth(false) };

                st_default.m_styleDic = new Dictionary<string, GUIStyle>();
                var ds = new GUIStyle();
                ds.fontSize = 12;
                st_default.m_styleDic["Default"] = ds;
                var llr = GUI.skin.GetStyle("Label");
                llr.alignment = TextAnchor.LowerRight;
                st_default.m_styleDic["LabelLR"] = llr;
                var lmr = GUI.skin.GetStyle("Label");
                lmr.alignment = TextAnchor.MiddleRight;
                st_default.m_styleDic["LabelMR"] = lmr;
            }
            return st_default;
        }
    }

    #region public
    static public void setCurSetting(StyleSetting cs) {
        s_curSetting = cs;
    }
    static public GUILayoutOption[] LayoutSetting(string subType = "") {
        if (s_curSetting == null) {
            setCurSetting(s_default);
        }
        if (s_curSetting.m_layoutSetting.ContainsKey(subType)) {
            return s_curSetting.m_layoutSetting[subType];
        }
        return null;
    }
    static public GUILayoutOption[] LayoutSetting<T>() {
        string type = typeof(T).Name;
        string subType = type.Substring(type.LastIndexOf('.') + 1);
        return LayoutSetting(subType);
    }
    static public GUIStyle Style(string subType = "") {
        if (s_curSetting == null) {
            setCurSetting(s_default);
        }
        if (s_curSetting.m_styleDic.ContainsKey(subType)) {
            return s_curSetting.m_styleDic[subType];
        } else if (s_curSetting.m_styleDic.ContainsKey("Default")) {
            return s_curSetting.m_styleDic["Default"];
        }
        return null;
    }
    static public GUIStyle Style<T>() {
        string type = typeof(T).Name;
        string subType = type.Substring(type.LastIndexOf('.') + 1);
        return Style(subType);
    }
    #endregion

    #region Ctrl
    static public void neLabel(string content, int width = 100) {
        GUILayout.Label(content, new GUILayoutOption[] { GUILayout.MinWidth(width), GUILayout.ExpandWidth(false) });
    }
    static public void neLabel(GUIContent content, int width = 100) {
        GUILayout.Label(content, new GUILayoutOption[] { GUILayout.MinWidth(width), GUILayout.ExpandWidth(false) });
    }
    static public void neHgBegin(int width = 800) {
        GUILayout.BeginHorizontal(new GUILayoutOption[] { GUILayout.MinWidth(width) });
    }
    static public void neHgEnd() {
        GUILayout.EndHorizontal();
    }
    static public void neVgBegin() {
        GUILayout.BeginVertical(EditorStyles.helpBox);
    }
    static public void neVgBegin(string title) {
        neVgBegin();
        GUILayout.Label(new GUIContent(title));
    }
    static public void neVgEnd() {
        GUILayout.EndVertical();
    }
    #endregion

    private static StyleSetting s_curSetting;
    private static StyleSetting st_default;
    private Dictionary<string, GUILayoutOption[]> m_layoutSetting;
    private Dictionary<string, GUIStyle> m_styleDic;
}
