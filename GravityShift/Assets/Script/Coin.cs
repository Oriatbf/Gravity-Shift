using UnityEngine;
using DG.Tweening;


public class Coin : MonoBehaviour
{
    public int coin;
    public PlayerGravity gravity;
    private void Start()
    {
        switch (gravity)
        {
            case PlayerGravity.Down:
                transform.DOLocalMoveY(transform.position.y+0.5f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCubic);
                transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.WorldAxisAdd).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
                break;
            case PlayerGravity.Up:
                transform.DOLocalMoveY(transform.position.y-0.5f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCubic);
                transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.WorldAxisAdd).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
                break;
            case PlayerGravity.Left:
                transform.DOLocalMoveX(transform.position.x+0.5f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCubic);
                transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.WorldAxisAdd).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
                break;
            case PlayerGravity.Right:
                transform.DOLocalMoveX(transform.position.x-0.5f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCubic);
                transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.WorldAxisAdd).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
                break;
        }
        //코인 회전, 위아래

        coin = 0;

    }

    void OnTriggerEnter(Collider obj)
    {
        Debug.Log($"Trigger Enter : {obj} - {obj.tag}");
        if (obj.gameObject.tag == "Player")
        {
            SoundManager.Instance.PlaySound("getCoin");
            Debug.Log("코인디버그");
            UImanager.Inst.SetCoinUI();
            Destroy(gameObject);
            coin += 1;

            GameManager.Inst.AddCoin();

        }
        //코인 닿으면 디버그, 사라짐

    }
}
