using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AbominationIdle : BaseState
{
    private AbominationSM _sm;
    private Stopwatch sw;

    public AbominationIdle(AbominationSM stateMachine) : base("AbominationIdle", stateMachine) {
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
        if(sw.ElapsedMilliseconds > 2500) {
            stateMachine.ChangeState(_sm.jumpingState);
        }
    }
}
