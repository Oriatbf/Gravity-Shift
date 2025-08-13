using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
[Serializable]
public struct InstanceInfo
{
    public Vector3 position;
    public GameObject instance;
    public float delay;
}
public class InstantiateTutorial : Tutorial
{
    
    public List<InstanceInfo> InstanceInfos = new List<InstanceInfo>();
    private List<GameObject> _instances = new List<GameObject>();
    protected override void Start()
    {
        isActive = false;
        _tutorialController.ChanagePlayerState(_playerState);
        DOVirtual.DelayedCall(tutorialDelay, () =>
        {
            StartSet();
            StartCoroutine(Action());
        } );
        
    }

    IEnumerator Action()
    {
        for (var i = 0; i < InstanceInfos.Count; i++)
        {
            yield return new WaitForSeconds(InstanceInfos[i].delay);
            var rot = InstanceInfos[i].instance.transform.rotation;
            var a=Instantiate(InstanceInfos[i].instance,InstanceInfos[i].position,rot,_tutorialController.map);
            _instances.Add(a);
        }
        isActive = true;
    }

    protected override void Update()
    {
        if (!isActive) return;
        if (_instances.Count <= 0 && isActive)
        {
            isActive = false;
            FinishTutorial();
        }
        _instances = _instances.Where(instance => instance.transform.position.z >= 0).ToList();
        foreach (var instance in _instances)
        {
            if(instance.transform.position.z<0)_instances.Remove(instance);
        }
    }
}
