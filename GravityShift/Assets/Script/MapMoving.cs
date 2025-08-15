using System;
using DG.Tweening;
using UnityEngine;
using VInspector;

public class MapMoving : MonoBehaviour
{
    public int movespeed;
    private bool isMoving = true;

    [Button]
    public void StopMoving()
    {
        isMoving = false;
    }

    void Update()
    {
        if(isMoving)
            transform.Translate(-Vector3.forward*Time.deltaTime*movespeed, Space.World);
    }
}
