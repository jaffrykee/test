using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDebug : MonoBehaviour {
    private Vector2 mMouseStart, mMouseEnd;
    private bool mBDrawMouseRect;
    public Shader m_lineShader;
    public Texture2D m_lineTexture;
    private Material m_lineMat = null;//画线的材质 不设定系统会用当前材质画线 结果不可控

    void Start() {
        mBDrawMouseRect = false;
        Color clr = Color.red;
        clr.a = 0.5f;
        m_lineMat = new Material(m_lineShader);
        m_lineMat.color = clr;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0))
        //按下鼠标左键  
        {
            Vector3 mousePosition = Input.mousePosition;
            mMouseStart = new Vector2(mousePosition.x, mousePosition.y);
        }

        if (Input.GetMouseButton(0))
        //持续按下鼠标左键  
        {
            mBDrawMouseRect = true;
            Vector3 mousePosition = Input.mousePosition;
            mMouseEnd = new Vector2(mousePosition.x, mousePosition.y);
        }

        if (Input.GetMouseButtonUp(0)) {
            mBDrawMouseRect = false;
        }
    }

    private void OnRenderObject() {
        if (mBDrawMouseRect)
            Draw(mMouseStart, mMouseEnd);
    }

    void OnGUI() {
    }

    //渲染2D框  
    void Draw(Vector2 start, Vector2 end) {

        GL.PushMatrix();
        m_lineMat.SetPass(0);
        GL.LoadPixelMatrix();

        GL.Begin(GL.QUADS);
        GL.Color(m_lineMat.color);
        GL.Vertex3(start.x, start.y, 0);
        GL.Vertex3(end.x, start.y, 0);
        GL.Vertex3(end.x, end.y, 0);
        GL.Vertex3(start.x, end.y, 0);
        GL.End();
        
        GL.Begin(GL.LINES);
        GL.Color(Color.green);
        GL.Vertex3(start.x, start.y, 0);
        GL.Vertex3(end.x, start.y, 0);
        GL.Vertex3(start.x, end.y, 0);
        GL.Vertex3(end.x, end.y, 0);
        GL.Vertex3(start.x, start.y, 0);
        GL.Vertex3(start.x, end.y, 0);
        GL.Vertex3(end.x, start.y, 0);
        GL.Vertex3(end.x, end.y, 0);
        GL.End();

        GL.PopMatrix();
    }
}
