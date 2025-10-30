Shader "Unlit/WindTreeWobble"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Strength ("Wind Strength", Float) = 0.05
        _Wobble ("Wobble Speed", Float) = 5.0
        _MinYEffect ("MinY Effect", Float) = 0.5

    }
    SubShader
    {
        Tags { "RenderType"="Transparent"}
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Strength;
            float _Wobble;
            float _MinYEffect;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata_t v)
            {
                v2f o;

                if (v.vertex.y >= _MinYEffect)
                {
                    float wave = sin(_Time.y * _Wobble + v.vertex.y * 10.0) * _Strength;
                    v.vertex.x += wave;
                }

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
