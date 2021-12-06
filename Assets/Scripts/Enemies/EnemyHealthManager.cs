using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    private void Awake() {
        if(!GetComponent<BoxCollider2D>()) {
            Debug.LogError("No collider attached to enemy");
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Bullet")) {
            currentHealth--;
            Debug.Log("was hit " + currentHealth );
            if(currentHealth < 0) {
                Destroy(gameObject);
            }
        }
    }
}
