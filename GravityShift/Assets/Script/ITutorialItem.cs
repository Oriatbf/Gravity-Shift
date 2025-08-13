using UnityEngine;

public class TutorialItem: MonoBehaviour
{
    Tutorial tutorial;
    public void Init(Tutorial tutorial) => this.tutorial = tutorial;

    public void EndTutorial()
    {
        tutorial.FinishTutorial();
    }
}
