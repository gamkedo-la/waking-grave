using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbominationTackle : BaseState
{
    private AbominationSM _sm;
    float chargeSpeed = 10f;

    public AbominationTackle(AbominationSM stateMachine) : base("AbominationTackle", stateMachine) {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        chargeSpeed = chargeSpeed * (_sm.isFacingRight ? 1 : -1);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _sm.rb2d.velocity = new Vector2(chargeSpeed, 0f);
    }
}
