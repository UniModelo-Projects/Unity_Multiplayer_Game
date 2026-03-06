using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviourPunCallbacks
{
    [Header("UI References")]
    public TMP_InputField usernameInput;
    public Button playButton;

    private const string USERNAME_KEY = "PhotonUsername";

    private void Start()
    {
        // Important for PUN: ensures joined clients load the same scene as the Master Client
        PhotonNetwork.AutomaticallySyncScene = true;

        // Disable play button until connected to Master
        playButton.interactable = false;

        // Load username or generate random one
        string savedName = PlayerPrefs.GetString(USERNAME_KEY, "Player " + Random.Range(0, 1000));
        usernameInput.text = savedName;
        PhotonNetwork.NickName = savedName;

        // Connect to Master Server if not already connected
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        else if (PhotonNetwork.IsConnectedAndReady)
        {
            playButton.interactable = true;
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server");
        playButton.interactable = true;
        // Removed JoinLobby from here
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }

    public void OnPlayButtonClicked()
    {
        string username = usernameInput.text;
        if (string.IsNullOrEmpty(username))
        {
            username = "Player " + Random.Range(0, 1000);
        }

        // Save username
        PlayerPrefs.SetString(USERNAME_KEY, username);
        PhotonNetwork.NickName = username;

        // Load Lobby scene
        SceneManager.LoadScene("Lobby");
    }
}
