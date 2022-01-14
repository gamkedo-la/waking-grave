using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EldritchBlast : MonoBehaviour
{
    bool charging;
    bool chasingPlayer;
    ParticleSystem eldrichBlastParticleSystem;
    public Transform target;
    Vector3 targetPosition;
    private float eldrichBlastSpeed = 0.03f;
    public Transform mwTransform;

    private void Awake() {
        eldrichBlastParticleSystem = GetComponent<ParticleSystem>();
    }
    // Start is called before the first frame update
    private void OnEnable() {
        // UnityEngine.Debug.Break();
        transform.position = mwTransform.position + new Vector3(0f, 1.5f, 0);
        var shapeModuleOfParticleSystem = eldrichBlastParticleSystem.shape;
        shapeModuleOfParticleSystem.angle = 0f;
        shapeModuleOfParticleSystem.radius = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!chasingPlayer) {
            var shape = eldrichBlastParticleSystem.shape;
            if (shape.angle < 1f)
            {
                shape.angle += 0.025f;
            }
            if (shape.radius < 1f)
            {
                shape.radius += 0.0125f;
            }
        } else  {
            var shape = eldrichBlastParticleSystem.shape;
            if (shape.angle > 0.3f)
            {
                shape.angle -= 0.05f;
            }
            if (shape.radius > 0.4f)
            {
                shape.radius -= 0.05f;
            }

            eldrichBlastParticleSystem.gameObject.transform.position =
                Vector2.MoveTowards(transform.position, targetPosition, eldrichBlastSpeed);

            if(Vector2.Distance(transform.position, targetPosition) < 0.1) {
                gameObject.SetActive(false);
                chasingPlayer = false;
            }
        }
    }

    public void Shoot() {
        chasingPlayer = true;
        targetPosition = target.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerController>().GetDamaged(transform.position.x, true);
            gameObject.SetActive(false);
            chasingPlayer = false;
        }
    }
}
