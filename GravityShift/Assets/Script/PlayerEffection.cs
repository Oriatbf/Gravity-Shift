using System;
using UnityEngine;

public class PlayerEffection : MonoBehaviour
{
    [SerializeField] private Panel adArrowPanel, upArrowPanel;
    private bool isShow = false;

    public void Show()
    {
        isShow = true;
        adArrowPanel.SetPosition(PanelStates.Show,true);
    }

    public void Hide()
    {
        adArrowPanel.SetPosition(PanelStates.Hide,true);
    }

    public void Hide(PlayerGravity gravity)
    {
        adArrowPanel.SetPosition(PanelStates.Hide,true);
        SetRot(gravity);
    }

    private void SetRot(PlayerGravity gravity)
    {
        Vector3 rot = Vector3.zero;
        switch (gravity)
        {
            case PlayerGravity.Down:
                rot = new Vector3(0, 0, 0);
                break;
            case PlayerGravity.Up:
                rot = new Vector3(0, 0, 180);
                break;
            case PlayerGravity.Left:
                rot = new Vector3(0, 0, 270);
                break;
            case PlayerGravity.Right:
                rot = new Vector3(0, 0, 90);
                break;
        }
        adArrowPanel.transform.rotation = Quaternion.Euler(rot);
    }

 
}
