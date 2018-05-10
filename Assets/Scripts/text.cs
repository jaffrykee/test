using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        m_cude = GameObject.Find("cb000");
    }
	
	// Update is called once per frame
	void Update ()
    {
        //var locPos = m_cude.transform.localPosition;
        //transform.localPosition = new Vector3(locPos.x, locPos.y, locPos.z);
        transform.LookAt(Camera.main.transform);
        
    }

    GameObject m_cude;
}
