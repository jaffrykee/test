using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInit : MonoBehaviour {
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
    GameObject createCell(Vector3 poi, Quaternion rot)
    {
        GameObject cell = new GameObject();
        cell.transform.position = poi;
        cell.transform.rotation = rot;
        cell.AddComponent<MeshFilter>();
        var mr = cell.AddComponent<MeshRenderer>();

        
        var mat = Resources.Load<Material>("Materials/normal");
        mr.material = mat;
        cell.transform.parent = gameObject.transform;

        var mesh = cell.GetComponent<MeshFilter>().mesh;
        mesh.vertices = getCellVertexs();
        mesh.triangles = getTriangles();
        return cell;
    }
	// Use this for initialization
	void Start () {
        s3 = Mathf.Sqrt(3);
        s23 = s3 * 0.5f;
        createCell(new Vector3(), new Quaternion());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    static float s3;
    static float s23;
    static float s_spacing = 0.1f;
}
