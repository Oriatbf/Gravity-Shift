using System;
using UnityEngine;
using DG.Tweening;

//벽점착아이템
public class item2 : ItemTurn
{

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            PlayerCtrl playerCtrl = obj.GetComponent<PlayerCtrl>();
            SFXManager.Inst.PlaySound("getItem");
            Debug.Log("item(adhesion) 디버그");
            Action action = () => playerCtrl.ActiveAdhesion();
            UImanager.Inst.ShowAdhesionitemUI(action);
            Destroy(gameObject);
        }
    }
    //플레이어에 닿으면 디버그하고 UI창에 이미지 보이기

}
