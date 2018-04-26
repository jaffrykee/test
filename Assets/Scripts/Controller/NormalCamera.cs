using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCamera : MonoBehaviour
{
    // Use this for initialization
    void Start () {
		
	}
	
    private float getNormalAngle(float src)
    {
        float ret = src;
        if(ret > -180.0f)
        {
            for(; ret >= 180.0f; ret -= 360.0f)
            {

            }
        }
        else
        {
            for (; ret < -180.0f; ret += 360.0f)
            {

            }
        }
        return ret;
    }
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButton(1))
        {
            float mx = Input.GetAxis("Mouse X") * m_speed;
            float my = Input.GetAxis("Mouse Y") * m_speed;
            //var oldRo = Camera.main.transform.rotation;
            //var dRo = Quaternion.Euler(-my, mx, 0);

            //Camera.main.transform.RotateAround(root.transform.position, root.transform.up, mx);
            //Camera.main.transform.RotateAround(root.transform.position, root.transform.right, -my);
            //Camera.main.transform.LookAt()
            var lea = Camera.main.transform.localEulerAngles;
            var ax = getNormalAngle(lea.x - my);
            
            if (ax < -90)
            {
                ax = -90;
            }
            else if (ax > 90)
            {
                ax = 90;
            }
            Camera.main.transform.localEulerAngles = new Vector3(ax, lea.y + mx, 0);
            //tra.rotation.z = 0;

            //Camera.main.transform.RotateAround(root.transform.position, root.transform., -my);
            //var newRo = new Quaternion(oldRo.x + dRo.x, oldRo.y + dRo.y, oldRo.z + dRo.z, oldRo.w + dRo.w);
            //var newRo = oldRo * dRo;

            //Camera.main.transform.rotation = newRo;
            //var nr = Camera.main.transform.rotation;
            //nr.y -= mx;
            //nr.w += my;
            //Camera.main.transform.rotation = nr;
        }
    }

    float m_speed = 3.0f;
}
