using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

namespace TkmGame.Gtr.Battle {
    /// <summary>
    /// 蜂窝地图。
    /// </summary>
    [System.Serializable]
    public class CellMap : MonoBehaviour {
        public const int c_cellSizeX = 100;
        public const int c_cellSizeY = 100;
        
        GameObject createCell(Vector3 poi, CellData data, int cellx = 0, int celly = 0, Quaternion rot = new Quaternion()) {
            GameObject cellObj = new GameObject();
            cellObj.transform.position = poi;
            cellObj.transform.rotation = rot;
            var mf = cellObj.AddComponent<MeshFilter>();
            var mr = cellObj.AddComponent<MeshRenderer>();
            var mc = cellObj.AddComponent<MeshCollider>();
            var newCell = cellObj.AddComponent<Cell>();
            mc.sharedMesh = mf.mesh;
            //mr.material = m_matCell;
            var mesh = mf.mesh;
            mesh.vertices = Cell.getCellVertexs();
            mesh.triangles = getTriangles();
            cellObj.transform.parent = gameObject.transform;
            newCell.m_data = data;
            newCell.m_centerPoi = poi;
            newCell.m_cellPoi = new Vector2Int(cellx, celly);
            if (m_showOutline == false) {

            }
            m_cellData[cellx, celly] = newCell;
            newCell.gameObject.SetActive(false);
            return cellObj;
        }
        public void resetData() {
            if (gameObject == null || m_mapSizeX <= 0 || m_mapSizeY <= 0) {
                return;
            }
            if (m_curData == null) {
                m_curData = new CellMapData(c_cellSizeX, c_cellSizeY);
                m_curData.rcf001();
            }
            m_mapSizeX = m_curData.mapSizeX;
            m_mapSizeY = m_curData.mapSizeY;
            m_cellData = new Cell[m_mapSizeX, m_mapSizeY];
            float di = (3 + m_spacing * Mathf.Sqrt(3)) * 0.5f;
            float dj = Mathf.Sqrt(3) + m_spacing;

            float dx, dy;
            if (m_isCenter == true) {
                if (m_mapSizeX > 1) {
                    dx = -(m_mapSizeX - 1) * di * 0.5f;
                    dy = -(m_mapSizeY - 1.5f) * dj * 0.5f;
                } else {
                    dx = 0;
                    dy = -(m_mapSizeY - 1) * dj * 0.5f;
                }
            } else {
                dx = 0;
                dy = 0;
            }

            for (int i = 0; i < m_mapSizeX; i++) {
                if (i % 2 == 0) {
                    for (int j = 0; j < m_mapSizeY; j++) {
                        createCell(new Vector3(i * di + dx, 0, j * dj + dy), m_curData.cellData[i + m_mapSizeX * j], i, j);
                    }
                } else {
                    for (int j = 0; j < m_mapSizeY; j++) {
                        createCell(new Vector3(i * di + dx, 0, (j - 0.5f) * dj + dy), m_curData.cellData[i + m_mapSizeX * j], i, j);
                    }
                }
            }

            foreach (var cell in m_cellData) {
                //cell.gameObject.SetActive(false);
            }
        }
        public void resetData(CellMapData newData) {
            m_curData = newData;
            resetData();
        }
        public void resetData(string resPath) {
            //Resources
            if (false) {
                TextAsset jsonText = Resources.Load(resPath) as TextAsset;
                if (jsonText != null) {
                    var cellMapData = JsonMapper.ToObject<CellMapData>(jsonText.text);
                    resetData(cellMapData);
                } else {
                    resetData();
                }
            } else {
                resetData();
            }
        }
        private int[] getTriangles() {
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
        void Start() {
            BattleManager.instance().m_curCellMap = this;
            if (m_curData == null) {
                resetData(c_defaultData);
            } else {
                resetData();
            }
        }

        // Update is called once per frame
        void Update() {
        }

        public void setCurCell(Cell curCell) {
            m_curCell = curCell;
            if (m_curCell != null) {
                if (m_selectLine == null) {
                    var comp = new System.Type[1];
                    comp[0] = typeof(LineRenderer);
                    m_selectLine = new GameObject("SelectCellLine", comp);
                }
                var lr = m_selectLine.GetComponent<LineRenderer>();
                if (lr != null) {
                    lr.positionCount = 7;
                    lr.numCornerVertices = 6;
                    var linePoi = m_curCell.m_centerPoi;
                    linePoi.y += m_selectLineHeight;
                    var curHeight = m_curCell.m_data.height;
                    if (curHeight > 0) {
                        linePoi.y += (curHeight * Cell.c_height);
                    }
                    lr.SetPositions(Cell.getLinesVertexs(linePoi));
                    lr.material = m_selectLineMaterial;
                    lr.startColor = m_selectLineColor;
                    lr.endColor = m_selectLineColor;
                    lr.startWidth = m_selectLineWidth;
                    lr.endWidth = m_selectLineWidth;
                }
            }
            Debug.Log(curCell.m_cellPoi.ToString() + curCell.m_centerPoi.ToString());
        }

        #region Neighbor
        public bool isLegal(int x, int y) {
            bool ret = false;
            if (x >= 0 && y >= 0 && x < c_cellSizeX && y < c_cellSizeY) {
                ret = true;
            }
            return ret;
        }
        public Cell assignNeighbor(int nx, int ny) {
            Cell ret = null;
            if (isLegal(nx, ny) == true) {
                ret = m_cellData[nx, ny];
            }
            return ret;
        }
        public Cell getNeighbor0(int x, int y) {
            if (x % 2 == 0) {
                return assignNeighbor(x, y + 1);
            } else {
                return assignNeighbor(x, y + 1);
            }
        }
        public Cell getNeighbor2(int x, int y) {
            if (x % 2 == 0) {
                return assignNeighbor(x + 1, y + 1);
            } else {
                return assignNeighbor(x + 1, y);
            }
        }
        public Cell getNeighbor4(int x, int y) {
            if (x % 2 == 0) {
                return assignNeighbor(x + 1, y);
            } else {
                return assignNeighbor(x + 1, y - 1);
            }
        }
        public Cell getNeighbor6(int x, int y) {
            if (x % 2 == 0) {
                return assignNeighbor(x, y - 1);
            } else {
                return assignNeighbor(x, y - 1);
            }
        }
        public Cell getNeighbor8(int x, int y) {
            if (x % 2 == 0) {
                return assignNeighbor(x - 1, y);
            } else {
                return assignNeighbor(x - 1, y - 1);
            }
        }
        public Cell getNeighbor10(int x, int y) {
            if (x % 2 == 0) {
                return assignNeighbor(x - 1, y + 1);
            } else {
                return assignNeighbor(x - 1, y);
            }
        }
        private delegate Cell GetNeighborFunc_D(int x, int y);
        private void setCellNeighborsVisibleFunc(Cell center, int range, GetNeighborFunc_D mainFunc, GetNeighborFunc_D subFunc, bool value) {
            Cell curCell = center;
            Cell curSubCell = null;
            for (int i = 0; i < range; i++) {
                curCell = mainFunc(curCell.m_cellPoi.x, curCell.m_cellPoi.y);
                if (curCell != null) {
                    curSubCell = curCell;
                    for (int j = 0; j < range - 1; j++) {
                        curSubCell = subFunc(curSubCell.m_cellPoi.x, curSubCell.m_cellPoi.y);
                        if (curSubCell != null) {
                            curSubCell.gameObject.SetActive(value);
                        } else {
                            break;
                        }
                    }
                    curCell.gameObject.SetActive(value);
                } else {
                    break;
                }
            }
        }
        public void setCellNeighborsVisible(Cell center, int range, bool value = true) {
            center.gameObject.SetActive(value);
            setCellNeighborsVisibleFunc(center, range, getNeighbor0, getNeighbor4, value);
            setCellNeighborsVisibleFunc(center, range, getNeighbor2, getNeighbor6, value);
            setCellNeighborsVisibleFunc(center, range, getNeighbor4, getNeighbor8, value);
            setCellNeighborsVisibleFunc(center, range, getNeighbor6, getNeighbor10, value);
            setCellNeighborsVisibleFunc(center, range, getNeighbor8, getNeighbor0, value);
            setCellNeighborsVisibleFunc(center, range, getNeighbor10, getNeighbor2, value);
        }
    #endregion

        //屏幕坐标法线所穿过的Cell。
        public static Cell getCellByScreenPoisition(Vector3 position, bool isHiddenTouch) {
            Cell ret = null;
            Ray ray = Camera.main.ScreenPointToRay(position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform != null) {
                    MeshFilter meshFilter = hit.transform.GetComponent<MeshFilter>();
                    if (meshFilter != null) {
                        if (isHiddenTouch) {
                            Mesh mesh = meshFilter.mesh;
                            int[] triangles = mesh.triangles;
                            List<int> tris = new List<int>(triangles);
                            tris.RemoveAt(hit.triangleIndex * 3 + 2);
                            tris.RemoveAt(hit.triangleIndex * 3 + 1);
                            tris.RemoveAt(hit.triangleIndex * 3);
                            int[] newTri = tris.ToArray();
                            mesh.triangles = newTri;
                            mesh.RecalculateNormals();
                            mesh.RecalculateBounds();
                            MeshCollider collider1 = hit.transform.GetComponent<MeshCollider>();
                            collider1.sharedMesh = mesh;
                        }
                        var objCell = meshFilter.gameObject;
                        if (objCell != null) {
                            ret = objCell.GetComponent<Cell>();
                        }
                    }
                }
            }
            return ret;
        }

        const string c_dataFolder = "Data/CellMap/";
        const string c_defaultData = c_dataFolder + "default";

        public int m_mapSizeX = 10;
        public int m_mapSizeY = 10;
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
}