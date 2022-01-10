using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlockSM : StateMachine
{
    [HideInInspector] public WarlockIdle idleState;
    [HideInInspector] public WarlockMoving movingState;
    [HideInInspector] public WarlockRetreating retreatingState;
    [HideInInspector] public WarlockLightblast lightblastState;
    public Rigidbody2D rb2d;

    [SerializeField] GameObject warlocksRetreatPosition;
    private CircleCollider2D warlocksCircleCollider;

    WarlockSM _sm;
    public GameObject lightBlastFS; // Lightblast the whole screen
    public bool finishedLightblast;
    public EnemyHealthManager healthManager;
    public Transform playerTransform;
    public bool onSecondLoop; // to indicate if the SM is currently in the second loop of states.

    private void Awake()
    {
        idleState = new WarlockIdle(this);
        movingState = new WarlockMoving(this);
        retreatingState = new WarlockRetreating(this);
        lightblastState = new WarlockLightblast(this);
        rb2d = GetComponent<Rigidbody2D>();
        warlocksCircleCollider = GetComponent<CircleCollider2D>();
        healthManager = GetComponent<EnemyHealthManager>();
        _sm = this;
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }
}

