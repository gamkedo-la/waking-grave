using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    private List<Text> keyboardHints;
    private List<Text> gamepadHints;

    [SerializeField] private PlayerController playerController;

    private string currentInputDevice;
    
    // Start is called before the first frame update
    void Start()
    {
        // Find player object if none is provided in editor.
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
        }
        
        // Find and store references to all gamepad hints.
        gamepadHints = new List<Text>();
        foreach (var hintObject in GameObject.FindGameObjectsWithTag("GamepadHint"))
        {
            var hintText = hintObject.GetComponent<Text>();
            gamepadHints.Add(hintText);
        }
        
        // Find and store references to all keyboard hints.
        keyboardHints = new List<Text>();
        foreach (var hintObject in GameObject.FindGameObjectsWithTag("KeyboardHint"))
        {
            var hintText = hintObject.GetComponent<Text>();
            keyboardHints.Add(hintText);
        }
        
        // Assume keyboard is used initially.
        currentInputDevice = "Keyboard";
        ToggleHintTexts(gamepadHints, true);
        ToggleHintTexts(gamepadHints, false);
        
        // Subscribe to player's input events to toggle correct hint text objects.
        playerController.GetPlatformerInputs.Player.Dash.performed += UpdateHintTexts;
        playerController.GetPlatformerInputs.Player.Jump.performed += UpdateHintTexts;
        playerController.GetPlatformerInputs.Player.Shoot.performed += UpdateHintTexts;
        playerController.GetPlatformerInputs.Player.Move.performed += UpdateHintTexts;
    }
    
    private void UpdateHintTexts(InputAction.CallbackContext obj)
    {
        var deviceName = obj.control.device.name;
        
        if (deviceName == "Keyboard" && currentInputDevice != "Keyboard")
        {
            ToggleHintTexts(keyboardHints, true);
            ToggleHintTexts(gamepadHints, false);
        }
        if (deviceName != "Keyboard" && currentInputDevice == "Keyboard")
        {
            ToggleHintTexts(keyboardHints, false);
            ToggleHintTexts(gamepadHints, true);
        }

        currentInputDevice = deviceName;
    }

    private void ToggleHintTexts(List<Text> hintList, bool status)
    {
        foreach (var hint in hintList)
        {
            hint.enabled = status;
        }
    }
}
