using System;
using UnityEngine;

public class GravityDirEffctionController : Singleton<GravityDirEffctionController>
{
    [SerializeField] Transform up,bottom,left,right;
    [SerializeField] Camera subCam;

    private void Start()
    {
        HideEffection();
    }

    public void HideEffection()
    {
        subCam.enabled = false;
    }
    
    public void ShowEffction(Transform upPos,Transform bottomPos,Transform leftPos,Transform rightPos,bool isItem,PlayerGravity gravity)
    {
        subCam.enabled = true;
        up.gameObject.SetActive(true);
        bottom.gameObject.SetActive(true);
        left.gameObject.SetActive(true);
        right.gameObject.SetActive(true);
        switch (gravity)
        {
            case PlayerGravity.Up:
                up.gameObject.SetActive(false);
                bottom.gameObject.SetActive(isItem);
                break;
            case PlayerGravity.Down:
                bottom.gameObject.SetActive(false);
                up.gameObject.SetActive(isItem);
                break;
            case PlayerGravity.Left:
                left.gameObject.SetActive(false);
                right.gameObject.SetActive(isItem);
                break;
            case PlayerGravity.Right:
                right.gameObject.SetActive(false);
                left.gameObject.SetActive(isItem);
                break;
        }
        bottom.position = bottomPos.position;
        up.position = upPos.position;
        left.position = leftPos.position;
        right.position = rightPos.position;
    }
    
    
}
