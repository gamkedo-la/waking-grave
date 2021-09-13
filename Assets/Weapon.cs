using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    private PlatformerInputs platformerInputs;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;

    private void Awake() {
        platformerInputs = new PlatformerInputs();
    }

    private void OnEnable() {
        platformerInputs.Player.Shoot.performed += Shoot;
        platformerInputs.Player.Shoot.Enable();
    }

    private void OnDisable() {
        platformerInputs.Player.Shoot.Disable();
    }


    // Update is called once per frame
    private void Shoot(InputAction.CallbackContext obj)
    {
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    }
}
