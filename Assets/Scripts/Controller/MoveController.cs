using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if(gameObject != null)
        {
            m_curRig = gameObject.GetComponent<Rigidbody>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(m_curRig != null) {
            float curPowerCude = m_initPower;
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.LeftShift)) {
                curPowerCude *= m_exTimes;
            }
            if (Input.GetKey(KeyCode.W)) {
                m_curRig.AddForce(curPowerCude, 0, 0);
            }
            if (Input.GetKey(KeyCode.S)) {
                m_curRig.AddForce(-curPowerCude, 0, 0);
            }
            if (Input.GetKey(KeyCode.D)) {
                m_curRig.AddForce(0, 0, -curPowerCude);
            }
            if (Input.GetKey(KeyCode.A)) {
                m_curRig.AddForce(0, 0, curPowerCude);
            }
            var poi = gameObject.transform.position;
            if(poi.y < -20)
            {
                gameObject.transform.position = new Vector3(poi.x, 20, poi.z);
            }
        }
    }

    GameObject m_curObject;
    Rigidbody m_curRig;
    float m_exTimes = 5;
    float m_initPower = 25;
}
