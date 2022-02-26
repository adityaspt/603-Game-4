Shader "Custom/BlockShader"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _MainTex ("Texture", 2D) = "white" {}
        _Dither ("Dither amount", Range(0,3)) = 0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float _Dither;
            float4 _Color;

            float DitherSome(float2 uv) 
            {
                int row = (int)(uv.y * 4);
                int col = (int)(uv.x * 4);

                if (col % 2 == 1)
                    return 1;
                if (row % 2 == 1)
                    return 1;

                return 0.25f;
            }

            float DitherHalf(float2 uv)
            {
                int row = (int)(uv.y * 4);
                int col = (int)(uv.x * 4);
                if ((col + row) % 2 == 0)
                    return 0.25f;
                return 1;

            }

            float DitherWhole(float2 uv)
            {
                int row = (int)(uv.y * 5);
                int col = (int)(uv.x * 5);
                if (row == 0 && col == 0 || row == 4 && col == 0 || row == 0 && col == 4 || row == 4 && col == 4)
                    return 1;
                if ((col + row) % 2 == 0)
                    return 0.25f;
                if (row == 0 || row == 4)
                    return 0.25f;
                if (col == 0 || col == 4)
                    return 0.25f;
                return 1;

            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                if (_Dither > 0 && _Dither <= 1)
                    col *= DitherSome(i.uv);
                if (_Dither > 1 && _Dither <= 2)
                    col *= DitherHalf(i.uv);
                if (_Dither > 2 && _Dither <= 3)
                    col *= (1.25f - DitherSome(i.uv));
                

                return col;
            }
            ENDCG
        }

    }
}
