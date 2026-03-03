using UnityEngine;
using Photon.Pun;

/// <summary>
/// Controls the local player's movement, including horizontal movement, jumping, and gravity.
/// Requires a CharacterController and SC_Local_Player_Input.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class SC_Local_Player_Movement : SC_Local_Player_Behaviour
{
    /// <summary> Movement speed for the character. </summary>
    public float speed = 5f;
    /// <summary> Force applied during a jump. </summary>
    public float jumpHeight = 2f;
    /// <summary> Gravity force affecting vertical movement. </summary>
    public float gravity = -9.81f;

    private CharacterController controller;
    private SC_Local_Player_Input input;
    private Vector3 velocity;

    /// <summary>
    /// Initializes internal references. Only runs for the local player.
    /// </summary>
    protected override void Awake()
    {
        base.Awake(); // Checks !photonView.IsMine
        if (!enabled) return;

        controller = GetComponent<CharacterController>();
        input = GetComponent<SC_Local_Player_Input>();
    }

    /// <summary>
    /// Updates character position and velocity.
    /// </summary>
    void Update()
    {
        // 1. Horizontal movement.
        Vector3 move = transform.right * input.MoveInput.x +
                       transform.forward * input.MoveInput.y;

        controller.Move(move * speed * Time.deltaTime);

        // 2. Ground check and velocity reset.
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Slight downward force to stay grounded.
        }

        // 3. Jump logic.
        if (input.JumpPressed && controller.isGrounded)
        {
            // Calculate jump velocity using basic physics.
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // 4. Apply gravity.
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
