using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TkmGame.Gtr.Battle;

namespace TkmGame.Gtr.Controller {
    public class MeshTouch : MonoBehaviour {
        void Update() {
            if (Input.GetMouseButtonDown(0)) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) {
                    if (hit.transform == null) {
                        return;
                    }
                    MeshFilter meshFilter = hit.transform.GetComponent<MeshFilter>();
                    if (meshFilter == null) {
                        return;
                    }
                    if (m_isHiddenTouch) {
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
                    var curCell = meshFilter.gameObject;
                    if (curCell != null) {
                        var cellData = curCell.GetComponent<Cell>();
                        if (cellData != null) {
                            //var render = curCell.GetComponent<Renderer>();
                            //render.enabled = !render.enabled;
                            //curCell.SetActive(!curCell.activeSelf);

                            var map = cellData.getParentMap();
                            if (map != null) {
                                map.setCurCell(cellData);
                            }
                        }
                    }
                }
            }
        }

        bool m_isHiddenTouch = false;
    }
}
