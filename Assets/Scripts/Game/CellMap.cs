﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CellMap : MonoBehaviour
{
    static float s23 = Mathf.Sqrt(3) * 0.5f;
    private int[] getTriangles()
    {
        int[] triangles = new int[12];

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        triangles[6] = 0;
        triangles[7] = 3;
        triangles[8] = 4;

        triangles[9] = 0;
        triangles[10] = 4;
        triangles[11] = 5;

        return triangles;
    }
    GameObject createCell(Vector3 poi, int cellx = 0, int celly = 0, Quaternion rot = new Quaternion())
    {
        GameObject cell = new GameObject();
        cell.transform.position = poi;
        cell.transform.rotation = rot;
        var mf = cell.AddComponent<MeshFilter>();
        var mr = cell.AddComponent<MeshRenderer>();
        var mc = cell.AddComponent<MeshCollider>();
        var cellData = cell.AddComponent<CellData>();
        mc.sharedMesh = mf.mesh;
        mr.material = m_matCell;
        var mesh = mf.mesh;
        mesh.vertices = CellData.getCellVertexs();
        mesh.triangles = getTriangles();
        cell.transform.parent = gameObject.transform;
        cellData.m_centerPoi = poi;
        cellData.m_cellPoi = new Vector2Int(cellx, celly);
        if(m_showOutline == false)
        {

        }
        m_cellData[cellx, celly] = cellData;
        return cell;
    }
	// Use this for initialization
	void Start ()
    {
        if (gameObject == null || m_countX <= 0 || m_countY <= 0)
        {
            return;
        }
        m_cellData = new CellData[m_countX, m_countY];
        float di = (3 + m_spacing * Mathf.Sqrt(3)) * 0.5f;
        float dj = Mathf.Sqrt(3) + m_spacing;

        float dx, dy;
        if (m_isCenter == true)
        {
            if (m_countX > 1)
            {
                dx = -(m_countX - 1) * di * 0.5f;
                dy = -(m_countY - 1.5f) * dj * 0.5f;
            }
            else
            {
                dx = 0;
                dy = -(m_countY - 1) * dj * 0.5f;
            }
        }
        else
        {
            dx = 0;
            dy = 0;
        }

        for (int i = 0; i < m_countX; i++)
        {
            if (i % 2 == 0)
            {
                for (int j = 0; j < m_countY; j++)
                {
                    createCell(new Vector3(i * di + dx, 0, j * dj + dy), i, j);
                }
            }
            else
            {
                for (int j = 0; j < m_countY; j++)
                {
                    createCell(new Vector3(i * di + dx, 0, (j - 0.5f) * dj + dy), i, j);
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
    }

    public void setCurCell(CellData curCell)
    {
        m_curCell = curCell;
        if(m_curCell != null)
        {
            if(m_selectLine == null)
            {
                var comp = new System.Type[1];
                comp[0] = typeof(LineRenderer);
                m_selectLine = new GameObject("SelectCellLine", comp);
            }
            var lr = m_selectLine.GetComponent<LineRenderer>();
            if(lr != null)
            {
                lr.positionCount = 7;
                lr.numCornerVertices = 6;
                var linePoi = m_curCell.m_centerPoi;
                linePoi.y += m_selectLineHeight;
                lr.SetPositions(CellData.getLinesVertexs(linePoi));
                lr.material = m_selectLineMaterial;
                lr.startColor = m_selectLineColor;
                lr.endColor = m_selectLineColor;
                lr.startWidth = m_selectLineWidth;
                lr.endWidth = m_selectLineWidth;
            }
        }
    }

    public int m_countX = 10;
    public int m_countY = 20;
    public bool m_isCenter = true;
    public float m_spacing = 0.1f;
    public float m_x = 0;
    public float m_y = 0;
    public float m_z = 0;
    public Material m_matCell;
    public bool m_showOutline = true;
    public bool m_isQuickOutline = true;
    public Material m_outlineMaterial;
    public Color m_outlineColor = Color.gray;
    public float m_outlineWidth = 0.05f;
    public float m_outlineHeight = 0.01f;
    public Material m_selectLineMaterial;
    public Color m_selectLineColor = Color.blue;
    public float m_selectLineWidth = 0.15f;
    public float m_selectLineHeight = 0.1f;
    [HideInInspector]
    public CellData[,] m_cellData;
    [HideInInspector]
    public CellData m_curCell = null;
    private GameObject m_selectLine = null;
}