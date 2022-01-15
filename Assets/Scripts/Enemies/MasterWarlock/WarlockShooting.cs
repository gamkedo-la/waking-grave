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
        _sm.anim.SetTrigger("StartBlast");
        _sm.eldritchBlast.gameObject.SetActive(true);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if(sw.ElapsedMilliseconds > 1000 && !hasShot) {
            _sm.anim.SetTrigger("EndBlast");
            hasShot = true;
            _sm.eldritchBlast.Shoot();
        }
        if (sw.ElapsedMilliseconds > 3000) {
            if(_sm.healthManager.OverHalfLife()) {
                stateMachine.ChangeState(_sm.retreatingState);
            } else {
                stateMachine.ChangeState(_sm.lightblastState);
            }
        }
    }
}
