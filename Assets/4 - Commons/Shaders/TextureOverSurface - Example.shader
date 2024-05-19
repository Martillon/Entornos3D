Shader "Custom/TextureOverSurface - Example"
{
    Properties
    {
        _MainTex("MainTexture", 2D) = "White"{}
        _Color("Color", Color) = (1,1,1,1)
        _OverTex("OverTexture", 2D) = "White"{}
        _Amount("Amount", Range(0,-2)) = 0
    }
    
    SubShader
    {
        
        Tags {"RenderType" = "Opaque"}
        LOD 100
        
        CGProgram

        #pragma surface surf Standard fullforwardshadows
        #include <UnityPBSLighting.cginc>

        sampler2D _MainTex;
        half4 _Color;
        sampler2D _OverTex;
        fixed _Amount;

        struct Input
        {
            half2 uv_MainTex;
            half2 uv_OverTex;
        };

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            half4 MainTex = tex2D(_MainTex, IN.uv_MainTex);
            half OverTex = tex2D(_OverTex, IN.uv_OverTex);

            if(dot(OverTex - 1, MainTex) > _Amount)
            {
                o.Albedo = OverTex.rgb * _Color.rgb;
            }
            else
            {
                o.Albedo = MainTex.rgb;
            }
        }
        
        ENDCG
    }
}
