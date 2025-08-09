using System;
using UnityEngine;
using DG.Tweening;


public class item1 : MonoBehaviour
{
    
    private void Start()
    {
        transform.DOLocalMoveY(0.5f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCubic);
        transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.WorldAxisAdd).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }
    //아이템 회전, 위아래


    void OnTriggerEnter(Collider obj)
    {
        if (obj.CompareTag("Player"))
        {
            //PlayerCtrl playerCtrl = obj.GetComponent<PlayerCtrl>();
            Debug.Log("item1(invincible) 디버그");
            Action action = () => Debug.Log("ppp");
            UImanager.Inst.ShowinvincibleitemUI(action);
            Destroy(gameObject);

        }
    }
    //플레이어에 닿으면 디버그하고 UI창에 이미지 보이기
}
