using UnityEngine;

/// <summary>
/// Handles player input via the new Input System's PlayerInputActions generated class.
/// Only runs if this script is on the local player's instance.
/// </summary>
public class SC_Local_Player_Input : SC_Local_Player_Behaviour
{
    /// <summary> Gets the move input value as a Vector2. </summary>
    public Vector2 MoveInput { get; private set; }
    /// <summary> Gets the look input value as a Vector2. </summary>
    public Vector2 LookInput { get; private set; }
    /// <summary> True if the jump button is currently pressed. </summary>
    public bool JumpPressed { get; private set; }

    private PlayerInputActions inputActions;

    /// <summary>
    /// Initializes input actions and registers event callbacks for Move, Look, and Jump actions.
    /// Only runs for the local player.
    /// </summary>
    protected override void Awake()
    {
        base.Awake(); // Checks !photonView.IsMine
        if (!enabled) return; // Only local player

        inputActions = new PlayerInputActions();
        inputActions.Enable();

        // Subscribe to input events to update local state.
        inputActions.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => MoveInput = Vector2.zero;

        inputActions.Player.Look.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Look.canceled += ctx => LookInput = Vector2.zero;

        inputActions.Player.Jump.performed += ctx => JumpPressed = true;
        inputActions.Player.Jump.canceled += ctx => JumpPressed = false;
    }

    /// <summary>
    /// Disables input actions when the component is disabled to release resources.
    /// </summary>
    void OnDisable()
    {
        if (enabled && inputActions != null)
            inputActions.Disable();
    }
}
