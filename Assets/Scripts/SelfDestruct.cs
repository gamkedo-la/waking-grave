using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float removeSeconds = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, removeSeconds);
        
    }

   
}
