using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] protected List<KeyCode> useKeycodes;
    [SerializeField] private Panel panel;
    protected bool isActive = false;
     protected TutorialController _tutorialController;
    [SerializeField] protected PlayerState _playerState;
    
    protected const float tutorialDelay = 0.3f;


    public void Inject(TutorialController tutorialController)
    {
        _tutorialController = tutorialController;
    }

    protected virtual void Start()
    {
        _tutorialController.ChanagePlayerState(_playerState);
        DOVirtual.DelayedCall(tutorialDelay, () =>
        {
            isActive = true;
            StartSet();
        } );
    }

    protected void StartSet()
    {
        panel.DOComplete();
        panel.SetPosition(PanelStates.Show,true,0.5f);
        _tutorialController.ChanagePlayerState(_playerState);
    }

    protected virtual void Update()
    {
        if (!isActive) return;
        foreach (var key in useKeycodes)
        {
            if (Input.GetKeyDown(key))
            {
                isActive = false;
                FinishTutorial();
            }
        }
    }

    public void FinishTutorial()
    {
        panel.SetPosition(PanelStates.Hide,true,0.5f);
        _tutorialController.EndTutorial();
        Destroy(gameObject,1f);
    }
}
