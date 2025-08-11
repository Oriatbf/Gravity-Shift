using System;
using UnityEngine;

public class PlayerIsHole : MonoBehaviour
{
    public Transform PlayerPos;
    private Vector3 halfBoxSize = new Vector3(0.4f, 0.4f, 0.4f); // 크기는 상황에 맞게 조절하세요
    private bool isDead = false;

    void Update()
    {
        PlayerPos.position = new Vector3(PlayerPos.position.x, PlayerPos.position.y, PlayerPos.position.z);
        int layerMask = ~LayerMask.GetMask("Player");
        Collider[] colliders = Physics.OverlapBox(PlayerPos.position, halfBoxSize, Quaternion.identity, layerMask);

        if (colliders.Length == 0 && !isDead)
        {
            isDead = true;
            SettingController.Inst.EndingUI(false);
        }
            
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, halfBoxSize*2);
    }
}