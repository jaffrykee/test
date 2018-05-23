using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDK;

public class SdkTest : MonoBehaviour {
    
    // Use this for initialization
    void Start() {
        m_sdk = SdkManager.instance();
        if (m_sdk != null && m_sdk.m_tkapi != null) {
            m_sdkJava = m_sdk.m_tkapi.CallStatic<AndroidJavaObject>("instance", "mx_log");
        }
    }

    // Update is called once per frame
    void Update() {
        return;
    }
    public void testFunc() {
        if (m_sdkJava != null) {
            m_sdkJava.Call("SayHello");
            var textObj = GameObject.Find("mx_log");
            if (textObj != null) {
                var textComp = textObj.GetComponent<UnityEngine.UI.Text>();
                if (textComp != null) {
                    textComp.text += m_sdkJava.Call<int>("CalculateAdd", 22, 33).ToString();
                }
            }
        } else {
            Debug.LogWarning("sdk is null.");
        }
    }
    public void loginQuick() {
        if (m_sdkJava != null) {
            m_sdkJava.Call<int>("loginQuick");
        } else {
            Debug.LogWarning("sdk is null.");
        }
    }
    public void logout() {
        if (m_sdkJava != null) {
            m_sdkJava.Call<int>("logout");
        } else {
            Debug.LogWarning("sdk is null.");
        }
    }
    public void register() {
        if (m_sdkJava != null) {
            m_sdkJava.Call<int>("register", "admin", "123456", "123456", "nick");
        } else {
            Debug.LogWarning("sdk is null.");
        }
    }
    public void password() {
        if (m_sdkJava != null) {
            m_sdkJava.Call<int>("password");
        } else {
            Debug.LogWarning("sdk is null.");
        }
    }
    public void wechat() {
        if (m_sdkJava != null) {
            m_sdkJava.Call<int>("loginWeChat");
        } else {
            Debug.LogWarning("sdk is null.");
        }
    }
    SdkManager m_sdk;
    AndroidJavaObject m_sdkJava;
}
