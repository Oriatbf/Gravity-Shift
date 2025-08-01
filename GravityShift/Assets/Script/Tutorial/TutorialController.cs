using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using VInspector;

public class TutorialController : MonoBehaviour
{
   public List<Tutorial> tutorials = new List<Tutorial>();
   [SerializeField] private int tutorialIndex = 0;
   [SerializeField] private PlayerKeyController _playerKeyController;
   public Transform map;
   

   [Button]
   public void SpawnTutorial()
   {
      if (tutorialIndex < tutorials.Count)
      {
         Tutorial _tutorial = tutorials[tutorialIndex];
         var t =Instantiate(_tutorial, transform.position, Quaternion.identity);
         t.Inject(this);
         tutorialIndex++;
      }
   }

   public void ChanagePlayerState(PlayerState state) => _playerKeyController.ChnageState(state);

   public void EndTutorial()
   {
      DOVirtual.DelayedCall(1f, () => SpawnTutorial());
   }
}
