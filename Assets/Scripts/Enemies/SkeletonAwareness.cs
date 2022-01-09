using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAwareness : MonoBehaviour
{
    public ZombieController zc;
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.name);
        if(other.CompareTag("Player") ) {
            zc.EngagePlayer(other.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player") ) {
            zc.DisengagePlayer();
        }
    }
}
