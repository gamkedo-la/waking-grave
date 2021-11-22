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
    public bool isCorrupted;
    
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
        if (gameObject.name == "Skeleton")
        {
            return;
        }
        GetComponent<AudioSource>().clip = zombieIdleAudioClip;
        GetComponent<AudioSource>().volume = 1f;
        GetComponent<AudioSource>().Play();

        // Set wander positions
        positions = new Vector2[2];
        positions[0] = transform.GetChild(0).position;
        positions[1] = transform.GetChild(1).position;

        mainCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if(playerPosition) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerPosition.position.x, transform.position.y), chaseSpeed * Time.deltaTime);
        } else {
            transform.position = Vector2.MoveTowards(transform.position, positions[posIndex], movementSpeed * Time.deltaTime);

            if(Vector2.Distance(transform.position, positions[posIndex]) < 0.1f) {
                posIndex = posIndex == 1 ? 0 : 1; // since there are only 2 posible positions
                transform.Rotate(0, 180f, 0);
            }
        }
    }

    // player just entered detection range
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            Debug.Log("Zombie is aware of the player!");
            if (spawnIfAlerted) {
                // spawn an "!" above their head and slightly in front of them in the z plane
                Instantiate(spawnIfAlerted, new Vector3(transform.position.x,transform.position.y+1,transform.position.z+0.1f), Quaternion.identity);
            }
            // turn on the red eyes!
            if (activeIfAlterted) activeIfAlterted.SetActive(true);
            // start following the player
            playerPosition = other.transform;

            mainCamera.GetComponent<CameraAudioScript>().PlayRandomZombieAlertSFX();
        }
    }

    

    // player just left detection range
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            Debug.Log(gameObject.name+" is no longer aware of the player!");
            // turn off the red eyes!
            if (activeIfAlterted) activeIfAlterted.SetActive(false);
            // stop following the player
            playerPosition = null;
        }
    }

    // something just bumped into us
    private void OnCollisionEnter2D(Collision2D coll) {

        // FIXME:
        // maybe check if the PROJECTILE/WEAPON MESH hit us, not the player?
        if(coll.gameObject.CompareTag("Player")) {
            
            Debug.Log(gameObject.name+" touched the player!");

            if (spawnIfTouched) {
                Instantiate(spawnIfTouched, new Vector3(transform.position.x,transform.position.y+1,transform.position.z+0.1f), Quaternion.identity);
            }

            if (dieIfTouched) {
                health = 0;
            }

            if (dieIfJumpedOn) {
                // is player above zombie?
                float voffset = coll.gameObject.transform.position.y - transform.position.y;
                if (voffset > 0.5f) {
                    Debug.Log(gameObject.name+" was jumped on top of by player!");
                    health = 0;
                } else {
                    Debug.Log(gameObject.name+" below player dist: "+voffset);
                }
            }

            // FIXME: this needs more work - flesh this out for score etc
            // also put this check elsewhere in case we lose health other ways
            if ((dieIfTouched || dieIfJumpedOn) && health<=0) {
                Debug.Log(gameObject.name+" has zero health! Dying!");
                if (spawnIfKilled) {
                    Instantiate(spawnIfKilled, new Vector3(transform.position.x,transform.position.y+1,transform.position.z+0.1f), Quaternion.identity);
                }
                 // vanish immediately! (FIXME: wait for an anim to finish?)
                 Destroy(gameObject/* ,delayinseconds */); 
            }

        }
    }
}
