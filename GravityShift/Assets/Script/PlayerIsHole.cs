using UnityEngine;

public class PlayerIsHole : MonoBehaviour
{
    public Transform PlayerPos;
    private Vector3 halfBoxSize = new Vector3(0.4f, 0.4f, 0.4f); // 크기는 상황에 맞게 조절하세요

    void Update()
    {
        int layerMask = ~LayerMask.GetMask("Player");
        Collider[] colliders = Physics.OverlapBox(PlayerPos.position, halfBoxSize, Quaternion.identity, layerMask);

        if (colliders.Length == 0)
            Debug.Log("추락사!");
    }
}