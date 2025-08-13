using System;
using UnityEngine;
public enum PlayerState
{
    AllKey,OnlyLR,OnlyShift,NoneKey,OnlyS
}
public class PlayerKeyController : MonoBehaviour
{
    [SerializeField]private PlayerState curState;

    public bool CheckCurState(PlayerState requireState)
    {
        if (requireState == curState || curState == PlayerState.AllKey) return true;
        else return false;
    }
    

    public void ChangeState(PlayerState state)
    {
        curState = state;
        switch (state)
        {
            case PlayerState.AllKey:
                break;
            case PlayerState.OnlyLR:
                break;
            case PlayerState.OnlyShift:
                break;
            case PlayerState.NoneKey:
                break;
        }
    }
}
