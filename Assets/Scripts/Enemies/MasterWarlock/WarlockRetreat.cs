using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlockRetreating : BaseState
{
    WarlockSM _sm;
    private Vector3 startPos;
    float speed = 3.5f;
    float xDeviation = 8.0f;
    float yDeviation = 4.0f;
    public WarlockRetreating(WarlockSM stateMachine) : base("WarlockRetreating", stateMachine)
    {
        _sm = stateMachine;
        startPos = _sm.transform.position;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (_sm.transform.position.x < warlocksRetreatPosition.transform.position.x)
        {
            _sm.transform.position.x += speed;
        }
    }
}
