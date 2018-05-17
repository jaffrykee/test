using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SdkTest : MonoBehaviour {

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        var test = SDK.SdkManager.instance();
        if (test != null) {
            Debug.Log(test);
        } else {
            Debug.Log("test is null.");
        }
    }
}
