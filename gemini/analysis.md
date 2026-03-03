# Project Analysis: Multiplayer Test (Unity + Photon PUN)

This project is a multiplayer setup using Unity and Photon Unity Networking (PUN 2). It implements a basic first-person player controller with network synchronization.

## 1. Networking Architecture
The project uses **Photon Unity Networking (PUN 2)** for multiplayer capabilities.
- **Entry Point:** `Launcher.cs` handles the initial connection to Photon Master Servers.
- **Room Management:** It automatically joins or creates a room named "Room1" upon connection.
- **Player Spawning:** Once joined, it uses `PhotonNetwork.Instantiate` to spawn the player prefab located at `Assets/Resources/Res_Player/Player.prefab`.

## 2. Player Controller System
The player controller is divided into several modular components, all inheriting from a common base class:

### Core Scripts:
- **`SC_Local_Player_Behaviour.cs`**: A foundational class that ensures components only run on the local player's client. It disables cameras, audio listeners, and the script itself if `photonView.IsMine` is false.
- **`SC_Local_Player_Input.cs`**: Utilizes the **New Input System**. It listens to the `PlayerInputActions` and exposes properties for movement, looking, and jumping.
- **`SC_Local_Player_MouseLook.cs`**: Handles first-person camera rotation (vertical) and player body rotation (horizontal). It also handles cursor locking.
- **`SC_Local_Player_Movement.cs`**: Implements basic character physics including:
    - Horizontal movement relative to the player's orientation.
    - Jumping with simple physics calculations.
    - Constant gravity application using a `CharacterController`.

## 3. Key Assets & Folders
- **`Assets/Photon/`**: Contains the full PUN 2 library and its dependencies.
- **`Assets/Resources/Res_Player/`**: Contains the player prefab, its associated scripts, and input action definitions.
- **`Assets/Settings/`**: Contains Universal Render Pipeline (URP) settings, suggesting the project uses URP for rendering.

## 4. Observations & Recommendations
- **Synchronization**: The player movement is local-first. Ensure the `Player` prefab has a `PhotonView` and appropriate synchronizers (like `PhotonTransformView`) to sync position and rotation across the network.
- **Input System**: The project successfully uses the modern Input System package, which is well-structured and decoupled from the movement logic.
- **Modularity**: The use of a base class (`SC_Local_Player_Behaviour`) to handle local-only logic is a clean and effective pattern for PUN development.
