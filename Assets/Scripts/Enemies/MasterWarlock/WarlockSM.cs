using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlockSM : StateMachine
{
    [HideInInspector] public WarlockIdle idleState;
    [HideInInspector] public WarlockMoving movingState;
    [HideInInspector] public WarlockRetreat retreatingState;
    public Rigidbody2D rb2d;

    [SerializeField] GameObject warlocksRetreatPosition;
    private CircleCollider2D warlocksCircleCollider;

    private void Awake()
    {
        idleState = new WarlockIdle(this);
        movingState = new WarlockMoving(this);
        rb2d = GetComponent<Rigidbody2D>();
        warlocksCircleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //stateMachine.ChangeState(_sm.retreatingState);
    }
    protected override BaseState GetInitialState()
    {
        return idleState;
    }
}

