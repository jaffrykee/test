using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCellData : MonoBehaviour
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
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector3 m_centerPoi;
    public Vector2Int m_cellPoi;
}
