using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VInspector;

public class SettingController : Singleton<SettingController>
{
    

    [Foldout("Rect,Panel")]
    [SerializeField] private Panel pausePanel;
    [SerializeField] private Panel countPanel;
    [SerializeField] private EndPanel endPanel;

    [SerializeField] private RectTransform leftPanel,rightPanel,settingPanel;
    [Foldout("UI")]
    [SerializeField] private Ease ease;
    [SerializeField] private Button settingBtn,resumeBtn,menuBtn,restartBtn;
    [SerializeField] private TextMeshProUGUI countTxt;
    [Foldout("Other")] [SerializeField] private Scrollbar sfxScroll, bgmScroll;
    [SerializeField] private PlayerKeyController playerKeyController;
    private bool isShow = false,isCounting = false,isEnd = false;
    
    private const float leftPanelShow = 50, rightPanelShow = -50,leftPanelHide = -1000,rightPanelHide = 1500,settingPanelHide = 2000;

    private void Start()
    {
        AudioManager.Inst.ChangeScrollbar(bgmScroll,sfxScroll);
        BtnSetting();
        HidePanels();
    }

    public void EndingUI(bool isWin)
    {
        isEnd = true;
        Show();
        endPanel.Show(isWin);
        if(!isWin)SetFailBtn();
        else SetWinBtn();
    }

    private void BtnSetting()
    {
        resumeBtn.onClick.AddListener(()=>Hide());
        settingBtn.onClick.AddListener(()=>ShowSettingPanel());
        settingBtn.onClick.AddListener(()=>AudioManager.Inst.SetScrollbar());
        menuBtn.onClick.AddListener(()=>FadeInFadeOutManager.Inst.FadeOut("MapSelect",true));
        restartBtn.onClick.AddListener(()=>FadeInFadeOutManager.Inst.FadeOut(SceneManager.GetActiveScene().buildIndex,true));
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isCounting && !isEnd)SetUI();
    }

    void SetAllBtn()
    {
        settingBtn.gameObject.SetActive(true);
        resumeBtn.gameObject.SetActive(true);
        menuBtn.gameObject.SetActive(true);
        restartBtn.gameObject.SetActive(true);
    }

    void SetFailBtn()
    {
        settingBtn.gameObject.SetActive(false);
        resumeBtn.gameObject.SetActive(false);
        menuBtn.gameObject.SetActive(true);
        restartBtn.gameObject.SetActive(true);
    }

    void SetWinBtn()
    {
        settingBtn.gameObject.SetActive(false);
        resumeBtn.gameObject.SetActive(false);
        menuBtn.gameObject.SetActive(true);
        restartBtn.gameObject.SetActive(true);
    }

    private void SetUI()
    {
        if (isShow) Hide();
        else
        {
            Show();
            SetAllBtn();
        }
    }

    private void ShowSettingPanel()
    {
        settingPanel.DOAnchorPos(Vector2.zero, 0.5f).SetUpdate(true).SetEase(ease);
    }

    private void Show()
    {
        playerKeyController.ChangeState(PlayerState.NoneKey);
        TimeController.ChangeTimeScale(0);
        pausePanel.SetPosition(PanelStates.Show,true);

        leftPanel.DOAnchorPos(new Vector2(leftPanelShow, 0), 0.5f).SetUpdate(true).SetEase(ease);
        rightPanel.DOAnchorPos(new Vector2(rightPanelShow,0),0.5f).SetUpdate(true).SetEase(ease);
        
        isShow = true;
    }

    private void HidePanels()
    {
        leftPanel.DOAnchorPos(new Vector2(leftPanelHide,0),0.5f).SetUpdate(true).SetEase(ease);
        rightPanel.DOAnchorPos(new Vector2(rightPanelHide,0),0.5f).SetUpdate(true).SetEase(ease);
        settingPanel.DOAnchorPos(new Vector2(settingPanelHide,0),0.5f).SetUpdate(true).SetEase(ease);
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
            playerKeyController.ChangeState(PlayerState.AllKey);
            TimeController.ChangeTimeScale(1);
            isShow = false;
            isCounting = false;
        });
        
        countSeq.Play();
    }
}
