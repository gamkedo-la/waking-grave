using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlockLightblast : BaseState
{
    private WarlockSM _sm;
    private bool alternativeLightblast;

    public WarlockLightblast(WarlockSM stateMachine) : base("WarlockLightblast", stateMachine) {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        if(_sm.healthManager.OverHalfLife()) {
            _sm.lightBlastIL.SetActive(true);
        } else {
            if(alternativeLightblast) {
                _sm.lightBlastIL.SetActive(true);
            } else {
                _sm.lightBlastFS.SetActive(true);
            }
        }
        _sm.finishedLightblast = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if(_sm.finishedLightblast) {
            if(_sm.healthManager.OverHalfLife()) {
                stateMachine.ChangeState(_sm.retreatingState);
            } else {
                if(alternativeLightblast) {
                    alternativeLightblast = !alternativeLightblast;
                    stateMachine.ChangeState(_sm.wallSpawnState);
                } else {
                    alternativeLightblast = !alternativeLightblast;
                    stateMachine.ChangeState(_sm.retreatingState);
                }
            }
        }
    }
}
