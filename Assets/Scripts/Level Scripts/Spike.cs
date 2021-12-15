using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    Animator anim;
    public float direction;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        direction = transform.localScale.x;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            anim.Play("Activate");
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerController>().GetDamaged(transform.position.x);
        }
    }
}
