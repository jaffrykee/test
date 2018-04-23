using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

	// Use this for initialization
	void Start () {
        m_sp = GameObject.Find("sp000");
        if(m_sp != null)
        {
            m_dpos = transform.position - m_sp.transform.position;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(m_sp)
        {
            transform.position = m_sp.transform.position + m_dpos;
        }
	}
    GameObject m_sp;
    Vector3 m_dpos;
}
