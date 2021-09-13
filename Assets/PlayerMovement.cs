using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private PlatformerInputs platformerInputs;
    private InputAction movementAction;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] Vector2 groundCheckSize;
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private float speed = 10f;
    [SerializeField] float airMoveSpeed = 10f;


    private float horizontal;
    private bool isFacingRight = true;
    private bool isGrounded = false;
    private bool canJump = false;

    [Header("WallSlide")]
    [SerializeField] float wallSlideSpeed = 0;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Transform wallCheck;
    [SerializeField] Vector2 wallCheckSize;
    private bool isOnWall;
    private bool isSliding;

    [Header("Wall Jump")]
    [SerializeField] float wallJumpForce = 18f;
    [SerializeField] Vector2 wallJumpAngle;

    // TODO: Only set can jump to false when touching the ground

    private void Awake() {
        platformerInputs = new PlatformerInputs();
        rb2D = GetComponent<Rigidbody2D>();
        wallJumpAngle.Normalize();
    }

    private void OnEnable() {
        movementAction = platformerInputs.Player.Move;
        movementAction.Enable();

        platformerInputs.Player.Jump.performed += NJump;
        platformerInputs.Player.Jump.Enable();
    }

    private void OnDisable() {
        movementAction.Disable();
        platformerInputs.Player.Jump.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = movementAction.ReadValue<Vector2>().x;
        bool wasGrounded = isGrounded;
        bool wasOnWall = isOnWall;
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize ,0, groundLayer);
        isOnWall = Physics2D.OverlapBox(wallCheck.position, wallCheckSize, 0, wallLayer);

        if(!wasGrounded && isGrounded || !wasOnWall && isOnWall) {
            canJump = false;
        }
    }

    private void NJump(InputAction.CallbackContext obj) {
        canJump = true;
    }

    private void FixedUpdate() {
        Move();
        Jump();
        WallSlide();
    }

    private void Move() {
        if (isGrounded)
        {
            rb2D.velocity = new Vector2(horizontal * speed, rb2D.velocity.y);
        }
        else if (!isGrounded &&(!isSliding || !isOnWall) && horizontal!=0 )
        {
            rb2D.AddForce(new Vector2(airMoveSpeed*horizontal,0));
            if (Mathf.Abs(rb2D.velocity.x)> speed)
            {
                rb2D.velocity = new Vector2(horizontal * speed, rb2D.velocity.y);
            }
        }

        if( !isFacingRight && horizontal > 0f || isFacingRight && horizontal < 0f) {
            Flip();
        }
    }

    public void Jump() {
        if(canJump && isGrounded) {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
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
            Flip();
        }

        if(isSliding && canJump) {
            rb2D.AddForce(new Vector2(wallJumpForce * wallJumpAngle.x * (isFacingRight ? 1 : -1), wallJumpForce * wallJumpAngle.y),ForceMode2D.Impulse);
            Flip();
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


