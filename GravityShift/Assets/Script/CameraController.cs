using System;
using DG.Tweening;
using UnityEngine;
using VInspector;

public enum PlayerGravity
{
    Down,Up,Left,Right
}

public class CameraController : Singleton<CameraController>
{
    [SerializeField] private float duration;
    [SerializeField] private Transform downTrans, upTrans, leftTrans, rightTrans;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    [Button]
    public void MoveCamera(PlayerGravity gravity)
    {
        Transform trans = downTrans;
        Vector3 rot = trans.localEulerAngles;
        switch (gravity)
        {
            case PlayerGravity.Down:
                trans = downTrans;
                rot = downTrans.localEulerAngles;
                break;
            case PlayerGravity.Up:
                trans = upTrans;
                rot = upTrans.localEulerAngles;
                break;
            case PlayerGravity.Left:
                trans = leftTrans;
                rot = leftTrans.localEulerAngles;
                break;
            case PlayerGravity.Right:
                trans = rightTrans;
                rot = rightTrans.localEulerAngles;
                break;
        }

        _camera.transform.DOMove(trans.position, duration).SetUpdate(true);
        _camera.transform.DOLocalRotate(rot, duration).SetUpdate(true);
      
    }

    
    
}
