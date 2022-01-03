using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AbominationTackle : BaseState
{
    private AbominationSM _sm;
    private Stopwatch sw;
    float chargeSpeed = 10f;

    public AbominationTackle(AbominationSM stateMachine) : base("AbominationTackle", stateMachine) {
        _sm = stateMachine;
        sw = new Stopwatch();
    }

    public override void Enter()
    {
        base.Enter();
        _sm.anim.SetTrigger("Walk");
        chargeSpeed = _sm.isFacingRight ? 10 : -10;
        _sm.hasCrashed = false;
        sw.Restart();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if(sw.ElapsedMilliseconds > 1000) {
            sw.Stop();
            if(!_sm.hasCrashed) {
                _sm.rb2d.velocity = new Vector2(chargeSpeed, 0f);
            } else {
                _sm.Flip();
                if(_sm.healthManager.OverHalfLife()) {
                    stateMachine.ChangeState(_sm.jumpingState);
                } else {
                    if( Vector2.Distance(_sm.transform.position, _sm.playerTransform.position) > 10 ) { // if Player is "Far" Away
                        stateMachine.ChangeState(_sm.wallSpawnState);
                    } else {
                        stateMachine.ChangeState(_sm.jumpingState);
                    }
                }
            }
        } else {
            _sm.hasCrashed = false;
        }
    }
}
