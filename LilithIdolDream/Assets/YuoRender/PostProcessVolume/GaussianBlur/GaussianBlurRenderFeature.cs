using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace YuoTools.Volume
{
    public class GaussianBlurRenderFeature : ScriptableRendererFeature
    {
        private GaussianBlurPass gaussianBlur;

        public override void Create()
        {
            gaussianBlur = new GaussianBlurPass(RenderPassEvent.BeforeRenderingPostProcessing);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            gaussianBlur.Setup(renderer.cameraColorTarget);
            renderer.EnqueuePass(gaussianBlur);
        }
    }

    public class GaussianBlurPass : ScriptableRenderPass
    {
        private static readonly string k_RenderTag = "Render GaussianBlur Effects";
        private static readonly int MainTexId = Shader.PropertyToID("_MainTex");
        private static readonly int TempTargetId = Shader.PropertyToID("_TempTargetGaussianBlur");
        private static readonly int PowerId = Shader.PropertyToID("Power");
        private static readonly int UseVoronoiId = Shader.PropertyToID("UseVoronoi");
        private static readonly int UseColorId = Shader.PropertyToID("UseColor");
        private GaussianBlur gaussianBlur;
        private Material gaussianBlurMaterial;
        private RenderTargetIdentifier currentTarget;

        public GaussianBlurPass(RenderPassEvent evt)
        {
            renderPassEvent = evt;
            var shader = Shader.Find("Shader Graphs/GaussianBlur");
            if (shader == null)
            {
                //Debug.LogError("GaussianBlur Shader not found.");
                return;
            }
            gaussianBlurMaterial = CoreUtils.CreateEngineMaterial(shader);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (gaussianBlurMaterial == null)
            {
                Debug.LogError("GaussianBlur Material not created.");
                return;
            }

            if (!renderingData.cameraData.postProcessEnabled) return;

            var stack = VolumeManager.instance.stack;
            gaussianBlur = stack.GetComponent<GaussianBlur>();
            if (gaussianBlur == null) { return; }
            if (!gaussianBlur.IsActive()) { return; }

            var cmd = CommandBufferPool.Get(k_RenderTag);
            Render(cmd, ref renderingData);
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public void Setup(in RenderTargetIdentifier currentTarget)
        {
            this.currentTarget = currentTarget;
        }

        private void Render(CommandBuffer cmd, ref RenderingData renderingData)
        {
            ref var cameraData = ref renderingData.cameraData;
            var source = currentTarget;
            int destination = TempTargetId;

            var w = cameraData.camera.scaledPixelWidth;
            var h = cameraData.camera.scaledPixelHeight;
            gaussianBlurMaterial.SetFloat(PowerId, gaussianBlur.Power.value);
            gaussianBlurMaterial.SetInteger(UseVoronoiId, gaussianBlur.UseVoronoi.value ? 1 : 0);
            gaussianBlurMaterial.SetInteger(UseColorId, gaussianBlur.UseColor.value ? 1 : 0);
            int shaderPass = 0;
            cmd.SetGlobalTexture(MainTexId, source);
            cmd.GetTemporaryRT(destination, w, h, 0, FilterMode.Point, RenderTextureFormat.Default);
            cmd.Blit(source, destination);
            cmd.Blit(destination, source, gaussianBlurMaterial, shaderPass);
        }
    }
}