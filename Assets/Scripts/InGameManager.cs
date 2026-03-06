using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // Added for new Input System

public class InGameManager : MonoBehaviourPunCallbacks
{
    [Header("UI Reference")]
    public GameObject menuPanel; // Drag your Canvas or a Panel here
    
    private bool isMenuOpen = false;

    void Start()
    {
        // Ensure menu is closed and cursor is locked at start
        CloseMenu();
    }

    void Update()
    {
        // New Input System check for Escape key
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isMenuOpen)
                CloseMenu();
            else
                OpenMenu();
        }
    }

    public void OpenMenu()
    {
        isMenuOpen = true;
        if (menuPanel != null) menuPanel.SetActive(true);
        
        // Unlock the cursor so you can click buttons
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseMenu()
    {
        isMenuOpen = false;
        if (menuPanel != null) menuPanel.SetActive(false);
        
        // Relock the cursor for the game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LeaveRoom()
    {
        Debug.Log("Leaving Room...");
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left Room. Loading Main Menu...");
        // Ensure cursor is visible for the main menu
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        SceneManager.LoadScene("Main_Menu");
    }
}
