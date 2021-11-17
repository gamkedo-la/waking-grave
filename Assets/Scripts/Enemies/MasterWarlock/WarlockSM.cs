using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlockSM : StateMachine
{
    [HideInInspector] public WarlockIdle idleState;
    [HideInInspector] public WarlockMoving movingState;
    public Rigidbody2D rb2d;

    private void Awake()
    {
        idleState = new WarlockIdle(this);
        movingState = new WarlockMoving(this);
        rb2d = GetComponent<Rigidbody2D>();
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }
}

