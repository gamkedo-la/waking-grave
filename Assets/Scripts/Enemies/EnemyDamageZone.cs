using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerController>().GetDamaged(transform.position.x, true);
        }
    }
}
