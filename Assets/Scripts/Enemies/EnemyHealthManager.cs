using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public Image healthBar;

    public GameObject prefabToSpawnOnDeath; //optional

    private void Awake() {
        if(!GetComponent<BoxCollider2D>()) {
            Debug.LogError("No collider attached to enemy" + transform.position + " " + !GetComponent<BoxCollider2D>() + " " + !GetComponent<CircleCollider2D>());
        }
    }

    public void GetDamaged(int damage) {
        currentHealth -= damage;
        if(healthBar) {
            healthBar.fillAmount -= (1.0f / maxHealth);
        }
        if(currentHealth == 0) {
            if (prefabToSpawnOnDeath)
            {
                GameObject.Instantiate(prefabToSpawnOnDeath, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    public bool OverHalfLife() {
        return currentHealth > maxHealth /2;
    }
}
