using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    // FIXME: why serialize a private field?
    // why not make it public so unity acts normal?
    // why? private makes it so other code can't access these values
    [Header("Zombie AI Settings")]
    [SerializeField] private Vector2[] positions;
    [SerializeField] private Transform playerPosition;     // Will only use if corrupted
    [SerializeField] private float movementSpeed;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private int health;
    [SerializeField] private int max_health;  // Might turn to a constant later on
    public bool isCorrupted; // if zombie is not corrupted he wont chase player
    // public fields
    [Header("Particle Prefabs to Instantiate")]
    public GameObject spawnIfAlerted;
    public GameObject spawnIfTouched; // UNIMPLEMENTED
    public GameObject spawnIfShot; // UNIMPLEMENTED
    public GameObject spawnIfKilled; // UNIMPLEMENTED
    public bool dieIfTouched = false;
    public bool dieIfJumpedOn = false;
    
    [Header("Things we turn on and off")]
    public GameObject activeIfAlterted;
    public GameObject activeIfDamaged; // UNIMPLEMENTED
    public GameObject activeIfWandering; // UNIMPLEMENTED

    // actual truely private variables
    private int posIndex;

    [Header("SFX")]
    //SFX
    [SerializeField] AudioClip zombieIdleAudioClip;
    private Camera mainCamera;

    
    private void Start()
    {
        // Set wander positions
        positions = new Vector2[2];
        positions[0] = transform.GetChild(0).position;
        positions[1] = transform.GetChild(1).position;

        if (gameObject.name == "Skeleton")
        {
            return;
        }

        GetComponent<AudioSource>().clip = zombieIdleAudioClip;
        GetComponent<AudioSource>().volume = 1f;
        GetComponent<AudioSource>().Play();

        mainCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if(playerPosition) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerPosition.position.x, transform.position.y), chaseSpeed * Time.deltaTime);
        } else {
            transform.position = Vector2.MoveTowards(transform.position, positions[posIndex], movementSpeed * Time.deltaTime);

            if(Vector2.Distance(transform.position, positions[posIndex]) < 0.3f) {
                posIndex = posIndex == 1 ? 0 : 1; // since there are only 2 posible positions
                transform.Rotate(0, 180f, 0);
            }
        }
    }

    // player just entered detection range
    public void EngagePlayer(Transform player) {
        if(isCorrupted) {
            if (spawnIfAlerted) {
                // spawn an "!" above their head and slightly in front of them in the z plane
                Instantiate(spawnIfAlerted, new Vector3(transform.position.x,transform.position.y+1,transform.position.z+0.1f), Quaternion.identity);
            }
            if (activeIfAlterted) activeIfAlterted.SetActive(true);
            // start following the player
            playerPosition = player;
        }
    }

    // player just left detection range
    public void DisengagePlayer() {
        if(isCorrupted) {
            // turn off the red eyes!
            if (activeIfAlterted) activeIfAlterted.SetActive(false);
            // stop following the player
            playerPosition = null;
        }
    }

    private void Die() {
        if ((dieIfTouched || dieIfJumpedOn) && health<=0) {
            if (spawnIfKilled) {
                Instantiate(spawnIfKilled, new Vector3(transform.position.x,transform.position.y+1,transform.position.z+0.1f), Quaternion.identity);
            }
            // vanish immediately! (FIXME: wait for an anim to finish?)
            Destroy(gameObject/* ,delayinseconds */); 
        }
    }
}
