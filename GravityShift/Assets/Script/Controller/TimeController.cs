using DG.Tweening;
using UnityEngine;

public static class TimeController
{
    public static void ChangeTimeScale(float _timeScale,float duration = 0)
    {
        if (duration == 0)
        {
            Time.timeScale = _timeScale;
        }
        else
        {
            DOTween.To(()=>Time.timeScale, x => Time.timeScale = x, _timeScale, duration);
        }
        Time.fixedDeltaTime = 0.02f * _timeScale;
        Debug.Log("현재 타일스케일 : "+Time.timeScale);
    }
}
