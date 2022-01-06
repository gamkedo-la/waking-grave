using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GateController : MonoBehaviour
{
    private bool isEnabled;
    private PlatformerInputs platformerInputs;
    public string sceneName;

    private void Awake() {
        platformerInputs = new PlatformerInputs();
    }
    private void OnEnable() {
        platformerInputs.Player.Shoot.performed += Enter;
        platformerInputs.Player.Shoot.Enable();
    }

    private void OnDisable() {
        platformerInputs.Player.Shoot.Disable();
    }

    private void Enter(InputAction.CallbackContext obj)
    {
        if(isEnabled) {
            SceneManager.LoadScene(sceneName);
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            isEnabled = true;
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            isEnabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
