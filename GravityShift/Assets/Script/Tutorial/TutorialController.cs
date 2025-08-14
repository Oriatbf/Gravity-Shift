using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using VInspector;

public class TutorialController : Singleton<TutorialController>
{
   [SerializeField] private bool startTutorial = false;
   public List<Tutorial> tutorials = new List<Tutorial>();
   [SerializeField] private int tutorialIndex = 0;
   [SerializeField] private PlayerKeyController _playerKeyController;
   public Transform map;

   private void Start()
   {
      _playerKeyController.ChangeState(PlayerState.NoneKey);
      if (startTutorial) DOVirtual.DelayedCall(5f, () => SpawnTutorial());
   }


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

   public void ChanagePlayerState(PlayerState state) => _playerKeyController.ChangeState(state);

   public void EndTutorial()
   {
      SpawnTutorial();
   }
}
