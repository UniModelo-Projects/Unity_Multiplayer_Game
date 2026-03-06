using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [Header("UI - Room Info")]
    public TextMeshProUGUI roomNameDisplay;
    public Button brawlButton;
    public TextMeshProUGUI brawlButtonText;

    [Header("Characters in Room")]
    public GameObject[] characterObjects; // 0: Ranger, 1: Barbarian, 2: Mage, 3: Knight

    private void Start()
    {
        if (!PhotonNetwork.InRoom) return;

        // Ensure we are syncing scenes correctly
        PhotonNetwork.AutomaticallySyncScene = true;

        roomNameDisplay.text = "Room: " + PhotonNetwork.CurrentRoom.Name;
        UpdateCharacterDisplay();
        UpdateBrawlButtonState();
    }

    // Called for the client that just joined
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom called in RoomManager.");
        UpdateCharacterDisplay();
        UpdateBrawlButtonState();
    }

    // Called for others already in the room
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined the room.");
        UpdateCharacterDisplay();
        UpdateBrawlButtonState();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + " left the room.");
        UpdateCharacterDisplay();
        UpdateBrawlButtonState();
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("Master Client switched to: " + newMasterClient.NickName);
        UpdateBrawlButtonState();
    }

    private void UpdateCharacterDisplay()
    {
        if (PhotonNetwork.CurrentRoom == null) return;

        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        Debug.Log("Updating display for " + playerCount + " players.");

        for (int i = 0; i < characterObjects.Length; i++)
        {
            if (characterObjects[i] != null)
            {
                // Activate characters based on the number of players
                characterObjects[i].SetActive(i < playerCount);
            }
        }
    }

    private void UpdateBrawlButtonState()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            brawlButton.interactable = true;
            brawlButtonText.text = "BRAWL!";
        }
        else
        {
            // If we're not the host, we should wait for the host to start.
            // AutomaticallySyncScene will handle late joiners automatically if it's set to true.
            brawlButton.interactable = false;
            brawlButtonText.text = "Waiting for Admin to start...";
        }
    }

    public void OnBrawlButtonClicked()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // If master starts, everyone follows because of AutomaticallySyncScene
            PhotonNetwork.LoadLevel("SampleScene");
        }
        else
        {
            Debug.LogWarning("Only the Host can start the game.");
        }
    }

    public void OnLeaveRoomButtonClicked()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }
}
