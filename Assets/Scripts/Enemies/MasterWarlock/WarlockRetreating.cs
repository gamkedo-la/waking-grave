using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlockRetreating : BaseState
{
    WarlockSM _sm;
    float speed = 0.025f;

    Vector2 retreatPosition = new Vector2(-1.5f, 1);
    public WarlockRetreating(WarlockSM stateMachine) : base("WarlockRetreating", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (_sm.transform.position.x != retreatPosition.x)
        {
            _sm.transform.position = Vector2.MoveTowards(_sm.transform.position, retreatPosition, speed);
        }
        else if (Vector2.Distance(_sm.transform.position, retreatPosition) < .1f)
        {
            _sm.ChangeState(_sm.movingState);
        }
    }
}