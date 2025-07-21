using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using VInspector;

public class VolumeController : Singleton<VolumeController>
{
    [SerializeField] private Volume globalVolume;
    
    private ColorAdjustments colorAdjustments;
    private ChromaticAberration chromaticAberration;

    private void Start()
    {
        if (globalVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments)) { }

        if (globalVolume.profile.TryGet<ChromaticAberration>(out chromaticAberration)) { }
    }
    
    public void SetSeturation(float value)
    {
        DOTween.To(() => colorAdjustments.saturation.value, x => colorAdjustments.saturation.value = x, value, 0.5f)
            .SetUpdate(true);
    }
    
    public void SetChromaticAberration(float value)
    {
        DOTween.To(()=> chromaticAberration.intensity.value,x=> chromaticAberration.intensity.value=x,value,0.5f)
            .SetUpdate(true);
    }
}
