using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace YuoTools.Volume
{
    public class YuoBloomRenderFeature : ScriptableRendererFeature
    {
        private YuoBloomPass yuoBloom;

        public override void Create()
        {
            yuoBloom = new YuoBloomPass(RenderPassEvent.BeforeRenderingPostProcessing);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            yuoBloom.Setup(renderer.cameraColorTarget);
            renderer.EnqueuePass(yuoBloom);
        }
    }

    public class YuoBloomPass : ScriptableRenderPass
    {
        private static readonly string k_RenderTag = "Render YuoBloom Effects";
        private static readonly int MainTexId = Shader.PropertyToID("_MainTex");
        private static readonly int TempTargetId = Shader.PropertyToID("_TempTargetYuoBloom");
        private static readonly int PowerId = Shader.PropertyToID("Power");
        private static readonly int UseVoronoiId = Shader.PropertyToID("UseVoronoi");
        private static readonly int UseColorId = Shader.PropertyToID("UseColor");
        private YuoBloom yuoBloom;
        private Material yuoBloomMaterial;
        private RenderTargetIdentifier currentTarget;

        public YuoBloomPass(RenderPassEvent evt)
        {
            renderPassEvent = evt;
            var shader = Shader.Find("Shader Graphs/GaussianBlur");
            if (shader == null)
            {
                //Debug.LogError("GaussianBlur Shader not found.");
                return;
            }
            yuoBloomMaterial = CoreUtils.CreateEngineMaterial(shader);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (yuoBloomMaterial == null)
            {
                Debug.LogError("YuoBloom Material not created.");
                return;
            }

            if (!renderingData.cameraData.postProcessEnabled) return;

            var stack = VolumeManager.instance.stack;
            yuoBloom = stack.GetComponent<YuoBloom>();
            if (yuoBloom == null) { return; }
            if (!yuoBloom.IsActive()) { return; }

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
            yuoBloomMaterial.SetFloat(PowerId, yuoBloom.Power.value);
            yuoBloomMaterial.SetInteger(UseVoronoiId, yuoBloom.UseVoronoi.value ? 1 : 0);
            yuoBloomMaterial.SetInteger(UseColorId, yuoBloom.UseColor.value ? 1 : 0);
            int shaderPass = 0;
            for (int i = 0; i < yuoBloom.Iteration.value; i++)
            {
                cmd.SetGlobalTexture(MainTexId, source);
                cmd.GetTemporaryRT(destination, w, h, 0, FilterMode.Point, RenderTextureFormat.Default);
                cmd.Blit(source, destination);
                cmd.Blit(destination, source, yuoBloomMaterial, shaderPass);
            }
        }
    }
}