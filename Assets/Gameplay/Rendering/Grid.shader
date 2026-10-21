Shader "Custom/Grid"
{
    Properties
    {
        [MainColor] _BaseColor("Base Color", Color) = (1, 1, 1, 1)
        [MainTexture] _BaseMap("Base Map", 2D) = "white" {}
        _Grid("Grid Size", Vector) = (10,5,0,0)
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
                half4 _BaseColor;
                float4 _BaseMap_ST;
                float2 _Grid;
            CBUFFER_END

            Varyings vert(Attributes IN) {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _BaseMap);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target {
                float2 gridPos = IN.uv * _Grid;
                float2 cellOffset = gridPos;
                
                if (_Grid.x % 2 == 0) {
                    cellOffset.x += 0.5;
                }

                if (_Grid.y % 2 == 0) {
                    cellOffset.y += 0.5;
                }

                cellOffset = cellOffset % 1;

                if (cellOffset.x < 0.1 || cellOffset.y < 0.1) {
                    return 1;
                }
                return 0;
            }
            ENDHLSL
        }
    }
}
