using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// Entry point for the multiplayer setup. Handles connecting to the Photon Master Server,
/// joining a room, and spawning the local player.
/// </summary>
public class Launcher : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// Initial connection attempt using default Photon settings.
    /// </summary>
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    /// <summary>
    /// Called when the client successfully connects to the Photon Master Server.
    /// Attempts to join a room named "Room1" or create it if it doesn't exist.
    /// </summary>
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");

        PhotonNetwork.JoinOrCreateRoom(
            "Room1",
            new RoomOptions { MaxPlayers = 4 },
            TypedLobby.Default
        );
    }

    /// <summary>
    /// Called when the client successfully joins a room.
    /// Instantiates the player prefab across the network.
    /// </summary>
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");

        Vector3 spawnPos = new Vector3(0, 2, 0);
        // Spawns the player prefab from Resources/Res_Player/Player.prefab
        PhotonNetwork.Instantiate("Res_Player/Player", spawnPos, Quaternion.identity);
    }
}
