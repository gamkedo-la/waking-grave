using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private PlatformerInputs platformerInputs;
    private InputAction movementAction;
    private Animator anim;
    [SerializeField] private CameraFollow cam;
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
    [Header("Shooting")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootCooldown;
    private bool canShoot;

    [Header("Health Variables")]
    [SerializeField] private Vector2 damagedForce;
    [SerializeField] private float immuneTime;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private Image healthBar;
    private bool isDamaged;

    // TODO: Only set can jump to false when touching the ground

    private void Awake() {
        platformerInputs = new PlatformerInputs();
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        wallJumpAngle.Normalize();
        canShoot = true;

        dustParticles = GetComponentInChildren<ParticleSystem>(); // fixme: we might need more than one
        if (!dustParticles) Debug.Log("Player is missing a dust particle system!");
        if(CheckpointManager.instance != null && CheckpointManager.instance.lastCheckpointPos != Vector2.zero ) {
            cam.enabled = true;
            transform.position = CheckpointManager.instance.lastCheckpointPos;
        }
    }

    private void OnEnable() {
        movementAction = platformerInputs.Player.Move;
        movementAction.Enable();

        platformerInputs.Player.Jump.performed += Jump;
        platformerInputs.Player.Jump.Enable();

        platformerInputs.Player.Dash.performed += AttemptDash;
        platformerInputs.Player.Dash.Enable();

        platformerInputs.Player.Shoot.performed += Shoot;
        platformerInputs.Player.Shoot.Enable();
    }

    private void OnDisable() {
        movementAction.Disable();
        platformerInputs.Player.Jump.Disable();
        platformerInputs.Player.Dash.Disable();
        platformerInputs.Player.Shoot.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = movementAction.ReadValue<Vector2>().x;
        bool wasGrounded = isGrounded;
        bool wasOnWall = isOnWall;
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize ,0, groundLayer);
        isOnWall = Physics2D.OverlapBox(wallCheck.position, wallCheckSize, 0, wallLayer);

        if(!wasGrounded && isGrounded || !wasOnWall && isOnWall ){
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
        rb2D.AddForce(new Vector2((isFacingRight ? 1: -1) * speed , 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        isDashing = false;
    }

    private void FixedUpdate() {
        if(!isDamaged) {
            Move();
            WallSlide();
        }
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

    private void Shoot(InputAction.CallbackContext obj)
    {
        if(canShoot) {
            canShoot = false;
            GameObject temp  = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            temp.GetComponent<Bullet>().SetDirection(isFacingRight);
            if(!isFacingRight) {
                temp.GetComponent<SpriteRenderer>().flipX = true;
            }
            Invoke("EnableShoot", shootCooldown);
        }
    }

    private void EnableShoot() {
        canShoot = true;
    }

    public void GetDamaged(float xPosition) {
        if(!isDamaged) {
            isDamaged = true;
            rb2D.velocity = Vector2.zero;
            int direction = xPosition > transform.position.x ? -1 : 1;
            Vector2 pushback = new Vector2(damagedForce.x * direction, damagedForce.y);
            rb2D.AddForce(pushback , ForceMode2D.Impulse);

            currentHealth -= 1;
            if(healthBar) {
                healthBar.fillAmount -= (1.0f / maxHealth);
            }

            if(currentHealth == 0) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            // TODO: Add restart logic when health < 0
            anim.SetTrigger("getDamaged");
        }
    }

    public void EnableDamage() {
        isDamaged = false;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(groundCheck.position, groundCheckSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(wallCheck.position, wallCheckSize);
    }
}


