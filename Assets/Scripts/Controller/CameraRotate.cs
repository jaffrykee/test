using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float roate_Speed = 1;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButton(1))
        {
            float mx = Input.GetAxis("Mouse X") * roate_Speed;
            float my = Input.GetAxis("Mouse Y") * roate_Speed;
            //var oldRo = Camera.main.transform.rotation;
            //var dRo = Quaternion.Euler(-my, mx, 0);
            var root = GameObject.Find("GameRoot");
            Camera.main.transform.RotateAround(root.transform.position, Vector3.up, mx);
            Camera.main.transform.RotateAround(root.transform.position, Vector3.left, -my);
            //var newRo = new Quaternion(oldRo.x + dRo.x, oldRo.y + dRo.y, oldRo.z + dRo.z, oldRo.w + dRo.w);
            //var newRo = oldRo * dRo;

            //Camera.main.transform.rotation = newRo;
            //var nr = Camera.main.transform.rotation;
            //nr.y -= mx;
            //nr.w += my;
            //Camera.main.transform.rotation = nr;
        }
    }
}
