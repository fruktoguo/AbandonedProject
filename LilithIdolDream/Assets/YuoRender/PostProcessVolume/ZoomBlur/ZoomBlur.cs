using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ZoomBlur : VolumeComponent, IPostProcessComponent
{
    [Range(0f, 100f), Tooltip("模糊强度")]
    public ClampedFloatParameter focusPower = new ClampedFloatParameter(0f, 0, 0.5f);

    [Range(0, 10), Tooltip("模糊层数")]
    public ClampedIntParameter focusDetail = new ClampedIntParameter(5, 0, 10);

    [Tooltip("模糊中心")]
    public Vector2Parameter focusScreenPosition = new Vector2Parameter(Vector2.zero);

    [Tooltip("默认水平分辨率")]
    public IntParameter referenceResolutionX = new IntParameter(10
        );

    public bool IsActive() => focusPower.value > 0f;

    public bool IsTileCompatible() => false;
}