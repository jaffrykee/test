using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CellMapPreview
{
    public CellMapPreview()
    {
        m_cellMapObject = AssetDatabase.LoadAssetAtPath<GameObject>(c_path);
    }
    public void OnGUI(Rect pos)
    {
#if false
        if(m_cellMapObject == null)
        {
            m_cellMapObject = AssetDatabase.LoadAssetAtPath<GameObject>(c_path);
        }
        if (m_cellMapObject != null)
        {
            if (m_cellMapObjectEditor == null)
            {
                m_cellMapObjectEditor = Editor.CreateEditor(m_cellMapObject);
                m_curObject = m_cellMapObject;
            }

            if (m_curObject != m_cellMapObject)
            {
                Object.DestroyImmediate(m_cellMapObjectEditor);
                m_cellMapObjectEditor = Editor.CreateEditor(m_cellMapObject);
                m_curObject = m_cellMapObject;
            }

            m_cellMapObjectEditor.OnPreviewGUI(GUILayoutUtility.GetRect(pos.width, pos.height), EditorStyles.textField);
        }
        else
        {
            GUIStyle gs = new GUIStyle();
            gs.alignment = TextAnchor.MiddleCenter;
            gs.fontSize = 40;
            gs.fontStyle = FontStyle.Bold;
            EditorGUILayout.TextField("No GameObject", gs, GUILayout.Width(pos.width), GUILayout.Height(pos.height));
        }
#else
        if (m_cellMapObject != null)
        {
            if (m_cellMapObjectEditor == null)
            {
                m_cellMapObjectEditor = Editor.CreateEditor(m_cellMapObject);
                m_curObject = m_cellMapObject;
            }
            if (m_curObject != m_cellMapObject)
            {
                Object.DestroyImmediate(m_cellMapObjectEditor);
                m_cellMapObjectEditor = Editor.CreateEditor(m_cellMapObject);
                m_curObject = m_cellMapObject;
            }
            m_cellMapObjectEditor.OnPreviewGUI(GUILayoutUtility.GetRect(pos.width, pos.height), EditorStyles.textField);
        }
        else
        {
            GUIStyle gs = new GUIStyle();
            gs.alignment = TextAnchor.MiddleCenter;
            gs.fontSize = 40;
            gs.fontStyle = FontStyle.Bold;
            EditorGUILayout.TextField("No GameObject", gs, GUILayout.Width(pos.width), GUILayout.Height(pos.height));
        }
#endif
    }

    const string c_path = "Assets/CellMap.prefab";
    const string c_scenePath = "Assets/Scenes/CellMapEditor.unity";
    GameObject m_cellMapObject;
    GameObject m_curObject;
    Editor m_cellMapObjectEditor;
}
