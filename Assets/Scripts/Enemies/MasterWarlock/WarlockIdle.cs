using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class WarlockIdle : BaseState
{
    private WarlockSM _sm;
    private Stopwatch sw;


    public WarlockIdle(WarlockSM stateMachine) : base("WarlockIdle", stateMachine) {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        sw = new Stopwatch();
        sw.Start();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if(sw.ElapsedMilliseconds > 1500) {
            stateMachine.ChangeState(_sm.lightblastState);
        }
    }
}