using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private const float c_dColor = 0.2f;
    private const uint c_max = 2;
    private const float c_minColor = 0.3f;
    private const float c_maxColor = c_minColor + c_max * c_dColor;
    static Dictionary<uint, Color> colorDictionary()
    {
        var cd = new Dictionary<uint, Color>
        {
            {c_cbtnStateNormal,     new Color(c_minColor, c_maxColor, c_minColor, 1.0f)},
            {c_cbtnStateCurSelect,  new Color(1.0f, 1.0f, 1.0f, 1.0f)},
            {c_cbtnStateDisable,    new Color(0.1f, 0.1f, 0.1f, 1.0f)},
        };
        for (uint i = 1; i <= c_max * 2; i++)
        {
            if(i <= c_max)
            {
                cd.Add(c_cbtnStateNormal + i, new Color(c_minColor + c_dColor * i, c_maxColor, c_minColor, 1.0f));
                cd.Add(c_cbtnStateNormal - i, new Color(c_minColor, c_maxColor, c_minColor + c_dColor * i, 1.0f));
            }
            else
            {
                cd.Add(c_cbtnStateNormal + i, new Color(c_maxColor, c_maxColor - c_dColor * (i - c_max), c_minColor, 1.0f));
                cd.Add(c_cbtnStateNormal - i, new Color(c_minColor, c_maxColor - c_dColor * (i - c_max), c_maxColor, 1.0f));
            }
        }
        return cd;
    }
    static float s23 = Mathf.Sqrt(3) * 0.5f;
    private const int c_cbtnStateNormal = 0x10000;
    private const int c_cbtnStateCurSelect = 0x20000;
    private const int c_cbtnStateDisable = 0x30000;
    static public Dictionary<uint, Color> s_cellColor = colorDictionary();
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
        vertexs[6] = new Vector3(-1.0f, 0, 0);

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
        Vector3[] vertexs = new Vector3[4];
        //约定第一个顶点在原点,从左向右，从上到下的顺序排序
        vertexs[0] = new Vector3(-1, 0, 0);
        vertexs[1] = new Vector3(-0.5f, 0, s23);
        vertexs[2] = new Vector3(0.5f, 0, s23);
        vertexs[3] = new Vector3(1, 0, 0);

        vertexs[0] += poi;
        vertexs[1] += poi;
        vertexs[2] += poi;
        vertexs[3] += poi;
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
        var cellMap = getParentMap();
        var linePoi = m_centerPoi;
        if (cellMap != null)
        {
            lr.material = cellMap.m_outlineMaterial;
            lr.startColor = cellMap.m_outlineColor;
            lr.endColor = cellMap.m_outlineColor;
            lr.enabled = cellMap.m_showOutline;
            if(cellMap.m_showOutline == true)
            {
                m_outlineType = OutlineType.normal;
            }
            else
            {
                m_outlineType = OutlineType.none;
            }
            linePoi.y += cellMap.m_outlineHeight;
            lr.startWidth = cellMap.m_outlineWidth;
            lr.endWidth = cellMap.m_outlineWidth;
            if(cellMap.m_isQuickOutline == true)
            {
                lr.positionCount = 4;
                lr.numCornerVertices = 6;
                lr.SetPositions(getHalfCellVertexs(linePoi));
            }
            else
            {
                lr.positionCount = 7;
                lr.numCornerVertices = 6;
                lr.SetPositions(getLinesVertexs(linePoi));
            }
        }
        m_data = m_data;
    }
	// Update is called once per frame
	void Update () {

    }
    private Color getCellColor(CellData data)
    {
        Color c = Color.white;
        if (s_cellColor.TryGetValue((uint)(c_cbtnStateNormal + data.height), out c) == true)
        {
        }
        return c;
    }
    private List<Color> getCellColors(CellData data)
    {
        var colorList = new List<Color>();
        Color c = getCellColor(data);
        for (int i = 0; i < 6; i++)
        {
            colorList.Add(c);
        }
        return colorList;
    }

    public const float c_height = 0.2f;
    private CellData mt_data = new CellData();
    public CellData m_data
    {
        get
        {
            return mt_data;
        }
        set
        {
            mt_data = value;
            if (gameObject == null)
            {
                return;
            }
            gameObject.SetActive(!value.disable);
            var np = gameObject.transform.position;
            np.y = (value.height * c_height);
            gameObject.transform.position = np;
            var mr = gameObject.GetComponent<MeshRenderer>();
            mr.material.color = getCellColor(value);
        }
    }

    private Vector3 mt_centerPoi;
    public Vector3 m_centerPoi
    {
        get
        {
            return mt_centerPoi;
        }
        set
        {
            mt_centerPoi = value;
        }
    }
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
}
