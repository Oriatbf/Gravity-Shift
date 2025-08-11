using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using VInspector;

public class CaveDotweenTest : MonoBehaviour
{
    public Transform spawnTrans;
    private List<Transform> childs = new List<Transform>();
    [SerializeField] private float secPerRatio = 30;
    [SerializeField] private bool startDotween = true;
    [SerializeField] private List<Transform> Objects = new List<Transform>();
    private float count;

    private const float AnimPos = 14;
    private float spawnTransZ;
    private bool spawnBool;

    private void Start()
    {
        spawnBool = true;
        foreach (Transform parent in Objects)
        {
            foreach (Transform child in parent)
            {
                childs.Add(child);
                if(startDotween) child.transform.position -= new Vector3(0, AnimPos, 0);   
            }
        }
        childs.Shuffle();
        count = childs.Count *(secPerRatio/100f);
        if(startDotween)Up();
    }

    private void Update()
    {
        if(spawnBool)
            spawnTransZ = spawnTrans.transform.position.z;
        
        if (spawnTransZ < 0)
        {
            Debug.Log("0보다 작아짐");
            MapSpawnController.Inst.SpawnMap();
            spawnBool = false;
            Destroy(gameObject,3);
            spawnTransZ = 1;
        }
            
    }

    [Button]
    private void Up()
    { 
        float delayBetween = 1/count;
        for (int i = 0; i < childs.Count; i++)
        {
            Transform child = childs[i];
            Vector3 newPos = child.position + new Vector3(0, AnimPos, 0);
            float delay = i * delayBetween;
            child.DOMoveY(newPos.y, 0.25f).SetDelay(delay);
        }
    }
}
