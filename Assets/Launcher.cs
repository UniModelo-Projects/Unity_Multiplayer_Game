using UnityEngine;

/// <summary>
/// This script is deprecated in favor of the specialized managers (MainMenuManager, LobbyManager, PlayerSpawner).
/// Please use the Main_Menu scene as your entry point.
/// </summary>
public class Launcher : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Please use the Main_Menu scene to start the game.");
    }
}
