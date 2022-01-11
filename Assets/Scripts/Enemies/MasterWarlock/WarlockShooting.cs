using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;


public class WarlockShooting : BaseState
{
    private WarlockSM _sm;
    private Stopwatch sw;
    private bool hasShot;


    public WarlockShooting(WarlockSM stateMachine) : base("WarlockShooting", stateMachine) {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        sw = new Stopwatch();
        sw.Start();
        hasShot = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if(sw.ElapsedMilliseconds > 1000 && !hasShot) {
            UnityEngine.Debug.Log("Shoot");
            hasShot = true;
        }
        if (sw.ElapsedMilliseconds > 2000) {
            stateMachine.ChangeState(_sm.movingState);
        }
    }
}
