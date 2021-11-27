using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbominationSM : StateMachine
{
    [HideInInspector] public AbominationIdle idleState;
    [HideInInspector] public AbominationJump jumpingState;
    [HideInInspector] public AbominationTackle tacklingState;

    public Rigidbody2D rb2d;
    public Transform groundCheck;
    public bool isFacingRight;
    public LayerMask groundLayer;


    void Awake()
    {
        idleState = new AbominationIdle(this);
        jumpingState = new AbominationJump(this);
        tacklingState = new AbominationTackle(this);
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }
}
