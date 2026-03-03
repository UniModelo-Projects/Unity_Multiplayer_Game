using UnityEngine;

/// <summary>
/// Controls the camera vertical rotation and player body horizontal rotation based on mouse input.
/// Only runs for the local player's instance.
/// </summary>
public class SC_Local_Player_MouseLook : SC_Local_Player_Behaviour
{
    /// <summary> Mouse sensitivity for look input. </summary>
    public float sensitivity = 0.1f;
    /// <summary> The transform of the player's body that rotates horizontally. </summary>
    public Transform playerBody;
    private float xRotation = 0f;
    private SC_Local_Player_Input input;

    /// <summary>
    /// Locks the cursor to the screen and initializes the input reference.
    /// Only runs for the local player.
    /// </summary>
    protected override void Awake()
    {
        base.Awake(); // Handles !photonView.IsMine
        if (enabled) // Only run if this is local player
        {
            Cursor.lockState = CursorLockMode.Locked;
            input = GetComponentInParent<SC_Local_Player_Input>();
        }
    }

    /// <summary>
    /// Updates rotations based on look input values.
    /// </summary>
    void Update()
    {
        if (!enabled) return;

        // Scale mouse input by sensitivity.
        float mouseX = input.LookInput.x * sensitivity;
        float mouseY = input.LookInput.y * sensitivity;

        // Vertical rotation clamped between -90 and 90 degrees.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply local rotation to the camera (where this script is attached).
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // Apply horizontal rotation to the player's main body.
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
