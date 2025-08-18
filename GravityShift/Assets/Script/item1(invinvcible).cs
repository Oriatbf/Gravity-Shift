using System;
using UnityEngine;
using DG.Tweening;


public class item1 : ItemTurn
{


    void OnTriggerEnter(Collider obj)
    {
        if (obj.CompareTag("Player"))
        {
            PlayerCtrl playerCtrl = obj.GetComponent<PlayerCtrl>();
            SFXManager.Inst.PlaySound("getItem");
            Debug.Log("item1(invincible) 디버그");
            Action action = () => playerCtrl.ActiveInvincible(3f);
            UImanager.Inst.ShowinvincibleitemUI(action);
            Destroy(gameObject);

        }
    }
    //플레이어에 닿으면 디버그하고 UI창에 이미지 보이기
}
