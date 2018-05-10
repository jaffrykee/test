using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using LitJson;
using System.IO;
using System.Text;

public class ZEditor : EditorWindow
{
    //[MenuItem("Editors/LiumkTest/123123")]
    static void ShowEditor()
    {
        //EditorUtility.DisplayDialog("MyTool", "Do It in C# !", "OK", "");
        if(s_instance == null)
        {
            //Type[] types = new Type[2] { typeof(BlackWood.NodeCanvas.Editor.SkinContainer), typeof(BlackWood.NodeCanvas.Editor.BBPContainer) };
            //s_instance = GetWindow<ZEditor>("Unit Browser", true, types);
            s_instance = GetWindow<ZEditor>(false, "ZEditor");
        }
        //s_instance.Show();
        s_instance.Show();

        var testObject = new ObjectTest();
        var writer = new LitJson.JsonWriter { PrettyPrint = true };
        LitJson.JsonMapper.ToJson(testObject, writer);

        string path = "./Assets/test.json";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        File.WriteAllText(path, writer.ToString(), Encoding.UTF8);
    }
    private void OnEnable()
    {
        UnityEngine.Input.imeCompositionMode = IMECompositionMode.On;
    }
    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("text:", ZESetting.LayoutSetting("LabelFieldShort"));
        EditorGUILayout.TextField(UnityEngine.Random.value.ToString(), ZESetting.LayoutSetting("TextField"));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Ok", ZESetting.LayoutSetting("Button")))
        {
            m_showValue = m_value00;
        }
        else if (GUILayout.Button("Cancel", ZESetting.LayoutSetting("Button")))
        {
            m_showValue = "";
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        //EditorGUILayout.LabelField(m_showValue, ZESetting.LayoutSetting("LabelField"));
    }

    public static ZEditor s_instance = null;

    public string m_value00 = "";
    public string m_value01 = "";
    public string m_value02 = "";
    public string m_showValue = "";
}