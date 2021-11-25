using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterWarlockScript : MonoBehaviour
{
    private WarlockSM _sm;
    private WarlockRetreating warlockRetreatingScript;

    private void Start()
    {
        _sm = GetComponent<WarlockSM>();
        //warlockRetreatingScript = GetComponent<WarlockRetreating>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision with warlocks circle collider detected, warlock should be retreating");
        _sm.ChangeState(_sm.retreatingState);
    }
}
