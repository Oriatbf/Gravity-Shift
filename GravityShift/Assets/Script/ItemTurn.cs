using System;
using DG.Tweening;
using UnityEngine;

public class ItemTurn : MonoBehaviour
{
    public PlayerGravity gravity;
    public Transform child;
    private const float dur = 1.5f;
    [SerializeField]private Vector3 originalPos;

    private void Update()
    {
        if(child != null)
             child.transform.localPosition = Vector3.zero;
    }


    private void Start()
    {
        switch (gravity)
        {
            case PlayerGravity.Down:
                transform.DOLocalMoveY(originalPos.y + 0.5f, dur)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutCubic);
                transform.DORotate(new Vector3(0, 360, 0), dur, RotateMode.WorldAxisAdd)
                    .SetLoops(-1, LoopType.Restart)
                    .SetEase(Ease.Linear);
                break;

            case PlayerGravity.Up:
                transform.DOLocalMoveY(originalPos.y - 0.5f, dur)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutCubic);
                transform.DORotate(new Vector3(0, 360, 0), dur, RotateMode.WorldAxisAdd)
                    .SetLoops(-1, LoopType.Restart)
                    .SetEase(Ease.Linear);
                break;

            case PlayerGravity.Left:
                transform.DORotate(new Vector3(360, 0, 0), dur, RotateMode.WorldAxisAdd)
                    .SetLoops(-1, LoopType.Restart)
                    .SetEase(Ease.Linear);
                transform.DOLocalMoveX(transform.localPosition.x + 0.5f, dur)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutCubic);
              
                break;

            case PlayerGravity.Right:
                transform.DORotate(new Vector3(360, 0, 0), dur, RotateMode.WorldAxisAdd)
                    .SetLoops(-1, LoopType.Restart)
                    .SetEase(Ease.Linear);
                transform.DOLocalMoveX(transform.localPosition.x - 0.5f, dur)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutCubic);
                break;
        }

    }
}
