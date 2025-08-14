using System;
using Unity.VisualScripting;
using UnityEngine;


public class EffectionBlock : MonoBehaviour
{
    
    public Material obstacleMat,originalMat;
    Renderer renderer;
    [SerializeField]private  float size,length;
    [SerializeField] private Vector3 colOffset;
    [SerializeField] private Vector3 rayOffset;

    [SerializeField] private PlayerGravity rayDir;
    private Vector3 rayDirVector;
    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    private void OnValidate()
    {
       SetRayDir();
    }

    void Start()
    {
        originalMat = renderer.material;
       SetRayDir();
    }

    void SetRayDir()
    {
        switch (rayDir)
        {
            case PlayerGravity.Down:
                rayDirVector = Vector3.down;
                break;
            case PlayerGravity.Up:
                rayDirVector = Vector3.up;
                break;
            case PlayerGravity.Right:
                rayDirVector = Vector3.right;
                break;
            case PlayerGravity.Left:
                rayDirVector = Vector3.left;
                break;
        }
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + colOffset, new Vector3(size/2,size/2,length/2));

        bool obstacle =!Physics.Raycast(transform.position + (rayDirVector*-1.2f),rayDirVector,out RaycastHit hit,3f);
        if(colliders.Length <=0&&!obstacle)obstacle=true;

        if (!obstacle)
        {
            foreach (var col in colliders)
            {
                if (col.CompareTag("thorn"))
                {
                    obstacle = true;
                    break;
                }
            }
        }

        if (obstacle)
            renderer.material = obstacleMat;
        else
            renderer.material = originalMat;
        
            
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + colOffset, new Vector3(size,size,length));
        Gizmos.DrawRay(transform.position +rayOffset , rayDirVector.normalized*3f);
    }


}
