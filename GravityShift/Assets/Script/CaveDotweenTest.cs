using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using VInspector;

public class CaveDotweenTest : MonoBehaviour
{
    public Transform spawnTrans;
    private List<Transform> childs = new List<Transform>();
    [SerializeField] private float secPerRatio;
    private float count;

    private const float AnimPos = 14;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            childs.Add(child);
            child.transform.position -= new Vector3(0, AnimPos, 0);
        }
        childs.Shuffle();
        count = childs.Count *(secPerRatio/100f);
        Up();
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
            child.DOMoveY(newPos.y, 0.35f).SetDelay(delay);
        }
    }
}
