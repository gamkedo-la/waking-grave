using UnityEngine;

public class LightBlast : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            other.GetComponent<PlayerController>().GetDamaged(transform.position.x);
        }
    }
}
