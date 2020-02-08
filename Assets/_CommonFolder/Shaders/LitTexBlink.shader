Shader "Custom/LitTexBlink"
{
    Properties
    {
        _Albedo ("Albedo (RGB), Alpha (A)", 2D) = "white" {}
         _BlinkSpeed("Blink", Range(1,10)) = 1
        _MainColor("MainColor", Color) = (0,0,0,0)
    }
 
    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType"="Transparent" }

        Pass{
  		  ZWrite ON
  		  ColorMask 0
		}

        Offset -1,-1 
        
        CGPROGRAM
        #pragma target 3.0
        #include "UnityPBSLighting.cginc"
        #include "UnityCG.cginc"
        #pragma surface surf Standard fullforwardshadows alpha:fade
        #pragma exclude_renderers gles
 
        struct Input
        {
            float2 uv_Albedo;
        };
 
        sampler2D _Albedo;
        float4 _MainColor;
        float _BlinkSpeed;
 
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
           fixed4 albedo = tex2D(_Albedo, IN.uv_Albedo)* _MainColor;
     
            o.Albedo = albedo.rgb;
            o.Alpha = sin(_Time.w * _BlinkSpeed)*0.5 +1;
        }
        ENDCG
    }
 
    FallBack "Diffuse"
}