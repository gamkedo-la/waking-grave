using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlockRetreating : BaseState
{
    WarlockSM _sm;
    private Vector3 startPos;
    float speed = 3.5f;
    float xDeviation = 8.0f;
    float yDeviation = 4.0f;
    public WarlockRetreating(WarlockSM stateMachine) : base("WarlockRetreating", stateMachine)
    {
        _sm = stateMachine;
        startPos = _sm.transform.position;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        // FIXME: warlocksRetreatPosition needs implementing
        //if (_sm.transform.position.x < warlocksRetreatPosition.transform.position.x)
        //{
           
            // error
            // _sm.transform.position.x += speed;
            // you can't change positions in unity this way ---^
            // you have to make a brand new vector3 or call a method

            // alternative that hopefully works:
            //_sm.transform.position = new Vector3(_sm.transform.position.x+speed,_sm.transform.position.y,_sm.transform.position.z);
            
        //}
    }
}
