using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateTutorial : Tutorial
{
    [Serializable]
    public struct InstanceInfo
    {
        public Vector3 position;
        public GameObject instance;
        public float delay;
    }
    
    public List<InstanceInfo> InstanceInfos = new List<InstanceInfo>();
    protected override void Start()
    {
        base.Start();
        StartCoroutine(Action());
    }

    IEnumerator Action()
    {
        for (var i = 0; i < InstanceInfos.Count; i++)
        {
            yield return new WaitForSeconds(InstanceInfos[i].delay);
            Instantiate(InstanceInfos[i].instance,InstanceInfos[i].position, Quaternion.identity,_tutorialController.map);
        }
    }

    protected override void Update()
    {
        
    }
}
