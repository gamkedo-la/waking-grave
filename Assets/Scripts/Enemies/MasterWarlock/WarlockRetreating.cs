using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlockRetreating : BaseState
{
    WarlockSM _sm;
    float speed = 0.125f;

    [SerializeField] GameObject warlocksRetreatPosition;
    public WarlockRetreating(WarlockSM stateMachine) : base("WarlockRetreating", stateMachine)
    {
        _sm = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        warlocksRetreatPosition = GameObject.FindGameObjectWithTag("WarlockRetreatPosition");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        Debug.Log("warlocksRetreatPosition.transform.position.x: " + warlocksRetreatPosition.transform.position.x);

        if (_sm.transform.position.x != warlocksRetreatPosition.transform.position.x)
        {
            _sm.transform.position = Vector2.MoveTowards(_sm.transform.position, warlocksRetreatPosition.transform.position, speed);
        }    
        else if (_sm.transform.position.x - warlocksRetreatPosition.transform.position.x < 0.1 &&
            _sm.transform.position.y - warlocksRetreatPosition.transform.position.y < 0.1)
        {
            _sm.ChangeState(_sm.idleState);
            Debug.Log("warlocksRetreatPosition: " + warlocksRetreatPosition);
            warlocksRetreatPosition.GetComponent<RetreatPositionScript>().ResetPosition();
        }
    }
}