using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellData : MonoBehaviour
{
    static float s23 = Mathf.Sqrt(3) * 0.5f;
    static public Vector3[] getCellVertexs()
    {
        Vector3[] vertexs = new Vector3[6];
        //约定第一个顶点在原点,从左向右，从上到下的顺序排序
        vertexs[0] = new Vector3(-1, 0, 0);
        vertexs[1] = new Vector3(-0.5f, 0, s23);
        vertexs[2] = new Vector3(0.5f, 0, s23);
        vertexs[3] = new Vector3(1, 0, 0);
        vertexs[4] = new Vector3(0.5f, 0, -s23);
        vertexs[5] = new Vector3(-0.5f, 0, -s23);
        return vertexs;
    }
    static public Vector3[] getLinesVertexs(Vector3 poi)
    {
        Vector3[] vertexs = new Vector3[7];
        //约定第一个顶点在原点,从左向右，从上到下的顺序排序
        vertexs[0] = new Vector3(-1, 0, 0);
        vertexs[1] = new Vector3(-0.5f, 0, s23);
        vertexs[2] = new Vector3(0.5f, 0, s23);
        vertexs[3] = new Vector3(1, 0, 0);
        vertexs[4] = new Vector3(0.5f, 0, -s23);
        vertexs[5] = new Vector3(-0.5f, 0, -s23);
        vertexs[6] = new Vector3(-1, 0, 0);

        vertexs[0] += poi;
        vertexs[1] += poi;
        vertexs[2] += poi;
        vertexs[3] += poi;
        vertexs[4] += poi;
        vertexs[5] += poi;
        vertexs[6] += poi;
        return vertexs;
    }
    static public Vector3[] getHalfCellVertexs(Vector3 poi)
    {
        Vector3[] vertexs = new Vector3[6];
        //约定第一个顶点在原点,从左向右，从上到下的顺序排序
        vertexs[0] = new Vector3(-1, 0, 0);
        vertexs[1] = new Vector3(-0.5f, 0, s23);
        vertexs[2] = new Vector3(0.5f, 0, s23);

        vertexs[0] += poi;
        vertexs[1] += poi;
        vertexs[2] += poi;
        return vertexs;
    }
    public LineRenderer createMapLine()
    {
        if (gameObject == null)
        {
            return null;
        }
        var lr = new LineRenderer();
        return lr;
    }
    public void clearMapLine()
    {
        if (gameObject == null)
        {
            return;
        }
    }
    public CellMap getParentMap()
    {
        try
        {
            return gameObject.transform.parent.gameObject.GetComponent<CellMap>();
        }
        catch
        {
            return null;
        }
    }
    // Use this for initialization
    void Start () {
		if(gameObject == null)
        {
            return;
        }
        var lr = gameObject.AddComponent<LineRenderer>();
        lr.positionCount = 7;
        lr.numCapVertices = 7;
        var parent = lr.gameObject.transform.parent.gameObject;
        if(parent != null)
        {
            var cellMap = parent.GetComponent<CellMap>();
            if(cellMap != null)
            {
                lr.material = cellMap.m_matOutline;
                lr.startColor = cellMap.m_outlineColor;
                lr.endColor = cellMap.m_outlineColor;
            }
        }
        //lr.numCornerVertices = 7;
        var linePoi = m_centerPoi;
        linePoi.y += 0.01f;
        lr.SetPositions(getLinesVertexs(linePoi));
        lr.startWidth = c_normalWidth;
        lr.endWidth = c_normalWidth;
        lr.startColor = Color.gray;
        lr.endColor = Color.gray;
        
    }
	// Update is called once per frame
	void Update () {
		
	}

    public Vector3 m_centerPoi;
    public Vector2Int m_cellPoi;
    public enum OutlineType
    {
        none,
        normal,
    }
    private OutlineType mt_outlineType = OutlineType.none;
    public OutlineType m_outlineType
    {
        get
        {
            return mt_outlineType;
        }
        set
        {
            if (mt_outlineType != value)
            {
                mt_outlineType = value;
                if(gameObject == null)
                {
                    return;
                }
                var lr = gameObject.GetComponent<LineRenderer>();
                if(lr == null)
                {
                    return;
                }
                switch(mt_outlineType)
                {
                    case OutlineType.none:
                        lr.enabled = false;
                        break;
                    case OutlineType.normal:
                        lr.enabled = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
    const float c_normalWidth = 0.05f;
    const float c_selectWidth = 0.15f;
}
