using System;
using UnityEngine;
using DG.Tweening;

//벽점착아이템
public class item2 : MonoBehaviour
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
            Debug.Log("item(adhesion) 디버그");
            Action action = () => Debug.Log("벽 점착 아이템 사용");
            UImanager.Inst.ShowAdhesionitemUI(action);
            Destroy(gameObject);

        }
    }
    //플레이어에 닿으면 디버그하고 UI창에 이미지 보이기

}
