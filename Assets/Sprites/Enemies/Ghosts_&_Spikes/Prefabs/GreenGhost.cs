using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenGhost : MonoBehaviour
{

    [SerializeField]
    GameObject Fireball;

    float fireRate;
    float nextFire;

    // Use this for initialization
    void Start()
    {
        fireRate = 3f;
        nextFire = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfTimeToFire();

    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            Instantiate(Fireball, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}
