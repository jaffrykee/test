using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapInit : MonoBehaviour
{
    static float s23 = Mathf.Sqrt(3) * 0.5f;
    private Vector3[] getCellVertexs()
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
    GameObject createCell(Vector3 poi, Quaternion rot = new Quaternion())
    {
        GameObject cell = new GameObject();
        cell.transform.position = poi;
        cell.transform.rotation = rot;
        cell.AddComponent<MeshFilter>();
        var mr = cell.AddComponent<MeshRenderer>();
        var mat = Resources.Load<Material>("Materials/normal");
        mat.color = m_color;
        mr.material = mat;
        cell.transform.parent = gameObject.transform;
        var mesh = cell.GetComponent<MeshFilter>().mesh;
        mesh.vertices = getCellVertexs();
        mesh.triangles = getTriangles();
        return cell;
    }
	// Use this for initialization
	void Start ()
    {
        if (gameObject == null)
        {
            return;
        }
        float di = (3 + m_spacing * Mathf.Sqrt(3)) * 0.5f;
        float dj = Mathf.Sqrt(3) + m_spacing;

        for(int i = 0; i < m_countX; i++)
        {
            if (i % 2 == 0)
            {
                for (int j = 0; j < m_countY; j++)
                {
                    createCell(new Vector3(i * di, 0, j * dj));
                }
            }
            else
            {
                for (int j = 0; j < m_countY; j++)
                {
                    createCell(new Vector3(i * di, 0, (j - 0.5f) * dj));
                }
            }
        }

        if(m_isCenter == true)
        {
            if(m_countX > 1)
            {
                gameObject.transform.position = new Vector3(m_x - ((m_countX - 1) * di * 0.5f), m_y, m_z - ((m_countY - 1.5f) * dj * 0.5f));
            }
            else
            {
                gameObject.transform.position = new Vector3(m_x, m_y, m_z - ((m_countY - 1) * dj * 0.5f));
            }
        }
        else
        {
            gameObject.transform.position = new Vector3(m_x, m_y, m_z);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
    }

    public int m_countX = 10;
    public int m_countY = 20;
    public bool m_isCenter = true;
    public float m_spacing = 0.1f;
    public float m_x = 0;
    public float m_y = 0;
    public float m_z = 0;
    public Color m_color = Color.gray;
}
