using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
/// https://shibuya24.info/entry/unity-urp-add-pass
/// è„ãLéQçl
/// </summary>
public class MosaicRendererFeature : ScriptableRendererFeature
{
    private MosaicRenderPass _targetPass;

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(_targetPass);
    }

    public override void Create()
    {
        _targetPass = new MosaicRenderPass();
    }


    public class MosaicRenderPass : ScriptableRenderPass
    {
        private readonly int _cameraRenderingTextureId = Shader.PropertyToID("MosaicBase");

        private readonly string _profilerTag = nameof(MosaicRenderPass);

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (renderingData.cameraData.isSceneViewCamera) return;

            var effect = renderingData.cameraData.camera.GetComponent<MosaicEffect>();
            if (effect == null || !effect.EnableEffect) return;

            var cmd = CommandBufferPool.Get(_profilerTag);

            var cameraData = renderingData.cameraData;
            var width = cameraData.camera.scaledPixelWidth / effect.Sampling;
            var height = cameraData.camera.scaledPixelHeight / effect.Sampling;

            cmd.GetTemporaryRT(_cameraRenderingTextureId,
                width, height, 0, FilterMode.Point, RenderTextureFormat.Default);

            var cameraTarget = renderingData.cameraData.renderer.cameraColorTarget;
            cmd.Blit(cameraTarget, _cameraRenderingTextureId);
            cmd.Blit(_cameraRenderingTextureId, cameraTarget);

            cmd.ReleaseTemporaryRT(_cameraRenderingTextureId);

            context.ExecuteCommandBuffer(cmd);
            context.Submit();

            CommandBufferPool.Release(cmd);

        }

        public MosaicRenderPass() => renderPassEvent = RenderPassEvent.AfterRenderingTransparents;


    }
}
