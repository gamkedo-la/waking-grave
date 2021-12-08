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

    public void GetDamaged(int damage) {
        currentHealth -= damage;
        Debug.Log("was hit " + currentHealth );
        if(currentHealth < 0) {
            Destroy(gameObject);
        }
    }
}
