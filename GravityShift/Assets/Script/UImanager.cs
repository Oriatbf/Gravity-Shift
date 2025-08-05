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
    //coin

    public Image invincibleItemImage;

    public void ShowinvincibleitemUI()
    {
        invincibleItemImage.enabled = true;
    }
    public void HideinvincibleitemUI()
    {
        invincibleItemImage.enabled = false;
    }
    //무적 아이템


    public Image adhesionItemImage;

    public void ShowAdhesionitemUI()
    {
        adhesionItemImage.enabled = true;
    }
    public void HideAdhesionitemUI()
    {
        adhesionItemImage.enabled = false;
    }
    //벽 점착 아이템
}
