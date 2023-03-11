Shader "Binary"
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

            CBUFFER_START(UnityPerMaterial)
            float4 _SourceTex_ST;
            CBUFFER_END

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                // URPではTransformObjectToHClip
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _SourceTex);
                return OUT;
            }

            half4 frag (Varyings IN) : SV_Target
            {
                half4 col = SAMPLE_TEXTURE2D(_SourceTex, sampler_SourceTex, IN.uv);
                half gray = dot(col.rgb, half3(0.299, 0.587, 0.114));
                half binary = step(1 - gray,0.8);
                return half4(binary, binary, binary, 1);
            }
            ENDHLSL
        }
    }
}
