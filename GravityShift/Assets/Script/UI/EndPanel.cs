using System;
using TMPro;
using UnityEngine;

public class EndPanel : Panel
{
    [SerializeField] private GameObject winTxt, loseTxt;
    [SerializeField] private TextMeshProUGUI timeTxt;
    private float timer;
    private bool isGaming = true;
    public void Show(bool isWin)
    {
        isGaming = false;
        SetPosition(PanelStates.Show,true);
        winTxt.SetActive(isWin);
        loseTxt.SetActive(!isWin);
        string t = "";
        if ((int)(timer % 60) < 10)
            t = "0"+(int)(timer % 60);
        else t = ((int)(timer % 60)).ToString();
        
        
        timeTxt.text = "0" + (int)(timer / 60) + ":" +t;
    }

    private void Update()
    {
        if(isGaming) timer += Time.deltaTime;
    }
}
