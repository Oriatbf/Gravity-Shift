using System;
using UnityEngine;

public class PlayerEffection : MonoBehaviour
{
    [SerializeField] private Panel adArrowPanel, upArrowPanel;
    private bool isShow = false;
    PlayerCtrl player;

    private void Awake()
    {
        player = GetComponent<PlayerCtrl>();
    }

    public void Show(bool isItem)
    {
        isShow = true;
        SetRot(player.playerGravity);
        adArrowPanel.SetPosition(PanelStates.Show,true);
        var upArrowState = isItem?PanelStates.Show:PanelStates.Hide;
        upArrowPanel.SetPosition( upArrowState,true);
    }

    public void Hide()
    {
        adArrowPanel.SetPosition(PanelStates.Hide,true);
    }

    public void Hide(PlayerGravity gravity)
    {
        adArrowPanel.SetPosition(PanelStates.Hide,true);
        upArrowPanel.SetPosition(PanelStates.Hide,true);
        SetRot(player.playerGravity);
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
        upArrowPanel.transform.rotation = Quaternion.Euler(rot);
    }

 
}
