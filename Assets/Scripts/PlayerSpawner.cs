using UnityEngine;
using Photon.Pun;

/// <summary>
/// Handles spawning the local player in the game scene.
/// </summary>
public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    private bool hasSpawned = false;

    void Start()
    {
        Debug.Log($"PlayerSpawner Start. Connected: {PhotonNetwork.IsConnectedAndReady}, InRoom: {PhotonNetwork.InRoom}");
        
        if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.InRoom)
        {
            SpawnPlayer();
        }
    }

    // Fallback in case the client wasn't fully "InRoom" the exact frame Start ran
    public override void OnJoinedRoom()
    {
        Debug.Log("PlayerSpawner: OnJoinedRoom called.");
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        if (hasSpawned) return;
        
        hasSpawned = true;
        Debug.Log("Spawning Player...");

        // 1. Find and disable the default scene camera to avoid conflicts with player camera
        Camera sceneCam = Camera.main;
        if (sceneCam != null)
        {
            Debug.Log("Disabling scene camera: " + sceneCam.name);
            sceneCam.gameObject.SetActive(false);
        }

        // 2. Simple random spawn point logic
        Vector3 spawnPos = new Vector3(Random.Range(-5f, 5f), 2, Random.Range(-5f, 5f));
        
        // 3. Spawns the player prefab from Resources/Res_Player/Player.prefab
        GameObject player = PhotonNetwork.Instantiate("Res_Player/Player", spawnPos, Quaternion.identity);
        
        if (player != null)
        {
            Debug.Log("Player successfully instantiated!");
        }
        else
        {
            Debug.LogError("Failed to instantiate player prefab!");
        }
    }
}
