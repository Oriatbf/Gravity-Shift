using System;
using UnityEngine;

public class PlayerEffection : MonoBehaviour
{
    [SerializeField] private Panel adArrowPanel, upArrowPanel;
    private bool isShow = false;
    [SerializeField] PlayerGravity playerGravity;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isShow = !isShow;
            if (isShow)
            {
                adArrowPanel.SetPosition(PanelStates.Show,true);
                Vector3 rot = Vector3.zero;
                switch (playerGravity)
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
            else
            {
                adArrowPanel.SetPosition(PanelStates.Hide,true);
            }
           
            
        }
    }
}
