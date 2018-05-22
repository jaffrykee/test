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
        if (test != null && test.m_tkapi != null) {
            Debug.Log(test);
            var sdkTest = test.m_tkapi.CallStatic<AndroidJavaObject>("instance", "mx_test");
            if (sdkTest != null) {
                sdkTest.Call("SayHello");
                var textObj = GameObject.Find("mx_test");
                if (textObj != null) {
                    var textComp = textObj.GetComponent<UnityEngine.UI.Text>();
                    if (textComp != null) {
                        textComp.text += sdkTest.Call<int>("CalculateAdd", 22, 33).ToString();
                    }
                }
            }
        } else {
            Debug.Log("test is null.");
        }
    }
}
