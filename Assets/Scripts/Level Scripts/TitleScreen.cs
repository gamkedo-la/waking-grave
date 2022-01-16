using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    private PlatformerInputs platformerInputs;
    private void Awake() {
        platformerInputs = new PlatformerInputs();
    }

    private void OnEnable() {
        platformerInputs.Player.Shoot.performed += NextScene;
        platformerInputs.Player.Shoot.Enable();
    }

    private void OnDisable() {
        platformerInputs.Player.Shoot.Disable();
    }

    private void NextScene(InputAction.CallbackContext obj)
    {
        PlayerStats.finishedGraveyard = false;
        SceneManager.LoadScene("SelectionScreen");
    }
}
