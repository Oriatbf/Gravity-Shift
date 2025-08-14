using DG.Tweening;
using UnityEngine;

public class TutorialEnd : Tutorial
{
    public int sceneIndex;
    public float sceneDelay;
    protected override void Start()
    {
        isActive = false;
        _tutorialController.ChanagePlayerState(PlayerState.NoneKey);
        DOVirtual.DelayedCall(tutorialDelay, () =>
        {
            _tutorialController.ChanagePlayerState(_playerState);
            StartSet();
            NextScene();
        } );
    }

    private void NextScene()
    {
        DOVirtual.DelayedCall(sceneDelay, () => FadeInFadeOutManager.Inst.FadeOut(sceneIndex, true));
    }
}
