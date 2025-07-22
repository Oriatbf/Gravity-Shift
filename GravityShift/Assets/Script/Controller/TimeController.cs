using UnityEngine;

public static class TimeController
{
    public static void ChangeTimeScale(float _timeScale)
    {
        Time.timeScale = _timeScale;
        Time.fixedDeltaTime = 0.02f * _timeScale;
        Debug.Log("현재 타일스케일 : "+Time.timeScale);
    }
}
