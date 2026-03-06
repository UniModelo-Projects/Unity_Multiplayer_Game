using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("UI - Username")]
    public TextMeshProUGUI usernameDisplay;

    [Header("UI - Room List")]
    public Transform roomListContent;
    public GameObject roomListItemPrefab;

    [Header("UI - Room Creation")]
    public TMP_InputField roomNameInput;
    public TMP_InputField maxPlayersInput;

    [Header("UI - Join by Code")]
    public TMP_InputField roomCodeInput;

    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();

    private void Start()
    {
        // Force sync scene to true
        PhotonNetwork.AutomaticallySyncScene = true;

        // Display current username
        usernameDisplay.text = PhotonNetwork.NickName;

        // Ensure we are in the lobby to receive the initial list
        if (PhotonNetwork.InLobby)
        {
            // If already in lobby, we must force a re-join or refresh
            // But usually, since we just loaded this scene, let's leave and join to be sure
            PhotonNetwork.LeaveLobby();
        }
        
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby. Waiting for initial room list...");
        cachedRoomList.Clear();
        UpdateRoomListUI();
    }

    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void OnCreateRoomButtonClicked()
    {
        string roomName = roomNameInput.text;
        if (string.IsNullOrEmpty(roomName))
        {
            roomName = "Room_" + Random.Range(1000, 9999);
        }

        byte maxPlayers;
        if (!byte.TryParse(maxPlayersInput.text, out maxPlayers))
        {
            maxPlayers = 4;
        }

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        
        // Keep the room alive for 60 seconds after the last player leaves.
        // This prevents the room from disappearing immediately during testing.
        roomOptions.EmptyRoomTtl = 60000; 

        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    public void OnJoinByCodeButtonClicked()
    {
        string roomCode = roomCodeInput.text;
        if (!string.IsNullOrEmpty(roomCode))
        {
            PhotonNetwork.JoinRoom(roomCode);
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Room List Update Received. Rooms in update: " + roomList.Count);
        // Update local cache
        foreach (var room in roomList)
        {
            if (room.RemovedFromList)
            {
                cachedRoomList.Remove(room.Name);
            }
            else
            {
                cachedRoomList[room.Name] = room;
            }
        }

        UpdateRoomListUI();
    }

    private void UpdateRoomListUI()
    {
        // Clear old list
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }

        // Add rooms from cache
        foreach (var room in cachedRoomList.Values)
        {
            GameObject entry = Instantiate(roomListItemPrefab, roomListContent);
            entry.GetComponent<RoomListItem>().SetInfo(room);
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room Successfully! IsMasterClient: " + PhotonNetwork.IsMasterClient);

        // ONLY the MasterClient should call LoadLevel. 
        // Other clients will follow automatically because of AutomaticallySyncScene = true.
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Room");
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Room Creation Failed: " + message);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Joining Room Failed: " + message);
    }
}
