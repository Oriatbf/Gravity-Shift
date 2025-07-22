using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VInspector;

public class SettingController : MonoBehaviour
{
    

    [Foldout("Rect,Panel")]
    [SerializeField] private Panel pausePanel;
    [SerializeField] private Panel countPanel;

    [SerializeField] private RectTransform leftPanel,rightPanel,settingPanel;
    [Foldout("UI")]
    [SerializeField] private Button settingBtn,resumeBtn,menuBtn;
    [SerializeField] private TextMeshProUGUI countTxt;
    private bool isShow = false,isCounting = false;
    
    private const float leftPanelShow = 50, rightPanelShow = -50,leftPanelHide = -1000,rightPanelHide = 1500,settingPanelHide = 2000;

    private void Start()
    {
        BtnSetting();
        HidePanels();
    }

    private void BtnSetting()
    {
        resumeBtn.onClick.AddListener(()=>Hide());
        settingBtn.onClick.AddListener(()=>ShowSettingPanel());
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isCounting)SetUI();
    }

    private void SetUI()
    {
        if (isShow) Hide();
        else Show();
    }

    private void ShowSettingPanel()
    {
        settingPanel.DOAnchorPos(Vector2.zero, 0.5f).SetUpdate(true);
    }

    private void Show()
    {
        TimeController.ChangeTimeScale(0);
        pausePanel.SetPosition(PanelStates.Show,true);
        
        leftPanel.DOAnchorPos(new Vector2(leftPanelShow,0),0.5f).SetUpdate(true).SetEase(Ease.OutSine);
        rightPanel.DOAnchorPos(new Vector2(rightPanelShow,0),0.5f).SetUpdate(true).SetEase(Ease.OutSine);
        
        isShow = true;
    }

    private void HidePanels()
    {
        leftPanel.DOAnchorPos(new Vector2(leftPanelHide,0),0.5f).SetUpdate(true);
        rightPanel.DOAnchorPos(new Vector2(rightPanelHide,0),0.5f).SetUpdate(true);
        settingPanel.DOAnchorPos(new Vector2(settingPanelHide,0),0.5f).SetUpdate(true);
    }

    private void Hide()
    {
        pausePanel.SetPosition(PanelStates.Hide,true);
        countPanel.SetPosition(PanelStates.Show,true);
        
        HidePanels();
        
        Sequence countSeq = DOTween.Sequence();
        isCounting = true;
        countSeq.SetUpdate(true);
        countSeq.AppendCallback(() => countTxt.text = "3").AppendInterval(1f);
        countSeq.AppendCallback(() => countTxt.text = "2").AppendInterval(1f);
        countSeq.AppendCallback(() => countTxt.text = "1").AppendInterval(1f);
        countSeq.AppendCallback(() =>
        {
            countPanel.SetPosition(PanelStates.Hide,true,0.1f);
            TimeController.ChangeTimeScale(1);
            isShow = false;
            isCounting = false;
        });
        
        countSeq.Play();
    }
}
