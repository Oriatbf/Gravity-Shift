using System;
using DG.Tweening;
using UnityEngine;

public class Adhesion_Tutorial : TutorialItem
{
    private void Start()
    {
        transform.DOLocalMoveY(0.5f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCubic);
        transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.WorldAxisAdd).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }
    //아이템 회전, 위아래

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "Player")
        {
            PlayerCtrl playerCtrl = obj.GetComponent<PlayerCtrl>();
            Debug.Log("item(adhesion) 디버그");
            Action action = () => playerCtrl.ActiveAdhesion();
            UImanager.Inst.ShowAdhesionitemUI(action);
            EndTutorial();
            Destroy(gameObject);

        }
    }
}
