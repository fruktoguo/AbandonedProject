using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace YuoTools.Volume
{
    public class EdgeRenderFeature : ScriptableRendererFeature
    {
        private EdgePass edgePass;

        public override void Create()
        {
            edgePass = new EdgePass(RenderPassEvent.BeforeRenderingPostProcessing);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            edgePass.Setup(renderer.cameraColorTarget);
            renderer.EnqueuePass(edgePass);
        }
    }

    public class EdgePass : ScriptableRenderPass
    {
        private static readonly string k_RenderTag = "Render Edge Effects";
        private static readonly int MainTexId = Shader.PropertyToID("_MainTex");
        private static readonly int TempTargetId = Shader.PropertyToID("_TempTargetEdge");
        private static readonly int EdgeColorId = Shader.PropertyToID("EdgeColor");
        private static readonly int EdgeWidthId = Shader.PropertyToID("EdgeWidth");
        private Edge edge;
        private Material edgeMaterial;
        private RenderTargetIdentifier currentTarget;

        public EdgePass(RenderPassEvent evt)
        {
            renderPassEvent = evt;
            var shader = Shader.Find("Shader Graphs/Edge");
            if (shader == null)
            {
                Debug.LogError("Edge Shader not found.");
                return;
            }
            edgeMaterial = CoreUtils.CreateEngineMaterial(shader);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (edgeMaterial == null)
            {
                Debug.LogError("Edge Material not created.");
                return;
            }

            if (!renderingData.cameraData.postProcessEnabled) return;

            var stack = VolumeManager.instance.stack;
            edge = stack.GetComponent<Edge>();
            if (edge == null) { return; }
            if (!edge.IsActive()) { return; }

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
            edgeMaterial.SetColor(EdgeColorId, edge.EdgeColor.value);
            edgeMaterial.SetFloat(EdgeWidthId, edge.EdgeWidth.value);

            int shaderPass = 0;
            cmd.SetGlobalTexture(MainTexId, source);
            cmd.GetTemporaryRT(destination, w, h, 0, FilterMode.Point, RenderTextureFormat.Default);
            cmd.Blit(source, destination);
            cmd.Blit(destination, source, edgeMaterial, shaderPass);
        }
    }
}