using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    // FIXME: why serialize a private field?
    // why not make it public so unity acts normal?
    // why? private makes it so other code can't access these values
    [SerializeField] private Vector2[] positions;
    [SerializeField] private Transform playerPosition;     // Will only use if corrupted
    [SerializeField] private float movementSpeed;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private int health;
    [SerializeField] private int max_health;  // Might turn to a constant later on
    
    // public fields
    public GameObject spawnIfAlerted;
    public GameObject spawnIfBumped;
    public GameObject spawnIfShot;
    public GameObject spawnIfKilled;

    // actual truely private variables
    private int posIndex;

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

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            Debug.Log("Zombie is aware of the player!");
            if (spawnIfAlerted) {
                // spawn an "!" above their head and slightly in front of them in the z plane
                Instantiate(spawnIfAlerted, new Vector3(transform.position.x,transform.position.y+1,transform.position.z+0.1f), Quaternion.identity);
            }
            playerPosition = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            Debug.Log("Zombie is no longer aware of the player!");
            playerPosition = null;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(positions[0], positions[1]);
    }
}
