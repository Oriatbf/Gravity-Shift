using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int coin;
    public PlayerCtrl player;
    void Start()
    {
        coin = 0;
    }
    public void AddCoin()
    {
        coin++;
        Debug.Log($"현재 코인 수 : {coin}");
    }

    public void SetCoin(int value)
    {
        coin = value;
    }

    public int GetCoin()
    {
        return coin;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
