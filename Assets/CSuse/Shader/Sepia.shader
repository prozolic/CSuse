Shader "Sepia"
{
    SubShader
    {
        Tags { "RenderType" = "Opaque" "Renderpipeline" = "UniversalPipeline" }
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
                float2 uv : TEXCOORD0;
                float4 positionHCS : SV_POSITION;
            };

            // swapBufferは_SourceTexで色情報が渡される
            TEXTURE2D(_SourceTex);
            SAMPLER(sampler_SourceTex);
            half _SepiaRate;

            CBUFFER_START(UnityPerMaterial)
            float4 _SourceTex_ST;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                // URPではTransformObjectToHClip
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _SourceTex);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                half4 col = SAMPLE_TEXTURE2D(_SourceTex, sampler_SourceTex, IN.uv);
                // 0.299*R + 0.587*G + 0.114*B
                half gray = dot(col.rgb, half3(0.299, 0.587, 0.114));
                half4 sepia = half4(gray * 0.9, gray * 0.7, gray * 0.4, col.a);
                // セピア率をスクリプトから調整
                sepia.xyz = lerp(col,sepia,_SepiaRate);
                return sepia;
            }
            ENDHLSL
        }
    }
}
