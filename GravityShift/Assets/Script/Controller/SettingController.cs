using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class SettingController : MonoBehaviour
{
    [SerializeField] private Panel settingPanel,countPanel;
    [SerializeField] private RectTransform leftPanel,rightPanel;
    [SerializeField] private Button soundSettingBtn;
    [SerializeField] private TextMeshProUGUI countTxt;
    private bool isShow = false;

    private void Start()
    {
        leftPanel.DOAnchorPos(new Vector2(-1000,0),0f).SetUpdate(true);
        rightPanel.DOAnchorPos(new Vector2(1500,0),0f).SetUpdate(true);
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))SetUI();
    }

    private void SetUI()
    {
        if (isShow) Hide();
        else Show();
    }

    private void Show()
    {
        TimeController.ChangeTimeScale(0);
        settingPanel.SetPosition(PanelStates.Show,true);
        leftPanel.DOAnchorPos(new Vector2(50,0),0.5f).SetUpdate(true).SetEase(Ease.OutSine);
        rightPanel.DOAnchorPos(new Vector2(-50,0),0.5f).SetUpdate(true).SetEase(Ease.OutSine);
        isShow = true;
    }

    private void Hide()
    {
        settingPanel.SetPosition(PanelStates.Hide,true);
        leftPanel.DOAnchorPos(new Vector2(-1000,0),0.5f).SetUpdate(true);
        rightPanel.DOAnchorPos(new Vector2(1500,0),0.5f).SetUpdate(true);
        countPanel.SetPosition(PanelStates.Show,true);
        
        Sequence countSeq = DOTween.Sequence();
        countSeq.SetUpdate(true);
        countSeq.AppendCallback(() => countTxt.text = "3").AppendInterval(1f);
        countSeq.AppendCallback(() => countTxt.text = "2").AppendInterval(1f);
        countSeq.AppendCallback(() => countTxt.text = "1").AppendInterval(1f);
        countSeq.AppendCallback(() =>
        {
            countPanel.SetPosition(PanelStates.Hide,true,0.1f);
            TimeController.ChangeTimeScale(1);
            isShow = false;
        });
        
        countSeq.Play();
    }
}
