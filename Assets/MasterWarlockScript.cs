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

    private bool warlockPreparingEldrichBlast = false;

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
                var shapeModuleOfParticleSystem = eldrichBlastParticleSystem.shape;
                if (shapeModuleOfParticleSystem.angle > 0.3f)
                {
                    shapeModuleOfParticleSystem.angle -= 0.05f;
                }
                if (shapeModuleOfParticleSystem.radius > 0.4f)
                {
                    shapeModuleOfParticleSystem.radius -= 0.05f;
                }
                

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

        if (warlockPreparingEldrichBlast)
        {
            var shapeModuleOfParticleSystem = eldrichBlastParticleSystem.shape;
            
            if (shapeModuleOfParticleSystem.angle < 1f)
            {
                shapeModuleOfParticleSystem.angle += 0.025f;
            }
            if (shapeModuleOfParticleSystem.radius < 1f)
            {
                shapeModuleOfParticleSystem.radius += 0.0125f;
            }
        }
    }

    private void GetAimingCoordinates()
    {
        aimingCoordinates = player.transform.position;
    }

    private void FireEldrichBlast()
    {
        warlockPreparingEldrichBlast = false;
        GetAimingCoordinates();
        eldrichBlastMovingTowardsPlayer = true;
    }
    private IEnumerator DelayedToggleAttackPreparation()
    {
        yield return new WaitForSeconds(2);
        if (eldrichBlastParticleSystem.gameObject.activeSelf == false)
        {
            warlockPreparingEldrichBlast = true;
            eldrichBlastParticleSystem.gameObject.SetActive(true);
            var shapeModuleOfParticleSystem = eldrichBlastParticleSystem.shape;
            shapeModuleOfParticleSystem.angle = 0f;
            shapeModuleOfParticleSystem.radius = 0f;
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
