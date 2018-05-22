using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SdkCallBack : MonoBehaviour {
    // Use this for initialization
    void Start() {

    }
    // Update is called once per frame
    void Update() {

    }
    void showText(string text) {
        var textComp = gameObject.GetComponent<Text>();
        if (textComp != null) {
            textComp.text = text;
        }
    }
}
