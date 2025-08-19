using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : Singleton<ScoreController>
{
    public TextMeshProUGUI stageTxt,timeTxt;
    public List<Image> coinImage;
    public TextMeshProUGUI clearTxt;
    public Transform parent;
    public Panel panel;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Init(StageData stageData,int stageIndex)
    {
        clearTxt.enabled = stageData.isClear;
        stageTxt.text = "스테이지 " + (stageIndex+1).ToString();
        float timer = stageData.finalTime;
        string t = "";
        if ((int)(timer % 60) < 10)
            t = "0"+(int)(timer % 60);
        else t = ((int)(timer % 60)).ToString();
        timeTxt.text = "소요시간 "+"0" + (int)(timer / 60) + ":" +t;
        for (int i = 0; i <3; i++)
        {
            coinImage[i].gameObject.SetActive(false);
        }
        for (int i = 0; i <stageData.coinCount; i++)
        {
            coinImage[i].gameObject.SetActive(true);
        }
    }

    public void SetPos(Transform trans,int stageIndex)
    {
        panel.SetPosition(PanelStates.Show,true);
        Vector3 screenPos = _camera.WorldToScreenPoint(trans.position);
        screenPos += new Vector3(0,200,0);
        parent.position = screenPos;
        Init(DataManager.Inst.Data.StageDatas[stageIndex], stageIndex);
    }
}
