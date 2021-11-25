using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterWarlockScript : MonoBehaviour
{
    [SerializeField] ParticleSystem eldrichBlastParticleSystem;
    private WarlockSM _sm;
    private WarlockRetreating warlockRetreatingScript;

    private GameObject player;
    private Vector2 aimingCoordinates;
    private bool eldrichBlastMovingTowardsPlayer = false;
    private float eldrichBlastSpeed = 0.3f;

    private void Start()
    {
        _sm = GetComponent<WarlockSM>();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(DelayedToggleAttackPreparation());
        //warlockRetreatingScript = GetComponent<WarlockRetreating>();
    }

    private void Update()
    {
        if (eldrichBlastMovingTowardsPlayer)
        {
            if (Mathf.Abs(aimingCoordinates.x - eldrichBlastParticleSystem.gameObject.transform.position.x) > 0.1 &&
                Mathf.Abs(aimingCoordinates.y - eldrichBlastParticleSystem.gameObject.transform.position.y) > 0.1)
            {
                Debug.Log("inside eldrich blast check");
                eldrichBlastParticleSystem.gameObject.transform.position =
                    Vector2.MoveTowards(eldrichBlastParticleSystem.gameObject.transform.position, aimingCoordinates, eldrichBlastSpeed);
            }
            else
            {
                eldrichBlastParticleSystem.gameObject.transform.position = gameObject.transform.position;
                eldrichBlastMovingTowardsPlayer = false;
                eldrichBlastParticleSystem.gameObject.SetActive(false);
                StartCoroutine(DelayedToggleAttackPreparation());
            }
        }
        else
        {
            eldrichBlastParticleSystem.gameObject.transform.position = gameObject.transform.position;
        }
    }

    private void GetAimingCoordinates()
    {
        aimingCoordinates = player.transform.position;
    }

    private void FireEldrichBlast()
    {
        GetAimingCoordinates();
        eldrichBlastMovingTowardsPlayer = true;
    }
    private IEnumerator DelayedToggleAttackPreparation()
    {
        yield return new WaitForSeconds(2);
        if (eldrichBlastParticleSystem.gameObject.activeSelf == false)
        {
            eldrichBlastParticleSystem.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(2);
        FireEldrichBlast();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision with warlocks circle collider detected, warlock should be retreating");
        _sm.ChangeState(_sm.retreatingState);
    }
}
