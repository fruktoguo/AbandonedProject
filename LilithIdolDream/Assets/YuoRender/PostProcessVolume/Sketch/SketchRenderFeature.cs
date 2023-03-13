using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace YuoTools.Volume
{
    public class SketchRenderFeature : ScriptableRendererFeature
    {
        private SketchPass sketchPass;

        public override void Create()
        {
            sketchPass = new SketchPass(RenderPassEvent.BeforeRenderingPostProcessing);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            sketchPass.Setup(renderer.cameraColorTarget);
            renderer.EnqueuePass(sketchPass);
        }
    }

    public class SketchPass : ScriptableRenderPass
    {
        private static readonly string k_RenderTag = "Render Sketch Effects";
        private static readonly int MainTexId = Shader.PropertyToID("_MainTex");
        private static readonly int TempTargetId = Shader.PropertyToID("_TempTargetSketch");
        private static readonly int BGColorId = Shader.PropertyToID("BGColor");
        private static readonly int LineColorId = Shader.PropertyToID("LineColor");
        private static readonly int LineWidthId = Shader.PropertyToID("LineWidth");
        private static readonly int BlackPowerId = Shader.PropertyToID("BlackPower");
        private static readonly int BackGroundRatioId = Shader.PropertyToID("BackGroundRatio");
        private Sketch sketch;
        private Material sketchMaterial;
        private RenderTargetIdentifier currentTarget;

        public SketchPass(RenderPassEvent evt)
        {
            renderPassEvent = evt;
            //var shader = Shader.Find("PostEffect/ZoomBlur");
            var shader = Shader.Find("Shader Graphs/Sketch");
            if (shader == null)
            {
                //Debug.LogError("Sketch Shader not found.");
                return;
            }
            sketchMaterial = CoreUtils.CreateEngineMaterial(shader);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (sketchMaterial == null)
            {
                Debug.LogError("Sketch Material not created.");
                return;
            }

            if (!renderingData.cameraData.postProcessEnabled) return;

            var stack = VolumeManager.instance.stack;
            sketch = stack.GetComponent<Sketch>();
            if (sketch == null) { return; }
            if (!sketch.IsActive()) { return; }

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
            sketchMaterial.SetColor(BGColorId, sketch.BGColor.value);
            sketchMaterial.SetColor(LineColorId, sketch.LineColor.value);
            sketchMaterial.SetFloat(LineWidthId, sketch.LineWidth.value);
            sketchMaterial.SetFloat(BlackPowerId, sketch.BlackPower.value);
            sketchMaterial.SetFloat(BackGroundRatioId, sketch.BackGroundRatio.value);

            int shaderPass = 0;
            cmd.SetGlobalTexture(MainTexId, source);
            cmd.GetTemporaryRT(destination, w, h, 0, FilterMode.Point, RenderTextureFormat.Default);
            cmd.Blit(source, destination);
            cmd.Blit(destination, source, sketchMaterial, shaderPass);
        }
    }
}