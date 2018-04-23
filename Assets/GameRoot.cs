using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    public Vector2 getTCtrlPos(GameObject obj)
    {
        //TerrainData td = new TerrainData();
        var tdata = m_terrain.terrainData;
        var pos = obj.transform.position - m_terrain.transform.position;
        return new Vector2(pos.x / tdata.size.x * tdata.heightmapWidth, pos.z / tdata.size.z * tdata.heightmapHeight);
    }
    //unsafe
    // 4 7    15 26    56 97
    //private void setFaa(Vector2Int pos, ref float[,] faa, float height, int n = 15, int m = 26)
    private void setFaa(Vector2Int pos, ref float[,] faa, float height, int n = 4, int m = 7)
    {
        int col = faa.GetUpperBound(0) + 1;
        int row = faa.GetUpperBound(1) + 1;
        if(pos.x + 4 * n > col || pos.y + 4 * m > row)
        {
            //error
            return;
        }
        double mvn = (double)(m) / (n);
        for (int cn = 0; cn < n; cn++)
        {
            double cy = -(mvn) * cn + m;
            for (int cm = 0; cm <= m; cm++)
            {
                if(cm >= cy)
                {
                    faa[cn + pos.x, cm + pos.y] = height;
                }
            }
        }
        for (int cn = n; cn <= 2 * n; cn ++)
        {
            for (int cm = 0; cm <= m; cm++)
            {
                faa[cn + pos.x, cm + pos.y] = height;
            }
        }
        for (int cn = 0; cn < 2 * n; cn++)
        {
            for (int cm = 0; cm <= m; cm++)
            {
                faa[4 * n - cn + pos.x, cm + pos.y] = faa[cn + pos.x, cm + pos.y];
            }
        }
        for (int cn = 0; cn < 4 * n; cn++)
        {
            for (int cm = 0; cm <= m; cm++)
            {
                faa[cn + pos.x, 2 * m - cm + pos.y] = faa[cn + pos.x, cm + pos.y];
            }
        }
    }
    // Use this for initialization
    void Start()
    {
#if false
        m_cude = GameObject.Find("cb000");
        m_sphere = GameObject.Find("sp000");
        m_cudeRb = m_cude.GetComponent<Rigidbody>();
        m_sphereRb = m_sphere.GetComponent<Rigidbody>();
        for(int i = 0; i < 512; i++)
        {
            GameObject curCude = Instantiate(m_cude);
            curCude.transform.position = new Vector3(-70 + (i / 64) * 20, 40 + (i % 64 / 8) * 20, -80 + (i % 8) * 20);
        }
        for (int i = 0; i < 512; i++)
        {
            GameObject curCude = Instantiate(m_cude);
            curCude.transform.position = new Vector3(-70 + (i / 64) * 20, 40 + (i % 64 / 8) * 20, -80 + (i % 8) * 20);
        }
        for (int i = 0; i < 512; i++)
        {
            GameObject curCude = Instantiate(m_cude);
            curCude.transform.position = new Vector3(-70 + (i / 64) * 20, 40 + (i % 64 / 8) * 20, -80 + (i % 8) * 20);
        }
        for (int i = 0; i < 512; i++)
        {
            GameObject curCude = Instantiate(m_cude);
            curCude.transform.position = new Vector3(-70 + (i / 64) * 20, 40 + (i % 64 / 8) * 20, -80 + (i % 8) * 20);
        }
#endif
        //const int mc = 50;
        m_terrain = Terrain.activeTerrain;
        //m_terrain.terrainData.deta = 2;
        var tdata = m_terrain.terrainData;
        if (tdata != null)
        {
#if false
            m_cb001 = GameObject.Find("cb001");
            double unitLen = 0.1d / (tdata.heightmapHeight + tdata.heightmapWidth);
            double ground = 0.166666666666666;
            var faa = new float[tdata.heightmapHeight, tdata.heightmapWidth];
            for (int x = 0; x < tdata.heightmapWidth; x++)
            {
                for (int y = 0; y < tdata.heightmapHeight; y++)
                {
                    faa[x, y] = (float)(ground + unitLen * (x + y));
                }
            }
            tdata.SetHeights(0, 0, faa);
#endif
            var faa = new float[tdata.heightmapHeight, tdata.heightmapWidth];
            setFaa(new Vector2Int(0, 0), ref faa, 0.01f);
            tdata.SetHeights(0, 0, faa);
        }
    }

    // Update is called once per frame
    void Update()
    {
#if false
        if (m_cude == null || m_cudeRb == null || m_sphere == null || m_sphereRb == null)
        {
            return;
        }

        float curPowerCude = s_initPower;
        if(Input.GetKey(KeyCode.LeftControl))
        {
            curPowerCude *= s_exTimes;
        }
        if (Input.GetKey(KeyCode.W))
        {
            m_cudeRb.AddForce(curPowerCude, 0, 0);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            m_cudeRb.AddForce(-curPowerCude, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_cudeRb.AddForce(0, 0, -curPowerCude);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            m_cudeRb.AddForce(0, 0, curPowerCude);
        }

        float curPowerSphere = s_initPower;
        if (Input.GetKey(KeyCode.RightControl))
        {
            curPowerSphere *= s_exTimes;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_sphereRb.AddForce(curPowerSphere, 0, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            m_sphereRb.AddForce(-curPowerSphere, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_sphereRb.AddForce(0, 0, -curPowerSphere);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_sphereRb.AddForce(0, 0, curPowerSphere);
        }
        if(Input.GetKey(KeyCode.Space))
        {
            m_sphereRb.AddForce(0, curPowerSphere, 0);
        }
#endif
        //Input.GetKey(KeyCode.)
    }

    GameObject m_cude;
    GameObject m_sphere;
    Rigidbody m_cudeRb;
    Rigidbody m_sphereRb;
    GameObject m_cb001;

    Terrain m_terrain;

    static float s_exTimes = 5;
    static float s_initPower = 200000;
}
