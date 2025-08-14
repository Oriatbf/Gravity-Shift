using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class GetItemTutorial : Tutorial
{
    public List<InstanceInfo> InstanceInfos = new List<InstanceInfo>();
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
            if (a.TryGetComponent<TutorialItem>(out TutorialItem item)) item.Init(this);
        }
    }
    
}
