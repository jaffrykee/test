Shader "Line/normalLine" {
    SubShader { 
        Pass {
            BindChannels {
                Bind "Color", color
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off
            Fog {
                Mode Off 
            }
        }
    }
}