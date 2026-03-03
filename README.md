# Unity Multiplayer Test (Photon PUN 2)

A multiplayer first-person project built in Unity using Photon Unity Networking (PUN 2) and the New Input System.

## 🚀 Getting Started

### 1. Clone the Repository
Open your terminal (or Git Bash) and run:
```bash
git clone https://github.com/UniModelo-Projects/Unity_Multiplayer_Game.git
```

### 2. Open in Unity
1.  Open **Unity Hub**.
2.  Click **Add** -> **Add project from disk**.
3.  Select the `Unity_Multiplayer_Game` folder.
4.  **Unity Version:** Use **Unity 6 (6000.3.9f1)** for best compatibility.
5.  **First Load:** Unity will rebuild the `Library/` folder (this may take a few minutes).

### 3. Photon Configuration
The project is pre-configured with a Photon AppID. To verify:
*   Navigate to `Assets/Photon/PhotonUnityNetworking/Resources/`.
*   Select `PhotonServerSettings.asset`.
*   Ensure **App Id Realtime** is populated.

### 4. Running the Game
1.  Open `Assets/Scenes/Main_Menu.unity`.
2.  Press the **Play** button in the Unity Editor.

### 5. Testing Multiplayer
To test with two players:
*   **Build & Run:** Go to `File` -> `Build Settings`, click `Add Open Scenes`, and click `Build and Run` to launch a standalone client. Then, press **Play** in the Unity Editor to join the same room.

## 🛠 Features
*   **Networking:** Photon PUN 2 for real-time synchronization.
*   **Input System:** Modern Unity Input System with decoupled logic.
*   **Modular Controller:** Base behaviour classes for clean network ownership handling.
