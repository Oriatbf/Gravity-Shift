using UnityEngine;

public class TimeController : MonoBehaviour
{
    public void ChangeTimeScale(float _timeScale)
    {
        Time.timeScale = _timeScale;
        Time.fixedDeltaTime = 0.02f * _timeScale;
    }
}
