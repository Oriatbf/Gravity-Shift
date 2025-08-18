using System;
using UnityEngine;

public class PlayerIsHole : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    private Vector3 halfBoxSize = new Vector3(0.2f, .5f, 0.2f); // 크기는 상황에 맞게 조절하세요
    private bool isDead = false;
    private PlayerCtrl playerCtrl;

    private void Awake()
    {
        playerCtrl = GetComponent<PlayerCtrl>();
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position , halfBoxSize, Quaternion.identity);

        if (colliders.Length == 0 && !isDead)
        {
            MapSpawnController.Inst.StopMapMoving();
            isDead = true; 
            playerCtrl.PlayerDead(true);
        }
        else if (colliders.Length <= 1&& !isDead)
        {
            if (colliders[0].CompareTag("Player"))
            {
                MapSpawnController.Inst.StopMapMoving();
                isDead = true; 
                SoundManager.Instance.PlaySound("fallDie");
                playerCtrl.PlayerDead(true);
            }
                
        }
            
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position+ offset, halfBoxSize*2);
    }
}