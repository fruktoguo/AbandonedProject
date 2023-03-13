using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace YuoTools.Volume
{
    [System.Serializable, VolumeComponentMenu("YuoPost-processing/YuoBloom")]
    public class YuoBloom : VolumeComponent, IPostProcessComponent
    {
        [Tooltip("是否开启")]
        public BoolParameter Open = new BoolParameter(false);

        public ClampedFloatParameter Power = new ClampedFloatParameter(0f, 0, 1f);

        public ClampedIntParameter Iteration = new ClampedIntParameter(3, 1, 10);

        public BoolParameter UseVoronoi = new BoolParameter(false);

        public BoolParameter UseColor = new BoolParameter(false);

        public bool IsActive() => Open.value;

        public bool IsTileCompatible() => false;
    }
}