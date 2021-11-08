using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private PlatformerInputs platformerInputs;
    private InputAction movementAction;
    private Animator anim;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] Vector2 groundCheckSize;
    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;


    private float horizontal;
    private bool isFacingRight = true;
    private bool isGrounded = false;

    [Header("WallSlide")]
    [SerializeField] private float wallSlideSpeed = 0;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Vector2 wallCheckSize;
    private bool isOnWall;
    private bool isSliding;

    [Header("Wall Jump")]
    [SerializeField] private float wallJumpForce = 18f;
    [SerializeField] private Vector2 wallJumpAngle;
    private bool isWallJumping;

    [Header("Dash")]
    private bool isDashing;
    private bool isDashJumping;

    private ParticleSystem dustParticles;

    // TODO: Only set can jump to false when touching the ground

    private void Awake() {
        platformerInputs = new PlatformerInputs();
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        wallJumpAngle.Normalize();

        dustParticles = GetComponentInChildren<ParticleSystem>(); // fixme: we might need more than one
        if (!dustParticles) Debug.Log("Player is missing a dust particle system!");
    }

    private void OnEnable() {
        movementAction = platformerInputs.Player.Move;
        movementAction.Enable();

        platformerInputs.Player.Jump.performed += Jump;
        platformerInputs.Player.Jump.Enable();

        platformerInputs.Player.Dash.performed += AttemptDash;
        platformerInputs.Player.Dash.Enable();
    }

    private void OnDisable() {
        movementAction.Disable();
        platformerInputs.Player.Jump.Disable();
        platformerInputs.Player.Dash.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = movementAction.ReadValue<Vector2>().x;
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize ,0, groundLayer);
        isOnWall = Physics2D.OverlapBox(wallCheck.position, wallCheckSize, 0, wallLayer);

        if(!wasGrounded && isGrounded){
            isDashJumping = false;
        }

        // toggle the dust particles on and off
        if (isGrounded) {
            // note: only actually emits when we are in motion
            if (!dustParticles.isEmitting) dustParticles.Play(true);
        } else {
            // don't emit while in the air
            if (dustParticles.isEmitting) dustParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    private void Jump(InputAction.CallbackContext obj) {
        if(isGrounded) {
            rb2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            if(isDashing) isDashJumping = true;
            anim.SetTrigger("Jump");
        } else if(isSliding) {
            StartCoroutine(WallJump());
            // Flip();
        }
    }

    private void AttemptDash(InputAction.CallbackContext obj) {
        if(!isDashing && isGrounded){
            StartCoroutine(Dash());
        }
    }

    private IEnumerator WallJump() {
        isWallJumping = true;
        rb2D.AddForce(new Vector2(wallJumpForce * wallJumpAngle.x * (isFacingRight ? -1 : 1), wallJumpForce * wallJumpAngle.y),ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        isWallJumping = false;
    }

    private IEnumerator Dash() {
        isDashing = true;
        rb2D.AddForce(new Vector2((isFacingRight ? 1: -1) * speed * 2, 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        isDashing = false;
    }

    private void FixedUpdate() {
        Move();
        WallSlide();
    }

    private void LateUpdate() {
        anim.SetBool("Idle", horizontal == 0);
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetFloat("VerticalVelocity", rb2D.velocity.y);
    }

    private void Move() {
        if(isDashJumping) {
            // if changes direction for dash
            if( !isFacingRight && horizontal > 0f || isFacingRight && horizontal < 0f) {
                isDashJumping = false;
            }
        }

        if (!isOnWall && !isDashing && !isWallJumping && !isDashJumping)
        {
            rb2D.velocity = new Vector2(horizontal * speed, rb2D.velocity.y);
        }

        if( !isFacingRight && horizontal > 0f || isFacingRight && horizontal < 0f) {
            Flip();
        }
    }

    void WallSlide () {
        bool isPressingAgainstWall = (isFacingRight && movementAction.ReadValue<Vector2>().x > 0) ||
                                    (!isFacingRight && movementAction.ReadValue<Vector2>().x < 0);

        isSliding = isOnWall && !isGrounded && rb2D.velocity.y < 0 && isPressingAgainstWall;
        if(isSliding) {
            rb2D.velocity = new Vector2(rb2D.velocity.x, wallSlideSpeed);
            // Flip();
        }
    }

    private void Flip() {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180f, 0);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(groundCheck.position, groundCheckSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(wallCheck.position, wallCheckSize);
    }
}


