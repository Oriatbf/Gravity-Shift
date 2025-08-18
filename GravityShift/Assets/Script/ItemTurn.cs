using DG.Tweening;
using UnityEngine;

public class ItemTurn : MonoBehaviour
{
    public PlayerGravity gravity;
    private const float dur = 1.5f;
    private void Start()
    {
        switch (gravity)
        {
            case PlayerGravity.Down:
                transform.DOLocalMoveY(transform.localPosition.y + 0.5f, dur)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutCubic);
                transform.DORotate(new Vector3(0, 360, 0), dur, RotateMode.WorldAxisAdd)
                    .SetLoops(-1, LoopType.Restart)
                    .SetEase(Ease.Linear);
                break;

            case PlayerGravity.Up:
                transform.DOLocalMoveY(transform.localPosition.y - 0.5f, dur)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutCubic);
                transform.DORotate(new Vector3(0, 360, 0), dur, RotateMode.WorldAxisAdd)
                    .SetLoops(-1, LoopType.Restart)
                    .SetEase(Ease.Linear);
                break;

            case PlayerGravity.Left:
                transform.DOLocalMoveX(transform.localPosition.x + 0.5f, dur)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutCubic);
                transform.DORotate(new Vector3(360, 0, 0), dur, RotateMode.WorldAxisAdd)
                    .SetLoops(-1, LoopType.Restart)
                    .SetEase(Ease.Linear);
                break;

            case PlayerGravity.Right:
                transform.DOLocalMoveX(transform.localPosition.x - 0.5f, dur)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutCubic);
                transform.DORotate(new Vector3(360, 0, 0), dur, RotateMode.WorldAxisAdd)
                    .SetLoops(-1, LoopType.Restart)
                    .SetEase(Ease.Linear);
                break;
        }

    }
}
