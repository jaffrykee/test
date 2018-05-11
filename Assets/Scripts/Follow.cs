using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Follow : MonoBehaviour {
    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        m_sp = GameObject.Find(m_id);
        if (m_sp != null) {
            transform.LookAt(m_sp.transform);
        }
    }
    public string m_id;
    public Vector3 m_dpos;
    private GameObject m_sp;
}
