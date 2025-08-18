using UnityEngine;
using DG.Tweening;


public class Coin : ItemTurn
{
    public int coin = 0;
    

    void OnTriggerEnter(Collider obj)
    {
        Debug.Log($"Trigger Enter : {obj} - {obj.tag}");
        if (obj.gameObject.tag == "Player")
        {
            SFXManager.Inst.PlaySound("getCoin");
            Debug.Log("코인디버그");
            UImanager.Inst.SetCoinUI();
            Destroy(gameObject);
            coin += 1;

            GameManager.Inst.AddCoin();

        }
        //코인 닿으면 디버그, 사라짐

    }
}
