using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using VInspector;

public class CaveDotweenTest : MonoBehaviour
{
    private List<Transform> childs = new List<Transform>();
    [SerializeField] private float secPerCount;

    private void Start()
    {
        foreach (Transform child in transform) childs.Add(child);
        childs.Shuffle();
    }

    [Button]
    private void Up()
    { 
        float delayBetween = 1/secPerCount;
        for (int i = 0; i < childs.Count; i++)
        {
            Transform child = childs[i];
            Vector3 newPos = child.position + new Vector3(0, 10, 0);
            float delay = i * delayBetween;
            child.DOMoveY(newPos.y, 0.35f).SetDelay(delay);
        }
    }
}
