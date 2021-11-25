using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class WarlockMoving : BaseState
{
    WarlockSM _sm;
    Stopwatch stopWatch;

    private Vector3 centerMovementReferencePoint;
    float speed = 3.5f;
    float xDeviation = 8.0f;
    float yDeviation = 4.0f;
    public WarlockMoving(WarlockSM stateMachine) : base("WarlockMoving", stateMachine) {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        stopWatch = new Stopwatch();
        stopWatch.Restart();
        centerMovementReferencePoint = _sm.transform.position;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        float elapsedTimeFloat = (float)stopWatch.ElapsedMilliseconds / 1000;

        _sm.transform.position = centerMovementReferencePoint + (Vector3.right * Mathf.Sin( (elapsedTimeFloat) / 2 * speed) * xDeviation -
                                              Vector3.up * Mathf.Sin(elapsedTimeFloat * speed) * yDeviation);
    }
}
