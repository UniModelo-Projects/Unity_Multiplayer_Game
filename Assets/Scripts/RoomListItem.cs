using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class RoomListItem : MonoBehaviour
{
    public TextMeshProUGUI roomNameText;
    public TextMeshProUGUI playerStatusText;
    public Button joinButton;
    
    private RoomInfo _info;

    public void SetInfo(RoomInfo info)
    {
        _info = info;
        roomNameText.text = info.Name;
        playerStatusText.text = $"{info.PlayerCount}/{info.MaxPlayers}";
        
        // Ensure the button is hooked up
        if (joinButton != null)
        {
            joinButton.onClick.RemoveAllListeners();
            joinButton.onClick.AddListener(OnClick);
        }
    }

    public void OnClick()
    {
        Debug.Log("Attempting to join room: " + _info.Name);
        PhotonNetwork.JoinRoom(_info.Name);
    }
}
