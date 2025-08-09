using DG.Tweening;
using UnityEngine;

public static class TimeController
{
    private static Tween tween;
    
    public static void ChangeTimeScale(float _timeScale, float duration = 0)
    {
        tween?.Complete(); 
        if (duration == 0f)
        {
            Time.timeScale = _timeScale;
            Time.fixedDeltaTime = 0.02f * _timeScale;
        }
        else
        {
            tween = DOTween.To(() => Time.timeScale, x =>
            {
                Time.timeScale = x;
                Time.fixedDeltaTime = 0.02f * x;
            }, _timeScale, duration);
        }
  
        Debug.Log("현재 타임스케일 : " + Time.timeScale);
    }
}
