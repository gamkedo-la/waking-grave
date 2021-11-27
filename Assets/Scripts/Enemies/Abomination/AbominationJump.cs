using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbominationJump : BaseState
{
    private AbominationSM _sm;
    private const int MAX_JUMPS = 5;
    float jumpForce = 10f;
    float horizontalSpeed = 5f;
    int currentJumps;
    int jumpCounter;
    bool hasJumped;
    bool isGrounded;

    public AbominationJump(AbominationSM stateMachine) : base("AbominationJump", stateMachine) {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        jumpCounter = 0;
        currentJumps = (int) Random.Range(1, MAX_JUMPS);
        isGrounded = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapBox(_sm.groundCheck.position, new Vector2(0.3f, 0.08f) , 0, _sm.groundLayer);
        if(isGrounded && !wasGrounded) {
            _sm.rb2d.velocity = Vector2.zero;
            if(jumpCounter%2== 0 && jumpCounter != 0) {
                _sm.isFacingRight = !_sm.isFacingRight;
                _sm.GetComponent<SpriteRenderer>().flipX = _sm.isFacingRight;
            }
            if(jumpCounter == currentJumps) {
                stateMachine.ChangeState(_sm.tacklingState);
            } else {
                _sm.rb2d.AddForce(new Vector2(horizontalSpeed * (_sm.isFacingRight ? 1 : -1), jumpForce), ForceMode2D.Impulse);
                jumpCounter++;
            }
        }
    }

}
