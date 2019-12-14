using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDebug : MonoBehaviour {
    public Shader m_lineShader;
    public Texture2D m_lineTexture;
    public Material m_lineMat = null;

    public Vector2 m_curCenter = new Vector2(0, 0);
    public Vector2 m_worldSize = new Vector2(10000, 10000);
    public Vector2? m_lastMousePoi = null;

    void Start() {
        Color clr = Color.red;
        clr.a = 0.5f;
        if (m_lineMat == null) {
            m_lineMat = new Material(m_lineShader);
        }
        m_lineMat.color = clr;
    }

    void Update() {
        //按下鼠标左键  
        if (Input.GetMouseButtonDown(0)) {
            if (m_lastMousePoi == null) {
                m_lastMousePoi = Input.mousePosition;
            }
        }

        //持续按下鼠标左键  
        if (Input.GetMouseButton(0)) {
            if (m_lastMousePoi != null) {
                float nx = m_curCenter.x + ((Vector2)m_lastMousePoi).x - Input.mousePosition.x;
                float ny = m_curCenter.y + ((Vector2)m_lastMousePoi).y - Input.mousePosition.y;
                if (nx < 0) {
                    nx = 0;
                } else if (nx > m_worldSize.x) {
                    nx = m_worldSize.x;
                }
                if (ny < 0) {
                    ny = 0;
                } else if (ny > m_worldSize.y) {
                    ny = m_worldSize.y;
                }
                m_curCenter = new Vector2(nx, ny);
            }
            m_lastMousePoi = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0)) {
            m_lastMousePoi = null;
        }
    }

    private void OnRenderObject() {
        GL.PushMatrix();
        m_lineMat.SetPass(0);
        GL.LoadPixelMatrix();

        drawBack();
        drawGridLine();
        GL.PopMatrix();
    }

    void OnGUI() {
    }

    private void drawRect(Vector2 start, Vector2 end, Color color) {
        GL.Begin(GL.QUADS);
        GL.Color(color);
        GL.Vertex3(start.x, start.y, 0);
        GL.Vertex3(end.x, start.y, 0);
        GL.Vertex3(end.x, end.y, 0);
        GL.Vertex3(start.x, end.y, 0);
        GL.End();
    }

    private void drawBack() {
        Color backColor = new Color(0.8f, 0.8f, 0.2f, 1.0f);
        drawRect(new Vector2(0, 0), new Vector2(Screen.width, Screen.height), backColor);
    }

    private void drawGridLine() {
        GL.Begin(GL.LINES);
        GL.Color(new Color(1f, 1f, 1f, 1f));
        for(int i = Screen.width % 10; i <= Screen.width; i += 10) {
            GL.Vertex3(i, 0, 0);
            GL.Vertex3(i, Screen.height, 0);
        }
        for (int j = Screen.height % 10; j <= Screen.height; j += 10) {
            GL.Vertex3(0, j, 0);
            GL.Vertex3(Screen.width, j, 0);
        }

        GL.End();
    }

    //渲染2D框  
    void Draw(Vector2 start, Vector2 end) {
        GL.PushMatrix();
        m_lineMat.SetPass(0);
        GL.LoadPixelMatrix();

        GL.Begin(GL.QUADS);
        GL.Color(new Color(0.8f, 0.8f, 0.2f, 0.5f));
        GL.Vertex3(start.x, start.y, 0);
        GL.Vertex3(end.x, start.y, 0);
        GL.Vertex3(end.x, end.y, 0);
        GL.Vertex3(start.x, end.y, 0);
        GL.End();

        GL.Begin(GL.LINES);
        GL.Color(new Color(0.2f, 0.8f, 0.8f, 0.5f));
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
