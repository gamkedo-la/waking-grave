using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlockLightblast : BaseState
{
    private WarlockSM _sm;

    public WarlockLightblast(WarlockSM stateMachine) : base("WarlockLightblast", stateMachine) {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _sm.lightBlastFS.SetActive(true);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if(_sm.finishedLightblast) {
            stateMachine.ChangeState(_sm.movingState);
        }
    }
}
