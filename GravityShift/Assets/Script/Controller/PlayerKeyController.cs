using System;
using UnityEngine;
public enum PlayerState
{
    AllKey,OnlyLR,OnlyShift,NoneKey,OnlyS
}
public class PlayerKeyController : MonoBehaviour
{
    [SerializeField]private PlayerState curState;

    private void Start()
    {
        if(DataManager.Inst.Data.curStage != 0)ChangeState(PlayerState.AllKey);
    }

    public bool CheckCurState(PlayerState requireState)
    {
        if (requireState == curState || curState == PlayerState.AllKey) return true;
        else return false;
    }
    

    public void ChangeState(PlayerState state)
    {
        curState = state;

    }
}
