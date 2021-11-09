using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlingAndDie : MonoBehaviour
{
    [Header("Explosion Forces!! (influenced by mass)")]
    public float forceXmin = -10;
    public float forceXmax = 10;
    public float forceYmin = 1;
    public float forceYmax = 10;
    public float spinMin = -10;
    public float spinMax = 10;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(forceXmin,forceXmax),Random.Range(forceYmin,forceYmax)));
        GetComponent<Rigidbody2D>().AddTorque(Random.Range(spinMin,spinMax));
        Destroy(gameObject,5f);
    }

}
