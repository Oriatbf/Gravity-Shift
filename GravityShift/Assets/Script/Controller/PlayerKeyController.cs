using System;
using UnityEngine;
public enum PlayerState
{
    AllKey,OnlyLR,OnlyShift,NoneKey
}
public class PlayerKeyController : MonoBehaviour
{
    PlayerCtrl playerCtrl;
    [SerializeField]private PlayerState curState;

    private void Start()
    {
        playerCtrl = GetComponent<PlayerCtrl>();
    }

    public void ChnageState(PlayerState state)
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
