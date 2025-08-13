using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UImanager : Singleton<UImanager>
{
    public List<GameObject> coinImage;
    private List<Image> _image = new();
    private int currentCoinCount = 0;
    //coin
    public TMP_Text coinnumber;

    private Action curAction;
    public Image invincibleItemImage;
    public Image adhesionItemImage;
    private bool havingItem = false;
    [SerializeField]private PlayerKeyController playerKeyController;

    void Awake()
    {
        for (int i = 0; i < 3; i++)
        {

            Image img = coinImage[i].GetComponent<Image>();
            _image.Add(img);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            _image[i].color = new Vector4(0f, 0f, 0f, 1f);

        }

    }

    // Update is called once per frame

    public void SetCoinUI()
    {
        if (currentCoinCount >= 3)
        {
            return;
        }

        _image[currentCoinCount].color = Color.yellow;
        currentCoinCount++;

        GameManager.Inst.SetCoin(currentCoinCount);
        UpdateCoinText();
    }
    //색변환, 코인 개수 텍스트

    public void UpdateCoinText()
    {
        int coin = GameManager.Inst.GetCoin();
        coinnumber.text = $"코인 : {coin}";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && havingItem && playerKeyController.CheckCurState(PlayerState.OnlyS))
        {
            UseItem();
        }
    }
    //coin

    public void ShowinvincibleitemUI(Action action)
    {
        if (havingItem) return;
        havingItem = true;
        curAction = action;
        invincibleItemImage.enabled = true;
    }
    //무적 아이템

    public void ShowAdhesionitemUI(Action action)
    {
        if (havingItem) return;
        havingItem = true;
        curAction = action;
        adhesionItemImage.enabled = true;
    }

    public void UseItem()
    {
        invincibleItemImage.enabled = false;
        adhesionItemImage.enabled = false;
        curAction?.Invoke();
        havingItem = false;
    }
    //벽 점착 아이템
}
