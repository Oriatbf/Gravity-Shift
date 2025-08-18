using System;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    [SerializeField]private Panel panel,enterPanel;
    [SerializeField]private ChargingBar chargingBar,enterBar;
    [SerializeField] StageSelectController stageSelectController;

    private bool isShow;

    private void Start()
    {
        if (!DataManager.Inst.isOpening)
        {
            panel.SetPosition(PanelStates.Hide);
            enterPanel.SetPosition(PanelStates.Show);
        }
        chargingBar.Init(()=>Hide());
        enterBar.Init(()=>stageSelectController.SetStage());
        
    }
    

    private void Hide()
    {
        DataManager.Inst.isOpening = false;
        panel.SetPosition(PanelStates.Hide,true);
        enterPanel.SetPosition(PanelStates.Show,true);
    }
}
