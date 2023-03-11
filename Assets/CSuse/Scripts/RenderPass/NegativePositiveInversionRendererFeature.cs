using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class NegativePositiveInversionRendererFeature : ScriptableRendererFeature
{
    private NegativePositiveInversionRendererPass _rendererPass;

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(_rendererPass);
    }

    public override void Create()
    {
        _rendererPass = new NegativePositiveInversionRendererPass();
    }

    private class NegativePositiveInversionRendererPass : ScriptableRenderPass
    {
        private static readonly string _passTag = nameof(NegativePositiveInversionRendererPass);

        public NegativePositiveInversionRendererPass() => renderPassEvent = RenderPassEvent.AfterRendering;

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (renderingData.cameraData.isSceneViewCamera) return;

            var effect = renderingData.cameraData.camera.GetComponent<NegativePositiveInversionEffect>();
            if (effect == null || !effect.EnableEffect) return;

            var material = effect.Material;
            var cmd = CommandBufferPool.Get(_passTag);
            Blit(cmd, ref renderingData, material);

            context.ExecuteCommandBuffer(cmd);
            context.Submit();

            CommandBufferPool.Release(cmd);
        }
    }
}
