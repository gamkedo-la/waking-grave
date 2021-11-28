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
    public bool hasCrashed;
    public bool isTackling;
    private SpriteRenderer sr;

    void Awake()
    {
        idleState = new AbominationIdle(this);
        jumpingState = new AbominationJump(this);
        tacklingState = new AbominationTackle(this);
        sr = GetComponent<SpriteRenderer>();
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }

    public void Flip() {
        isFacingRight = !isFacingRight;
        sr.flipX = isFacingRight;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Wall") && currentState.name == "AbominationTackle") {
            Debug.Log("choco");
            hasCrashed = true;
        }
    }
}
