using System;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    [SerializeField]private Panel panel;
    [SerializeField]private ChargingBar chargingBar;

    private bool isShow;

    private void Start()
    {
        chargingBar.Init(()=>Hide());
    }

    private void Show()
    {
        
    }

    private void Hide()
    {
        panel.SetPosition(PanelStates.Hide,true);
    }
}
