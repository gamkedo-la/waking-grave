using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlingAndDie : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-100f,100f),Random.Range(1f,100f)));
        GetComponent<Rigidbody2D>().AddTorque(Random.Range(-100f,100f));
        Destroy(gameObject,5f);
    }

}
