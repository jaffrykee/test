using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SdkTest : MonoBehaviour {
    
    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        return;
        var test = SDK.SdkManager.instance();
        if (test != null && test.m_tkapi != null) {
            var sdkTest = test.m_tkapi.CallStatic<AndroidJavaObject>("instance", "mx_log");
            if (sdkTest != null) {
                sdkTest.Call("SayHello");
                var textObj = GameObject.Find("mx_log");
                if (textObj != null) {
                    var textComp = textObj.GetComponent<UnityEngine.UI.Text>();
                    if (textComp != null) {
                        textComp.text += sdkTest.Call<int>("CalculateAdd", 22, 33).ToString();
                    }
                }
            }
        } else {
            Debug.LogWarning("test is null.");
        }
    }
    public void loginWeChat() {
        var sdk = SDK.SdkManager.instance();
        if (sdk != null && sdk.m_tkapi != null) {
            var sdkTest = sdk.m_tkapi.CallStatic<AndroidJavaObject>("instance", "mx_log");
            if (sdkTest != null) {
                sdkTest.Call("loginWeChat");
            }
        } else {
            Debug.LogWarning("test is null.");
        }
    }
}
