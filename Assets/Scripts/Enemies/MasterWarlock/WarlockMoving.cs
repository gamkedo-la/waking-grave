using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class WarlockMoving : BaseState
{
    WarlockSM _sm;
    Stopwatch sw;

    private Vector3 centerMovementReferencePoint;
    float speed = 3.5f;
    float xDeviation = 8.0f;
    float yDeviation = 3.0f;
    int endpoint;
    public WarlockMoving(WarlockSM stateMachine) : base("WarlockMoving", stateMachine) {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        sw = new Stopwatch();
        sw.Restart();
        _sm.transform.position = new Vector2(-1.5f, 1);
        endpoint = Random.Range(2000, 4000);
        centerMovementReferencePoint = _sm.transform.position;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        float elapsedTimeFloat = (float)sw.ElapsedMilliseconds / 1000;

        _sm.transform.position = centerMovementReferencePoint + (Vector3.right * Mathf.Sin((elapsedTimeFloat) / 2 * speed) * xDeviation -
                                              Vector3.up * Mathf.Sin(elapsedTimeFloat * speed) * yDeviation);
        if(sw.ElapsedMilliseconds > endpoint) {
            if(!_sm.onSecondLoop) {
                _sm.onSecondLoop = true;
                stateMachine.ChangeState(_sm.shootingState);
            } else {
                _sm.onSecondLoop = false;
                stateMachine.ChangeState(_sm.shootingState);
            }
        }
    }
}
