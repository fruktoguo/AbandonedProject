using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace YuoTools.Volume
{
    [System.Serializable, VolumeComponentMenu("YuoPost-processing/Edge")]
    public class Edge : VolumeComponent, IPostProcessComponent
    {
        [Tooltip("是否开启")]
        public BoolParameter Open = new BoolParameter(false);

        public ClampedFloatParameter EdgeWidth = new ClampedFloatParameter(1, 0, 3);

        public ColorParameter EdgeColor = new ColorParameter(Color.black);

        public bool IsActive() => Open.value;

        public bool IsTileCompatible() => false;
    }
}