using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

[System.Serializable]
public class CellMap : MonoBehaviour
{
    GameObject createCell(Vector3 poi, CellData data, int cellx = 0, int celly = 0, Quaternion rot = new Quaternion())
    {
        GameObject cell = new GameObject();
        cell.transform.position = poi;
        cell.transform.rotation = rot;
        var mf = cell.AddComponent<MeshFilter>();
        var mr = cell.AddComponent<MeshRenderer>();
        var mc = cell.AddComponent<MeshCollider>();
        var cellData = cell.AddComponent<Cell>();
        mc.sharedMesh = mf.mesh;
        mr.material = m_matCell;
        var mesh = mf.mesh;
        mesh.vertices = Cell.getCellVertexs();
        mesh.triangles = getTriangles();
        cell.transform.parent = gameObject.transform;
        cellData.resetData(data);
        cellData.m_centerPoi = poi;
        cellData.m_cellPoi = new Vector2Int(cellx, celly);
        if (m_showOutline == false)
        {

        }
        m_cellData[cellx, celly] = cellData;
        return cell;
    }
    public void resetData()
    {
        if (gameObject == null || m_mapSizeX <= 0 || m_mapSizeY <= 0)
        {
            return;
        }
        if(m_curData == null)
        {
            m_curData = new CellMapData(2, 2);
        }
        m_mapSizeX = m_curData.mapSizeX;
        m_mapSizeY = m_curData.mapSizeY;
        m_cellData = new Cell[m_mapSizeX, m_mapSizeY];
        float di = (3 + m_spacing * Mathf.Sqrt(3)) * 0.5f;
        float dj = Mathf.Sqrt(3) + m_spacing;

        float dx, dy;
        if (m_isCenter == true)
        {
            if (m_mapSizeX > 1)
            {
                dx = -(m_mapSizeX - 1) * di * 0.5f;
                dy = -(m_mapSizeY - 1.5f) * dj * 0.5f;
            }
            else
            {
                dx = 0;
                dy = -(m_mapSizeY - 1) * dj * 0.5f;
            }
        }
        else
        {
            dx = 0;
            dy = 0;
        }

        for (int i = 0; i < m_mapSizeX; i++)
        {
            if (i % 2 == 0)
            {
                for (int j = 0; j < m_mapSizeY; j++)
                {
                    createCell(new Vector3(i * di + dx, 0, j * dj + dy), m_curData.cellData[i * m_mapSizeY + j], i, j);
                }
            }
            else
            {
                for (int j = 0; j < m_mapSizeY; j++)
                {
                    createCell(new Vector3(i * di + dx, 0, (j - 0.5f) * dj + dy), m_curData.cellData[i * m_mapSizeY + j], i, j);
                }
            }
        }
    }
    public void resetData(CellMapData newData)
    {
        m_curData = newData;
        resetData();
    }
    public void resetData(string resPath)
    {
        //Resources
        var test = Resources.Load("Data/CellMap/default.txt");
        TextAsset jsonText = Resources.Load("Data/CellMap/default.txt") as TextAsset;
        if(jsonText != null)
        {
            var cellMapData = JsonMapper.ToObject<CellMapData>(jsonText.text);
            resetData(cellMapData);
        } else
        {
            resetData();
        }
    }
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
    // Use this for initialization
    void Start()
    {
        if(m_curData == null)
        {
            resetData(c_defaultData);
        }
        else
        {
            resetData();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setCurCell(Cell curCell)
    {
        m_curCell = curCell;
        if (m_curCell != null)
        {
            if (m_selectLine == null)
            {
                var comp = new System.Type[1];
                comp[0] = typeof(LineRenderer);
                m_selectLine = new GameObject("SelectCellLine", comp);
            }
            var lr = m_selectLine.GetComponent<LineRenderer>();
            if (lr != null)
            {
                lr.positionCount = 7;
                lr.numCornerVertices = 6;
                var linePoi = m_curCell.m_centerPoi;
                linePoi.y += m_selectLineHeight;
                lr.SetPositions(Cell.getLinesVertexs(linePoi));
                lr.material = m_selectLineMaterial;
                lr.startColor = m_selectLineColor;
                lr.endColor = m_selectLineColor;
                lr.startWidth = m_selectLineWidth;
                lr.endWidth = m_selectLineWidth;
            }
        }
    }

    const string c_dataFolder = "Data/CellMap/";
    const string c_defaultData = c_dataFolder + "default.json";

    public int m_mapSizeX = 10;
    public int m_mapSizeY = 20;
    public bool m_isCenter = true;
    public float m_spacing = 0;
    public float m_x = 0;
    public float m_y = 0;
    public float m_z = 0;
    //outLight
    public Material m_matCell;
    public bool m_showOutline = true;
    public bool m_isQuickOutline = true;
    //CellMapOutline
    public Material m_outlineMaterial;
    public Color m_outlineColor = Color.gray;
    public float m_outlineWidth = 0.05f;
    public float m_outlineHeight = 0.01f;
    //CellMapSelectLine
    public Material m_selectLineMaterial;
    public Color m_selectLineColor = Color.white;
    public float m_selectLineWidth = 0.1f;
    public float m_selectLineHeight = 0.15f;
    [HideInInspector]
    public Cell[,] m_cellData;
    [HideInInspector]
    public Cell m_curCell = null;
    private GameObject m_selectLine = null;
    public CellMapData m_curData = null;
}
