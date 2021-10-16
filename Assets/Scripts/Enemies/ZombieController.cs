using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    [SerializeField] private Vector2[] positions;
    [SerializeField] private Transform playerPosition;     // Will only use if corrupted
    [SerializeField] private float movementSpeed;
    [SerializeField] private int health;
    [SerializeField] private int max_health;  // Might turn to a constant later on
    private int posIndex;


    // Update is called once per frame
    void Update()
    {
        if(playerPosition) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerPosition.position.x, transform.position.y), movementSpeed * 2 * Time.deltaTime);
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
            playerPosition = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            playerPosition = null;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(positions[0], positions[1]);
    }
}
