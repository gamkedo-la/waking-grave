using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlockMoving : BaseState
{
    WarlockSM _sm;
    private Vector3 startPos;
    float speed = 3.5f;
    float xDeviation = 8.0f;
    float yDeviation = 4.0f;
    public WarlockMoving(WarlockSM stateMachine) : base("WarlockMoving", stateMachine) {
        _sm = stateMachine;
        startPos = _sm.transform.position;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _sm.transform.position =  startPos + (Vector3.right * Mathf.Sin(Time.timeSinceLevelLoad/2*speed) * xDeviation - Vector3.up * Mathf.Sin(Time.timeSinceLevelLoad * speed) * yDeviation);
        
    }
}
