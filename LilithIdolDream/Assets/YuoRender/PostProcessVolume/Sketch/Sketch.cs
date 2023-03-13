using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace YuoTools.Volume
{
    [System.Serializable, VolumeComponentMenu("YuoPost-processing/Sketch")]
    public class Sketch : VolumeComponent, IPostProcessComponent
    {
        [Tooltip("是否开启")]
        public BoolParameter Open = new BoolParameter(false);

        public FloatParameter BlackPower = new FloatParameter(1);

        public FloatParameter LineWidth = new FloatParameter(1);

        public ColorParameter LineColor = new ColorParameter(Color.black);

        public ColorParameter BGColor = new ColorParameter(Color.white);

        public FloatParameter BackGroundRatio = new FloatParameter(0.5f);

        public bool IsActive() => Open.value;

        public bool IsTileCompatible() => false;
    }
}