using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] protected List<KeyCode> useKeycodes;
    [SerializeField] private Panel panel;
    protected bool isActive = false;
     protected TutorialController _tutorialController;
    [SerializeField] private PlayerState _playerState;


    public void Inject(TutorialController tutorialController)
    {
        _tutorialController = tutorialController;
    }

    protected virtual void Start()
    {
        isActive = true;
        StartSet();
    }

    protected void StartSet()
    {
        panel.SetPosition(PanelStates.Show,true,0.8f);
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

    protected void FinishTutorial()
    {
        panel.SetPosition(PanelStates.Hide,true,0.8f);
        _tutorialController.EndTutorial();
        Destroy(gameObject,1f);
    }
}
