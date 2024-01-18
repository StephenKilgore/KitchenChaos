using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler onInteractAction;
    public event EventHandler onInteractAlternateAction;
    public event EventHandler OnPauseAction;
    
    public static GameInput Instance { get; private set; }
    private PlayerInputActions playerInputActions;
    
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;

        Instance = this;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;
        
        playerInputActions.Dispose();
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        onInteractAction.Invoke(this,EventArgs.Empty);
    }
    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        onInteractAlternateAction.Invoke(this,EventArgs.Empty);
    }
    public Vector2 GetMovementVectorNormalized()
    { 
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = (Vector3) inputVector.normalized;
        return inputVector;
    }
}
