using System;
using UnityEngine;

public class TutorialItem: MonoBehaviour
{
    protected Tutorial tutorial;
    public void Init(Tutorial tutorial) => this.tutorial = tutorial;

    private void Update()
    {
        if (transform.position.z < -15)
        {
            SettingController.Inst.EndingUI(false);
        }
    }

    public void EndTutorial()
    {
        tutorial.FinishTutorial();
    }
}
