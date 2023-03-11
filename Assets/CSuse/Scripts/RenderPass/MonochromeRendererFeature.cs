using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MonochromeRendererFeature : ScriptableRendererFeature
{
    private MonochromeRendererPass _rendererPass;

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(_rendererPass);
    }

    public override void Create()
    {
        _rendererPass = new MonochromeRendererPass();
    }

    private class MonochromeRendererPass : ScriptableRenderPass
    {
        private static readonly string _passTag = nameof(MonochromeRendererPass);

        public MonochromeRendererPass() => renderPassEvent = RenderPassEvent.AfterRendering;

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (renderingData.cameraData.isSceneViewCamera) return;

            var effect = renderingData.cameraData.camera.GetComponent<MonochromeEffect>();
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
