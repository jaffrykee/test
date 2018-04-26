using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshTouch : MonoBehaviour
{
    void Update()
    {
        /**点选*/
        if (Input.GetMouseButtonDown(0))
        {//点出鼠标左键  
            //通过鼠标点击，摄像机从屏幕上一点发射出一条射线，此处的参数应为屏幕坐标  
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;//用来保存射线碰撞信息  
            //射线投射  
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == null)//如果射线没有碰撞到Transform,return  
                    return;
                //获取hit.transform的MeshFilter组件  
                MeshFilter meshFilter = hit.transform.GetComponent<MeshFilter>();
                if (meshFilter == null)
                    return;
                //获取hit.transform的mesh  
                Mesh mesh = meshFilter.mesh;

                //取出mesh的顶点和三角形数组  
                int[] triangles = mesh.triangles;
                //new一个List将mesh的三角形数组拷贝来，方便删除修改  
                //将Array数组转换为List  
                List<int> tris = new List<int>(triangles);
                //hit.triangleIndex为射线碰撞到的三角面的索引，从0开始  
                //三角面由三个顶点组成，  
                //tris(hit.triangleIndex*3+2)为第triangleIndex个三角面的第三个顶点  
                //tris(hit.triangleIndex*3+2)为第triangleIndex个三角面的第二个顶点  
                //tris(hit.triangleIndex*3+2)为第triangleIndex个三角面的第一个顶点  
                tris.RemoveAt(hit.triangleIndex * 3 + 2);
                tris.RemoveAt(hit.triangleIndex * 3 + 1);
                tris.RemoveAt(hit.triangleIndex * 3);
                //将List转换为Array数组  
                int[] newTri = tris.ToArray();

                mesh.triangles = newTri;//更新三角面数组  
                mesh.RecalculateNormals();//更新法线  
                mesh.RecalculateBounds();//更新包围体  
                //获取MeshCollider组件，更新用于检测碰撞的sharedMesh  
                MeshCollider collider1 = hit.transform.GetComponent<MeshCollider>();
                collider1.sharedMesh = mesh;
                //检测碰撞必须给物体添加MeshCollider组件  

            }
        }
    }
}
