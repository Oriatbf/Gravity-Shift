using System;
using UnityEngine;

public class PlayerEffection : MonoBehaviour
{
    [SerializeField] private Panel adArrowPanel, upArrowPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            adArrowPanel.SetPosition(PanelStates.Show,true);
        }
    }
}
