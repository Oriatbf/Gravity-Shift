using UnityEngine;
using DG.Tweening;


public class Coin : MonoBehaviour
{
    private void Start()
    {
        transform.DOLocalMoveY(0.5f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCubic);
        transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.WorldAxisAdd).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag == "coin")
        {
            Debug.Log("코인디버그");
            Destroy (obj.gameObject);
        }
    }

}
