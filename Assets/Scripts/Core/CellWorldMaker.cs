using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TkmGame.Core {
    public class CellWorldMaker : MonoBehaviour {
        public Vector2 getTCtrlPos(GameObject obj) {
            //TerrainData td = new TerrainData();
            var tdata = m_terrain.terrainData;
            var pos = obj.transform.position - m_terrain.transform.position;
            return new Vector2(pos.x / tdata.size.x * tdata.heightmapWidth, pos.z / tdata.size.z * tdata.heightmapHeight);
        }
        //unsafe
        // 4 7    15 26    56 97
        //private void setFaa(Vector2Int pos, ref float[,] faa, float height, int n = 15, int m = 26)
        static private void setCellInit(ref float[,] faa, int n = 4, int m = 7, float height = 0.002f) {
            int col = faa.GetUpperBound(0) + 1;
            int row = faa.GetUpperBound(1) + 1;
            if (4 * n >= col || 4 * m >= row) {
                //error
                return;
            }
            s_curCelln = n;
            s_curCellm = m;
            s_curCellHeight = height;
            double mvn = (double)(m) / (n);
            for (int cn = 0; cn < n; cn++) {
                double cy = -(mvn) * cn + m;
                for (int cm = 0; cm <= m; cm++) {
                    if (cm >= cy) {
                        faa[cn, cm] = height;
                    }
                }
            }
            for (int cn = n; cn <= 2 * n; cn++) {
                for (int cm = 0; cm <= m; cm++) {
                    faa[cn, cm] = height;
                }
            }
            for (int cn = 0; cn < 2 * n; cn++) {
                for (int cm = 0; cm <= m; cm++) {
                    if (faa[cn, cm] == height) {
                        faa[4 * n - cn, cm] = height;
                    }
                }
            }
            for (int cn = 0; cn < 4 * n; cn++) {
                for (int cm = 0; cm <= m; cm++) {
                    if (faa[cn, cm] == height) {
                        faa[cn, 2 * m - cm] = height;
                    }
                }
            }
        }
        private void setCell(Vector2Int pos, ref float[,] faa, float height) {
            int curCol = faa.GetUpperBound(0) + 1;
            int curRow = faa.GetUpperBound(1) + 1;
            if (4 * s_curCelln + pos.x >= curCol || 4 * s_curCellm + pos.y >= curRow) {
                //error
                return;
            }
            int bufCol = s_cellBuffer.GetUpperBound(0) + 1;
            int bufRow = s_cellBuffer.GetUpperBound(1) + 1;
            for (int i = 0; i < bufCol; i++) {
                for (int j = 0; j < bufRow; j++) {
                    if (s_cellBuffer[i, j] == s_curCellHeight) {
                        faa[i + pos.x, j + pos.y] = height;
                    } else {
                        ;
                    }
                }
            }
        }
        // Use this for initialization
        void Start() {
            //const int mc = 50;
            m_terrain = Terrain.activeTerrain;
            s_cellBuffer = new float[c_n * 4 + 1, c_m * 4 + 1];
            setCellInit(ref s_cellBuffer, c_n, c_m, c_height);
            //m_terrain.terrainData.deta = 2;
            var tdata = m_terrain.terrainData;
            if (tdata != null) {
                var faa = new float[tdata.heightmapHeight, tdata.heightmapWidth];
                const int maxCountX = 100;
                const int maxCountY = 200;
                // 6 2
                const int dsx = 0;
                const int dsy = 0;
                int ci = (c_n * 6 + dsx);
                for (int j = 0; j < maxCountY; j++) {
                    if (j % 2 == 0) {
                        for (int i = 0; i < maxCountX; i++) {
                            setCell(new Vector2Int(i * ci, j * (c_m + dsy)), ref faa, Random.Range(0.000f, 0.01f));
                        }
                    } else {
                        for (int i = 0; i < maxCountX; i++) {
                            setCell(new Vector2Int(i * ci + ci / 2, j * (c_m + dsy)), ref faa, Random.Range(0.000f, 0.01f));
                        }
                    }
                }
                tdata.SetHeights(0, 0, faa);
            }
        }

        // Update is called once per frame
        void Update() {
        }

        GameObject m_cude;
        GameObject m_sphere;
        Rigidbody m_cudeRb;
        Rigidbody m_sphereRb;
        GameObject m_cb001;

        Terrain m_terrain;

        const int c_n = 4;
        const int c_m = 7;
        const float c_height = 0.001f;

        static float[,] s_cellBuffer;
        static int s_curCelln;
        static int s_curCellm;
        static float s_curCellHeight;
    }
}
