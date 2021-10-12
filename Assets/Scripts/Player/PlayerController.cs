using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private PlatformerInputs platformerInputs;
    private InputAction movementAction;
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

    [Header("Dash")]
    private bool isDashing;


    // TODO: Only set can jump to false when touching the ground

    private void Awake() {
        platformerInputs = new PlatformerInputs();
        rb2D = GetComponent<Rigidbody2D>();
        wallJumpAngle.Normalize();
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
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize ,0, groundLayer);
        isOnWall = Physics2D.OverlapBox(wallCheck.position, wallCheckSize, 0, wallLayer);
    }

    private void Jump(InputAction.CallbackContext obj) {
        if(isGrounded) {
            rb2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        } else if(isSliding) {
            // Disable movement for player to move away from wall
            rb2D.AddForce(new Vector2(wallJumpForce * wallJumpAngle.x * (isFacingRight ? -1 : 1), wallJumpForce * wallJumpAngle.y),ForceMode2D.Impulse);
            // Flip();
        }
    }

    private void AttemptDash(InputAction.CallbackContext obj) {
        if(!isDashing && isGrounded){
            StartCoroutine(Dash());
        }
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

    private void Move() {
        if (!isOnWall && !isDashing)
        {
            rb2D.velocity = new Vector2(horizontal * speed, rb2D.velocity.y);
        }

        if( !isFacingRight && horizontal > 0f || isFacingRight && horizontal < 0f) {
            Flip();
        }
    }

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsOnWall() {
        return Physics2D.OverlapBox(wallCheck.position, wallCheckSize, 0, wallLayer);
    }

    void WallSlide () {
        bool isPressingAgainstWall = (isFacingRight && movementAction.ReadValue<Vector2>().x > 0) ||
                                    (!isFacingRight && movementAction.ReadValue<Vector2>().x < 0);

        isSliding = IsOnWall() && !IsGrounded() && rb2D.velocity.y < 0 && isPressingAgainstWall;
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


