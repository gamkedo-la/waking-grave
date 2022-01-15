using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlockSM : StateMachine
{
    [HideInInspector] public WarlockIdle idleState;
    [HideInInspector] public WarlockMoving movingState;
    [HideInInspector] public WarlockRetreating retreatingState;
    [HideInInspector] public WarlockLightblast lightblastState;
    [HideInInspector] public WarlockShooting shootingState;
    [HideInInspector] public WarlockWallSpawn wallSpawnState;

    public Rigidbody2D rb2d;

    [SerializeField] GameObject warlocksRetreatPosition;
    private CircleCollider2D warlocksCircleCollider;

    WarlockSM _sm;
    public GameObject lightBlastFS; // Lightblast the whole screen
    public GameObject lightBlastIL; // Lightblast the interleaved

    public bool finishedLightblast;
    public EnemyHealthManager healthManager;
    public Transform playerTransform;
    public Animator anim;
    public bool onSecondLoop; // to indicate if the SM is currently in the second loop of states.
    public EldritchBlast eldritchBlast;
    public GameObject[] wallPrefabs;

    private void Awake()
    {
        idleState = new WarlockIdle(this);
        movingState = new WarlockMoving(this);
        retreatingState = new WarlockRetreating(this);
        lightblastState = new WarlockLightblast(this);
        shootingState = new WarlockShooting(this);
        wallSpawnState = new WarlockWallSpawn(this);
        rb2d = GetComponent<Rigidbody2D>();
        warlocksCircleCollider = GetComponent<CircleCollider2D>();
        healthManager = GetComponent<EnemyHealthManager>();
        anim = GetComponent<Animator>();
        _sm = this;
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }

    public void SpawnWall(int index) {
        bool isFacingRight = transform.position.x  < -1.5f;
        Vector3 spawnPosition = isFacingRight ? new Vector3(-11.5f, -3.5f + (0.5f*index), 0f) : new Vector3(8.5f, -3.5f + (0.5f*index), 0f);
        GameObject wall = Instantiate(wallPrefabs[index], spawnPosition, Quaternion.identity);
        wall.GetComponent<AbominationWall>().SetDirection(isFacingRight);
    }
}

