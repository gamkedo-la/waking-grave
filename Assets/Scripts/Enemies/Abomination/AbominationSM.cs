using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbominationSM : StateMachine
{
    [HideInInspector] public AbominationIdle idleState;
    [HideInInspector] public AbominationJump jumpingState;
    [HideInInspector] public AbominationTackle tacklingState;
    [HideInInspector] public AbominationWallSpawn wallSpawnState;

    public Rigidbody2D rb2d;
    public Transform groundCheck;
    public Animator anim;
    public bool isFacingRight;
    public LayerMask groundLayer;
    public bool hasCrashed;
    public bool isTackling;
    public EnemyHealthManager healthManager;
    public Transform playerTransform;
    private SpriteRenderer sr;

    public GameObject[] wallPrefabs;

    void Awake()
    {
        idleState = new AbominationIdle(this);
        jumpingState = new AbominationJump(this);
        tacklingState = new AbominationTackle(this);
        wallSpawnState = new AbominationWallSpawn(this);
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        healthManager = GetComponent<EnemyHealthManager>();
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
        if(other.gameObject.CompareTag("Wall")) {
            hasCrashed = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Add Logic to hurt player
    }

    public void SpawnWall(int index) {
        GameObject wall = Instantiate(wallPrefabs[index], new Vector3 (transform.position.x + (isFacingRight ? 3 : -3), -14.5f + (0.5f*index) , 0), Quaternion.identity);
        wall.GetComponent<AbominationWall>().SetDirection(isFacingRight);
    }
}
