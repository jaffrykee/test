using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCamera : MonoBehaviour {
    // Use this for initialization
    void Start() {

    }

    private float getNormalAngle(float src) {
        float ret = src;
        if (ret > -180.0f) {
            for (; ret >= 180.0f; ret -= 360.0f) {

            }
        } else {
            for (; ret < -180.0f; ret += 360.0f) {

            }
        }
        return ret;
    }
    // Update is called once per frame
    void Update() {
#if UNITY_ANDROID || UNITY_IPHONE || UNITY_IOS
//        var tra = Camera.main.transform;
//        if (Input.GetMouseButton(0)) {
//            switch (m_fixedType) {
//                case FixedType.RPG: {
//                        float mx = Input.GetAxis("Mouse X") * m_rotationSpeed;
//                        float my = Input.GetAxis("Mouse Y") * m_rotationSpeed;
//                        var dpos = tra.rotation * new Vector3(mx, 0, my);
//                        dpos.y = 0;
//                        tra.position += dpos;
//                    }
//                    break;
//                default:
//                    break;
//            }
//        }
#else
        var tra = Camera.main.transform;
        if (Input.GetMouseButton(1))
        {
            switch (m_fixedType)
            {
                case FixedType.RPG:
                    {
                        float mx = Input.GetAxis("Mouse X") * m_rotationSpeed;
                        float my = Input.GetAxis("Mouse Y") * m_rotationSpeed;
                        var lea = tra.localEulerAngles;
                        var ax = getNormalAngle(lea.x - my);

                        if (ax < -90)
                        {
                            ax = -89.9f;
                        }
                        else if (ax > 90)
                        {
                            ax = 89.9f;
                        }
                        tra.localEulerAngles = new Vector3(ax, lea.y + mx, 0);
                    }
                    break;
                case FixedType.SLG:
                    {
                        float mx = Input.GetAxis("Mouse X") * m_rotationSpeed;
                        float my = Input.GetAxis("Mouse Y") * m_rotationSpeed;
                        var lea = tra.localEulerAngles;
                        var ax = getNormalAngle(lea.x - my);

                        if (ax < -90)
                        {
                            ax = -90;
                        }
                        else if (ax > 90)
                        {
                            ax = 90;
                        }
                        tra.localEulerAngles = new Vector3(90, lea.y + mx, ax);
                    }
                    break;
                default:
                    break;
            }
        }
        switch(m_zoomType)
        {
            case ZoomType.Move:
                {
                    tra.position = tra.rotation * new Vector3(0.0F, 0.0F, Input.GetAxis("Mouse ScrollWheel") * m_zoomSpeed) + tra.position;
                }
                break;
            case ZoomType.Telescope:
                {
                    if (Input.GetAxis("Mouse ScrollWheel") < 0)
                    {
                        if (Camera.main.fieldOfView <= 100)
                        {
                            Camera.main.fieldOfView += 2;
                        }
                        if (Camera.main.orthographicSize <= 20)
                        {
                            Camera.main.orthographicSize += 0.5F;
                        }
                    }
                    if (Input.GetAxis("Mouse ScrollWheel") > 0)
                    {
                        if (Camera.main.fieldOfView > 2)
                        {
                            Camera.main.fieldOfView -= 2;
                        }
                        if (Camera.main.orthographicSize >= 1)
                        {
                            Camera.main.orthographicSize -= 0.5F;
                        }
                    }
                }
                break;
            default:
                break;
        }
        float curMove = m_moveSpeed;
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.LeftShift))
        {
            curMove *= m_moveExTimes;
        }
        if (Input.GetKey(KeyCode.W))
        {
            var dpos = tra.rotation * new Vector3(0, 0, curMove);
            dpos.y = 0;
            tra.position += dpos;
        }
        if (Input.GetKey(KeyCode.S))
        {
            var dpos = tra.rotation * new Vector3(0, 0, -curMove);
            dpos.y = 0;
            tra.position += dpos;
        }
        if (Input.GetKey(KeyCode.D))
        {
            var dpos = tra.rotation * new Vector3(curMove, 0, 0);
            dpos.y = 0;
            tra.position += dpos;
        }
        if (Input.GetKey(KeyCode.A))
        {
            var dpos = tra.rotation * new Vector3(-curMove, 0, 0);
            dpos.y = 0;
            tra.position += dpos;
        }
#endif
    }

    public enum FixedType {
        RPG,
        SLG,
    };
    public enum ZoomType {
        Move,
        Telescope,
    }
    public FixedType m_fixedType = FixedType.RPG;
    public ZoomType m_zoomType = ZoomType.Move;
    public float m_rotationSpeed = 3.0f;
    public float m_moveSpeed = 1;
    public float m_moveExTimes = 5;
    public float m_curDistance = 0;
    public float m_zoomSpeed = 1;
}
