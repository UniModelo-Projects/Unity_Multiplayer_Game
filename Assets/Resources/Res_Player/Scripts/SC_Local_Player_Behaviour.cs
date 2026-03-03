using UnityEngine;
using Photon.Pun;

/// <summary>
/// Base class for player components that should only run on the local player's instance.
/// Automatically disables itself and nested components (Camera, AudioListener) if PhotonView is not owned by the local client.
/// </summary>
public class SC_Local_Player_Behaviour : MonoBehaviourPun
{
    protected virtual void Awake()
    {
        // If this instance is NOT controlled by the local player, disable essential components to prevent conflicts.
        if (!photonView.IsMine)
        {
            enabled = false;
            // Disable nested cameras and audio listeners to avoid multi-camera and multi-listener conflicts.
            foreach (var cam in GetComponentsInChildren<Camera>())
                cam.enabled = false;
            foreach (var audio in GetComponentsInChildren<AudioListener>())
                audio.enabled = false;
        }
    }
}
