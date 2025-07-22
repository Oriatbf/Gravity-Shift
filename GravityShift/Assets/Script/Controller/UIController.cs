using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar;

    [SerializeField] private Transform player,endTrans;
    private float totalDistance;

    private void Start()
    {
        scrollbar.value = 0;
        totalDistance = Vector3.Distance(player.position, endTrans.position);
        
    }

    private void Update()
    {
        float curDistance = Vector3.Distance(player.position, endTrans.position);
        scrollbar.value = (totalDistance- curDistance) / totalDistance;
    }
}
