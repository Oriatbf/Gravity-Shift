using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private List<KeyCode> useKeycodes;
    [SerializeField] private Panel panel;
    bool isActive = false;

    private void Awake()
    {
        isActive = true;
        panel.SetPosition(PanelStates.Show,true);
    }

    private void Update()
    {
        if (!isActive) return;
        foreach (var key in useKeycodes)
        {
            if (Input.GetKeyDown(key))
            {
                isActive = false;
                panel.SetPosition(PanelStates.Hide,true);
                Destroy(gameObject,1f);
            }
        }
    }
}
