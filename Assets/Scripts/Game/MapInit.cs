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
        Material[] mats = new Material[2];
        if(m_mat0)
        {
            mats[0] = m_mat0;
            if(m_mat1)
            {
                mats[1] = m_mat1;
            }
        }
        if (mats[0] != null)
        {
            mats[0].color = m_color0;
        }
        if (mats[1] != null)
        {
            mats[1].color = m_color1;
        }
        mr.materials = mats;
        mr.material = mats[0];
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
                    createCell(new Vector3(i * di + dx, 0, j * dj + dy));
                }
            }
            else
            {
                for (int j = 0; j < m_countY; j++)
                {
                    createCell(new Vector3(i * di + dx, 0, (j - 0.5f) * dj + dy));
                }
            }
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
    public Color m_color0 = Color.gray;
    public Color m_color1 = Color.gray;
    public Material m_mat0;
    public Material m_mat1;
}
