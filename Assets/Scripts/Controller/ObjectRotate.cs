using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    public float roate_Speed = 100.0f;
    void Start()
    {
    }
    void Update()
    {
        Transform target_transform = null;
        if (Input.GetMouseButton(1))
        {
            Ray rayObj = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitObj;
            if (Physics.Raycast(rayObj, out hitObj))
            { 
                target_transform = hitObj.transform;
            }
            if (target_transform != null)
            {
                float mousX = Input.GetAxis("Mouse X") * roate_Speed;
                float mousY = Input.GetAxis("Mouse Y") * roate_Speed;
                target_transform.transform.Rotate(new Vector3(-mousY, -mousX, -mousX));
            }
            else
            {
                Debug.Log("无法取得对象");
            }
        }
    }
}
