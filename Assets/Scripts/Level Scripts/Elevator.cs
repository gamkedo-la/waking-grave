using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float speed; // 2 is a good speed for now

    public bool isEnabled;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnabled) {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }
}
