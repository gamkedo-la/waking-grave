using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbominationJump : BaseState
{
    private AbominationSM _sm;
    private const int MAX_JUMPS = 3;  // it will be multiplied by 2, i think 6 might be a bit too much but we'll see
    float jumpForce = 10f;
    float horizontalSpeed = 6f;
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
        // for now the jumps will be a multiple of two as I'm unsure on how to detect the position in a clean way and which attacks to do from there
        currentJumps = 4;
        isGrounded = false;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapBox(_sm.groundCheck.position, new Vector2(0.3f, 0.08f) , 0, _sm.groundLayer);
        if(isGrounded && !wasGrounded) {
            _sm.rb2d.velocity = Vector2.zero; // velocity needs to be restarted otherwise previous velocities add up
            if(jumpCounter%2== 0 && jumpCounter != 0) {
                _sm.Flip();
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
