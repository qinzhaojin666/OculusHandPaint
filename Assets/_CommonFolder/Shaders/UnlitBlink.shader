Shader "Custom/UnlitBlink"
{
    Properties
    {
        _BlinkSpeed("Blink", Range(1,10)) = 1
        _MainColor("MainColor", Color) = (0,0,0,0)
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType"="Transparent" }
        LOD 100

        ZWrite Off
        Offset -1,-1 

        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 _MainColor;
            float _BlinkSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 col = _MainColor;
                col.a = sin(_Time.w * _BlinkSpeed)*0.5 +1;
                return col;
            }
            ENDCG
        }
    }
}
