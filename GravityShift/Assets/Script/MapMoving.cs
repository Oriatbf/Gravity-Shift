using System;
using UnityEngine;

public class MapMoving : MonoBehaviour
{
    public int movespeed;


    void Update()
    {
        transform.Translate(-Vector3.forward*Time.deltaTime*movespeed, Space.World);
    }
}
