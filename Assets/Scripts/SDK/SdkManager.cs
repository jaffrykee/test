using UnityEngine;
using System.Collections;

namespace SDK {
    public class SdkManager {
        private static SdkManager s_instance;
        public static SdkManager instance() {
            if (s_instance == null) {
                s_instance = new SdkManager();
            }
            return s_instance;
        }

        public AndroidJavaClass m_tkapi;
        private SdkManager() {
#if UNITY_ANDROID
            m_tkapi = new AndroidJavaClass("cn.jj.jjgamesdk.TKAPI");
            //m_tkapi.CallStatic("getInstance", )
#endif
        }
        override public string ToString() {
            if (m_tkapi != null) {
                return m_tkapi.ToString();
            } else {
                return "<null>";
            }
        }
    }
}
