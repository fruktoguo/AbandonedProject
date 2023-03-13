using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace YuoTools.Volume
{
    [System.Serializable, VolumeComponentMenu("YuoPost-processing/GaussianBlur")]
    public class GaussianBlur : VolumeComponent, IPostProcessComponent
    {
        [Tooltip("是否开启")]
        public BoolParameter Open = new BoolParameter(false);

        [Range(0, 0.2f)]
        public ClampedFloatParameter Power = new ClampedFloatParameter(0f, 0, 0.5f);

        public BoolParameter UseVoronoi = new BoolParameter(false);
        public BoolParameter UseColor = new BoolParameter(false);

        public bool IsActive() => Open.value;

        public bool IsTileCompatible() => false;
    }
}