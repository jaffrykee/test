using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ZESetting
{
    public static ZESetting s_default
    {
        get
        {
            if (st_default == null)
            {
                st_default = new ZESetting();
                st_default.m_layoutSetting = new Dictionary<string, GUILayoutOption[]>();
                st_default.m_layoutSetting["TextField"] = new GUILayoutOption[] { GUILayout.Width(100) , GUILayout.ExpandWidth(false)};
                st_default.m_layoutSetting["TextFieldLong"] = new GUILayoutOption[] { GUILayout.Width(500), GUILayout.ExpandWidth(false)};
                st_default.m_layoutSetting["Button"] = new GUILayoutOption[] { GUILayout.Width(50) };
                st_default.m_layoutSetting["LabelFieldShort"] = new GUILayoutOption[] { GUILayout.Width(50) };
                st_default.m_layoutSetting["LabelField"] = new GUILayoutOption[] { GUILayout.Width(200) };
                st_default.m_layoutSetting["LabelFieldLong"] = new GUILayoutOption[] { GUILayout.Width(500) };

                st_default.m_styleDic = new Dictionary<string, GUIStyle>();
                var ds = new GUIStyle();
                ds.fontSize = 12;
                st_default.m_styleDic["Default"] = ds;
            }
            return st_default;
        }
    }

    #region public
    static public void setCurSetting(ZESetting cs)
    {
        s_curSetting = cs;
    }
    static public GUILayoutOption[] LayoutSetting(string subType = "")
    {
        if (s_curSetting == null)
        {
            setCurSetting(s_default);
        }
        if (s_curSetting.m_layoutSetting.ContainsKey(subType))
        {
            return s_curSetting.m_layoutSetting[subType];
        }
        return null;
    }
    static public GUILayoutOption[] LayoutSetting<T>()
    {
        string type = typeof(T).Name;
        string subType = type.Substring(type.LastIndexOf('.') + 1);
        return LayoutSetting(subType);
    }
    static public GUIStyle Style(string subType = "")
    {
        if (s_curSetting == null)
        {
            setCurSetting(s_default);
        }
        if (s_curSetting.m_styleDic.ContainsKey(subType))
        {
            return s_curSetting.m_styleDic[subType];
        }
        else if(s_curSetting.m_styleDic.ContainsKey("Default"))
        {
            return s_curSetting.m_styleDic["Default"];
        }
        return null;
    }
    static public GUIStyle Style<T>()
    {
        string type = typeof(T).Name;
        string subType = type.Substring(type.LastIndexOf('.') + 1);
        return Style(subType);
    }
    #endregion

    private static ZESetting s_curSetting;
    private static ZESetting st_default;
    private Dictionary<string, GUILayoutOption[]> m_layoutSetting;
    private Dictionary<string, GUIStyle> m_styleDic;
}
