Shader "Unlit/WindTreeNoise"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint Color", Color) = (1,1,1,1)
        _Alpha ("Opacity", Range(0,1)) = 1
        _Speed("Wind Speed", Float)=0.5
        _WindStrength ("WindStrength", Float) = 0.05
        _Frequency ("Frequency Wind", Float) = 5.0
        _MinYEffect ("MinY Effect", Float) = 0.5
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

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
                float4 color : COLOR; // Supports SpriteRenderer color
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _WindStrength;
            float _Speed;
            float _Frequency;
            float _MinYEffect;

            // Gradient noise helpers
            float2 unity_gradientNoise_dir(float2 p)
            {
                p = fmod(p, 289);
                float x = fmod((34 * p.x + 1) * p.x, 289) + p.y;
                x = fmod((34 * x + 1) * x, 289);
                x = frac(x / 41) * 2 - 1;
                return normalize(float2(x - floor(x + 0.5), abs(x) - 0.5));
            }

            float unity_gradientNoise(float2 p)
            {
                float2 ip = floor(p);
                float2 fp = frac(p);
                float d00 = dot(unity_gradientNoise_dir(ip), fp);
                float d01 = dot(unity_gradientNoise_dir(ip + float2(0, 1)), fp - float2(0, 1));
                float d10 = dot(unity_gradientNoise_dir(ip + float2(1, 0)), fp - float2(1, 0));
                float d11 = dot(unity_gradientNoise_dir(ip + float2(1, 1)), fp - float2(1, 1));
                fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
                return lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x);
            }

            void Unity_GradientNoise_float(float2 UV, float Scale, out float Out)
            {
                Out = unity_gradientNoise(UV * Scale);
            }

            v2f vert (appdata v)
            {
                v2f o;

                // time factor for animation
                float t = _Time.y * _Speed;

                // apply stronger wind effect higher up the mesh
                float heightFactor = saturate(v.vertex.y) + 0.1;

                if (v.vertex.y > _MinYEffect)
                {
                    float noise;
                    Unity_GradientNoise_float(v.uv.xy, 1, noise);

                    // Use vertex.x + noise as phase input instead of xy to avoid stretching
                    float windX = sin(v.vertex.x * _Frequency + t + noise * 2.0);

                    // smooth the effect slightly to avoid harsh lines
                    windX *= smoothstep(_MinYEffect, 1.0, v.vertex.y);

                    // apply displacement
                    v.vertex.x += windX * heightFactor * _WindStrength;
                }

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * i.color;
                return col;
            }
            ENDCG
        }
    }


}